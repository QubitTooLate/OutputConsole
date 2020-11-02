

namespace OutputConsole.Graphics
{
    public class RenderTarget : IRenderTarget
    {
        protected IImage _image;

        public void DrawImage(IImage image)
        {
            var source = image.CharInfos;
            var width = image.Size.X;
            var height = image.Size.Y;

            var back = _image.CharInfos;
            var backWidth = _image.Size.X;
            var backHeight = _image.Size.Y;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var destinationX = 0 + x;
                    var destinationY = 0 + y;
                    var destinationIndex = destinationX + (destinationY * backWidth);

                    if ((destinationX >= 0) && (destinationX < backWidth) && (destinationY >= 0) && (destinationY < backHeight))
                    {
                        back[destinationIndex] = source[x + (y * width)];
                    }
                }
            }
        }

        public void SetTarget(IImage image)
        {
            _image = image;
        }
    }
}
