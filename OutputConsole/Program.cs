using OutputConsole.Extern;
using OutputConsole.Graphics;

using System;

namespace OutputConsole
{
    public static class Program
    {
        public static unsafe void Main(string[] args)
        {
            var image = new Image(
                new Kernel.CharInfo[]
                {
                    new Kernel.CharInfo
                    {
                        UnicodeChar = (short)'H',
                        Attributes =
                            Kernel.CharAttributes.ForegroundRed |
                            Kernel.CharAttributes.ForegroundBlue |
                            Kernel.CharAttributes.ForegroundIntensity
                    },
                    new Kernel.CharInfo
                    {
                        UnicodeChar = (short)'I',
                        Attributes =
                            Kernel.CharAttributes.BackgroundRed |
                            Kernel.CharAttributes.BackgroundBlue |
                            Kernel.CharAttributes.BackgroundIntensity
                    }
                },
                2,
                1
            );

            var backBuffer = new Image(100, 100);

            var renderer = new RenderTarget();

            renderer.SetTarget(backBuffer);
            renderer.DrawImage(image);

            var context = new ConsoleContext();

            context.Create(ConsoleContextDescriptor.Default);
            context.SetSource(backBuffer);
            context.SetViewPort();
            context.Set();
            context.Present();

            Console.ReadKey();
        }
    }
}
