using System;
using System.Runtime.InteropServices;

namespace OutputConsole.Extern
{
    public static class Kernel
    {
        public const string Library = "kernel32.dll";

        public enum StandardHandle : int
        {
            Input = -10,
            Output = -11,
            Error = -12
        }

        [DllImport(Library, EntryPoint = "GetStdHandle")]
        public static extern unsafe IntPtr GetStandardHandle(StandardHandle standard_handle);

        [Flags]
        public enum AccesRights : uint
        {
            GenericRead = 0x80000000,
            GenericWrite = 0x40000000
        }

        [Flags]
        public enum FileShareMode : uint
        {
            Read = 1,
            Write = 2
        }

        public const uint CONSOLE_TEXTMODE_BUFFER = 1;

        [DllImport(Library, SetLastError = true)]
        public static extern unsafe IntPtr CreateConsoleScreenBuffer(
            AccesRights desiredAcces,
            FileShareMode shareMode,
            void* securityAttributes,
            uint flags,
            void* screenBufferData
        );

        [DllImport(Library, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetConsoleActiveScreenBuffer(
            IntPtr consoleOutputHandle
        );

        [Flags]
        public enum CharAttributes : ushort
        {
            ForegroundBlue = 1,
            ForegroundGreen = 2,
            ForegroundRed = 4,
            ForegroundIntensity = 8,
            BackgroundBlue = 16,
            BackgroundGreen = 32,
            BackgroundRed = 64,
            BackgroundIntensity = 128,
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 4)]
        public struct CharInfo
        {
            [FieldOffset(0)] public short UnicodeChar;
            [FieldOffset(0)] public sbyte AsciiChar;
            [FieldOffset(2)] public CharAttributes Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short x, short y) => (X, Y) = (x, y);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        [DllImport(Library, EntryPoint = "WriteConsoleOutputW", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern unsafe bool WriteConsoleOutput(
            IntPtr consoleOutputHandle,
            CharInfo* buffer,
            Coord bufferSize,
            Coord bufferCoord,
            SmallRect* writeRegion
        );
    }
}