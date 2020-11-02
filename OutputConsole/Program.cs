using OutputConsole.Extern;
using System;

namespace OutputConsole
{
    public static class Program
    {
        public static unsafe void Main(string[] args)
        {
            IntPtr standardContextHandle = Kernel.GetStandardConsoleContext(Kernel.StandardHandle.Output);

            if (standardContextHandle.ToInt32() <= 0)
                return;

            IntPtr newContextHandle = Kernel.CreateConsoleContext(
                Kernel.AccesRights.GenericWrite,
                Kernel.FileShareMode.Read |
                Kernel.FileShareMode.Write,
                null,
                Kernel.CONSOLE_TEXTMODE_BUFFER,
                null
            );

            if (newContextHandle.ToInt32() <= 0)
                return;

            bool result = Kernel.SetConsoleContext(newContextHandle);

            if (!result)
                return;

            var text = "Hello, world!";
            var charInfos = stackalloc Kernel.CharInfo[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                charInfos[i] = new Kernel.CharInfo
                {
                    UnicodeChar = (short)text[i],
                    Attributes =
                        Kernel.CharAttributes.ForegroundRed |
                        Kernel.CharAttributes.ForegroundGreen |
                        Kernel.CharAttributes.ForegroundBlue |
                        Kernel.CharAttributes.ForegroundIntensity
                };
            }

            var outputRect = new Kernel.SmallRect(0, 0, text.Length, 0);

            result = Kernel.WriteToConsoleContext(
                newContextHandle,
                charInfos,
                new Kernel.Coord((short)text.Length, 1),
                new Kernel.Coord(0, 0),
                &outputRect
            );

            if (!result)
                return;

            Console.ReadKey();

            result = Kernel.SetConsoleContext(standardContextHandle);
        }
    }
}
