using OutputConsole.Extern;

using System;

namespace OutputConsole.Graphics
{
    public class Image : IImage
    {
        public Kernel.CharInfo[] CharInfos { get; }
        public short Width { get; }
        public short Height { get; }

        public Image(Kernel.CharInfo[] char_infos, short width, short height) =>
            (CharInfos, Width, Height) = (char_infos, width, height);

        public Image(short width, short height) =>
            (CharInfos, Width, Height) = (new Kernel.CharInfo[width * height], width, height);
    }
}
