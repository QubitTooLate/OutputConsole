using OutputConsole.Extern;

using System;

namespace OutputConsole.Graphics
{
    public struct ConsoleContextDescriptor : IContextDescriptor
    {
        public Kernel.AccesRights AccesRights;
        public Kernel.FileShareMode ShareMode;

        public static ConsoleContextDescriptor Default = new ConsoleContextDescriptor
        {
            AccesRights = Kernel.AccesRights.GenericWrite,
            ShareMode = Kernel.FileShareMode.Read | Kernel.FileShareMode.Write
        };
    }

    public class ConsoleContext : IContext
    {
        private IntPtr _handle;
        private IImage _backBuffer;
        private Kernel.Coord _backBufferPartCoord;
        private Kernel.Coord _backBufferPartSize;
        private Kernel.SmallRect _viewPortRect;

        public IntPtr Handle => _handle;

        public bool Create(IContextDescriptor console_context_descriptor)
        {
            var desc = (ConsoleContextDescriptor)console_context_descriptor;

            unsafe
            {
                _handle = Kernel.CreateConsoleContext(
                    desc.AccesRights,
                    desc.ShareMode,
                    null,
                    Kernel.CONSOLE_TEXTMODE_BUFFER,
                    null
                );

                return _handle.ToInt32() > 0;
            }
        }

        public bool Present()
        {
            unsafe
            {
                fixed (Kernel.CharInfo* charInfos = _backBuffer.CharInfos)
                fixed (Kernel.SmallRect* viewPortRect = &_viewPortRect)
                {
                    return Kernel.WriteToConsoleContext(
                        _handle,
                        charInfos,
                        _backBufferPartSize,
                        _backBufferPartCoord,
                        viewPortRect
                    );
                }
            }
        }

        public bool Set()
        {
            return Kernel.SetConsoleContext(_handle);
        }

        public void SetSource(IImage image)
        {
            _backBuffer = image;
        }

        public void SetViewPort(Kernel.SmallRect source_rect, Kernel.SmallRect destination_rect)
        {
            _backBufferPartCoord.X = source_rect.Left;
            _backBufferPartCoord.Y = source_rect.Top;
            _backBufferPartSize.X = (short)(source_rect.Right - source_rect.Left);
            _backBufferPartSize.Y = (short)(source_rect.Bottom - source_rect.Top);

            _viewPortRect = destination_rect;
        }

        public void SetViewPort()
        {
            _backBufferPartSize = new Kernel.Coord(_backBuffer.Width, _backBuffer.Height);

            _viewPortRect = new Kernel.SmallRect((short)0, (short)0, _backBuffer.Width, _backBuffer.Height);
        }
    }
}
