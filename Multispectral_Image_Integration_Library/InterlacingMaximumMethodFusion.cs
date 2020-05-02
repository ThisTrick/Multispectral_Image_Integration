using System;

namespace Multispectral_Image_Integration_Library
{
    /// <summary>
    /// Метод слияния двух изображений основанный на том что в каждую n+1 строку добавляются пиксели максимальной интенсивности.
    /// Где строка n - строка изображения img1, строка n+1 - строка изображения img2.
    /// </summary>
    public class InterlacingMaximumMethodFusion : IImageFusion
    {
        public FastBitmap Fusion(FastBitmap img1, FastBitmap img2)
        {
            if (img1.Width != img2.Width || img2.Height != img2.Height)
            {
                throw new ArgumentException("Изображения должны быть одного размера");
            }
            var imgResult = img1.Clone();
            for (int x = 0; x < imgResult.Width; x++)
            {
                for (int y = 0; y < imgResult.Height; y += 2)
                {
                    for (int color = 0; color < imgResult.DimensionsColor; color++)
                    {
                        imgResult[x, y, color] = img1[x, y, color] > img2[x, y, color] ? img1[x, y, color] : img2[x, y, color];
                    }
                }
            }
            return imgResult;
        }
    }
}
