using System;

namespace Multispectral_Image_Integration_Library
{
    public class AveragingMethodFusion : IImageFusion
    {
        public FastBitmap Fusion(FastBitmap img1, FastBitmap img2)
        {
            if (img1.Width != img2.Width && img2.Height != img2.Height)
            {
                throw new ArgumentException("Изображения должны быть одного размера");
            }
            var imgResult = img1.Clone();
            for (int x = 0; x < imgResult.Width; x++)
            {
                for (int y = 0; y < imgResult.Height; y++)
                {
                    for (int color = 0; color < imgResult.DimensionsColor; color++)
                    {
                        imgResult[x, y, color] = (byte)((img1[x, y, color] + img2[x, y, color]) / 2);
                    }
                }
            }
            return imgResult;
        }
    }
}
