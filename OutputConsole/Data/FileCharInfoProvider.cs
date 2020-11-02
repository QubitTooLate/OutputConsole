using OutputConsole.Extern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace OutputConsole.Data
{
    public class FileCharInfoProvider : ICharInfoProvider
    {
        private FileInfo _fileInfo;

        public Kernel.CharInfo[] CharInfos => ReadFile();

        public FileCharInfoProvider(string file)
        {
            _fileInfo = new FileInfo(file);
        }

        private Kernel.CharInfo[] ReadFile()
        {
            using var reader = _fileInfo.OpenText();

            Span<char> buffer = stackalloc char[256];

            using var writer = new MemoryStream();

            Span<Kernel.CharInfo> charInfoBuffer = stackalloc Kernel.CharInfo[1];
            Span<byte> charInfoBufferAsBytes = MemoryMarshal.AsBytes(charInfoBuffer);

            Kernel.CharAttributes charAttributes =
                Kernel.CharAttributes.ForegroundRed |
                Kernel.CharAttributes.ForegroundGreen |
                Kernel.CharAttributes.ForegroundBlue |
                Kernel.CharAttributes.ForegroundIntensity;

            while (!reader.EndOfStream)
            {
                int read = reader.ReadBlock(buffer);

                for (int i = 0; i < read; i++)
                {
                    if (buffer[i] == '~')
                    {
                        ++i;

                        int value = 0;

                        while (i < read && buffer[i] >= '0' && buffer[i] <= '9')
                        {
                            value *= 10;
                            value += buffer[i] - '0';

                            ++i;
                        }

                        charAttributes = (Kernel.CharAttributes)value;
                    }
                    else
                    {
                        charInfoBuffer[0].UnicodeChar = (short)buffer[i];
                        charInfoBuffer[0].Attributes = charAttributes;

                        writer.Write(charInfoBufferAsBytes);
                    }
                }
            }

            var result = writer.ToArray().AsSpan();

            return MemoryMarshal.Cast<byte, Kernel.CharInfo>(result).ToArray();
        }
    }
}
