using OutputConsole.Data;
using OutputConsole.Extern;
using OutputConsole.Graphics;

using System;
using System.IO;

namespace OutputConsole
{
    public static class Program
    {
        public static unsafe void Main(string[] args)
        {
            var provider = new FileCharInfoProvider(Path.Combine(Environment.CurrentDirectory, "Example/test.txt"));

            var image = new Image(provider.CharInfos, 30, 1);

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
