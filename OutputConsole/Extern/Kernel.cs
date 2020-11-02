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
        public static extern unsafe IntPtr GetStandardConsoleContext(StandardHandle standard_handle);

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

        [DllImport(Library, EntryPoint = "CreateConsoleScreenBuffer", SetLastError = true)]
        public static extern unsafe IntPtr CreateConsoleContext(
            AccesRights desired_acces,
            FileShareMode share_mode,
            void* security_attributes,
            uint flags,
            void* screen_buffer_data
        );

        [DllImport(Library, EntryPoint = "SetConsoleActiveScreenBuffer", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetConsoleContext(
            IntPtr console_output_handle
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

            public CharInfo(short unicode_char, CharAttributes char_attributes) =>
                (AsciiChar, UnicodeChar, Attributes) = (0, unicode_char, char_attributes);

            public CharInfo(sbyte ascii_har, CharAttributes char_attributes) =>
                (UnicodeChar, AsciiChar, Attributes) = (0, ascii_har, char_attributes);
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

            public SmallRect(short left, short top, short right, short bottom) =>
                (Left, Top, Right, Bottom) = (left, top, right, bottom);

            public SmallRect(int x, int y, int width, int height) =>
                (Left, Top, Right, Bottom) = ((short)x, (short)y, (short)(x + width), (short)(y + height));
        }

        [DllImport(Library, EntryPoint = "WriteConsoleOutputW", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern unsafe bool WriteToConsoleContext(
            IntPtr console_output_handle,
            CharInfo* buffer,
            Coord buffer_size,
            Coord buffer_coord,
            SmallRect* write_region
        );
    }
}