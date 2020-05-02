using System;

namespace Multispectral_Image_Integration_Library
{
    /// <summary>
    /// Метод слияния двух изображений основанный на том что в каждую n+1 строку добавляются пиксели максимальной интенсивности.
    /// Где строка n - строка изображения img1, строка n+1 - строка изображения img2.
    /// </summary>
    public class InterlacingMaximumMethodFusion : PixelByPixelFusion, IImageFusion
    {
        public FastBitmap Fusion(FastBitmap img1, FastBitmap img2)
        {
            Func<byte, byte, byte> method = (pixel1, pixel2) => pixel1 > pixel2 ? pixel1 : pixel2;
            return PixelFusion(img1, img2, method, 2);
        }
    }
}
