using OutputConsole.Extern;

using System;

namespace OutputConsole.Graphics
{
    public interface IImage
    {
        Kernel.CharInfo[] CharInfos { get; set; }

        Kernel.Coord Size { get; set; }
    }

    public interface IRenderTarget
    {
        void SetTarget(IImage image);

        void DrawImage(IImage image);
    }

    public interface IContextDescriptor
    {

    }

    public interface IContext : IRenderTarget
    {
        IntPtr Handle { get; }

        bool Create(IContextDescriptor context_descriptor);

        bool Set();

        void SetViewPort(Kernel.SmallRect source_rect, Kernel.SmallRect destination_rect);

        bool Present();
    }
}
