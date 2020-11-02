using OutputConsole.Extern;
using OutputConsole.Graphics;

using System;

namespace OutputConsole
{
    public static class Program
    {
        public static unsafe void Main(string[] args)
        {
            var image = new TextImage();

            image.CharInfos = new Kernel.CharInfo[]
            {
                new Kernel.CharInfo
                {
                    UnicodeChar = (short)'H',
                    Attributes =
                        Kernel.CharAttributes.ForegroundRed |
                        Kernel.CharAttributes.ForegroundGreen |
                        Kernel.CharAttributes.ForegroundBlue |
                        Kernel.CharAttributes.ForegroundIntensity
                },
                new Kernel.CharInfo
                {
                    UnicodeChar = (short)'I',
                    Attributes =
                        Kernel.CharAttributes.BackgroundRed |
                        Kernel.CharAttributes.BackgroundGreen |
                        Kernel.CharAttributes.BackgroundBlue |
                        Kernel.CharAttributes.BackgroundIntensity
                }
            };

            image.Size = new Kernel.Coord(2, 1);

            var backBuffer = new TextImage();

            backBuffer.CharInfos = new Kernel.CharInfo[100 * 100];

            /*for (int i = 0; i < backBuffer.CharInfos.Length; i++)
            {
                backBuffer.CharInfos[i] =
                new Kernel.CharInfo
                {
                    UnicodeChar = (short)' ',
                    Attributes =
                        Kernel.CharAttributes.BackgroundRed |
                        Kernel.CharAttributes.BackgroundGreen |
                        Kernel.CharAttributes.BackgroundBlue |
                        Kernel.CharAttributes.BackgroundIntensity
                };
            }*/

            backBuffer.Size = new Kernel.Coord(100, 100);

            var renderer = new RenderTarget();

            renderer.SetTarget(backBuffer);
            renderer.DrawImage(image);

            var context = new ConsoleContext();

            context.Create(ConsoleContextDescriptor.Default);
            context.SetSource(backBuffer);
            context.SetViewPort(
                new Kernel.SmallRect((short)0, (short)0, backBuffer.Size.X, backBuffer.Size.Y),
                new Kernel.SmallRect((short)0, (short)0, backBuffer.Size.X, backBuffer.Size.Y)
            );
            context.Set();
            context.Present();

            Console.ReadKey();






            /*

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
            */
        }
    }
}
