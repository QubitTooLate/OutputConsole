using OutputConsole.Extern;

using System;

namespace OutputConsole.Graphics
{
    public class TextImage : IImage
    {
        private Kernel.Coord _size;
        private Kernel.CharInfo[] _charInfos;

        public Kernel.Coord Size { get => _size; set => _size = value; }

        public Kernel.CharInfo[] CharInfos { get => _charInfos; set => _charInfos = value; }
    }
}
