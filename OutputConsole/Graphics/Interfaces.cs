﻿using OutputConsole.Extern;

using System;
using System.Runtime.InteropServices.WindowsRuntime;

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

    public interface ITextRenderTarget
    {
        void SetTarget(IImage image);

        void Append(string text);
    }

    public interface IContextDescriptor
    {

    }

    public interface IContext
    {
        IntPtr Handle { get; }

        bool Create(IContextDescriptor context_descriptor);

        void SetSource(IImage image);

        void SetViewPort(Kernel.SmallRect source_rect, Kernel.SmallRect destination_rect);

        bool Set();

        bool Present();
    }
}
