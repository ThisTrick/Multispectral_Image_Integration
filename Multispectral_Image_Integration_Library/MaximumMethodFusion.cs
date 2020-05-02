using System;

namespace Multispectral_Image_Integration_Library
{
    /// <summary>
    /// Метод слияния двух изображений основанный на выборе пикселей максимальной интенсивности
    /// </summary>
    public class MaximumMethodFusion : PixelByPixelFusion , IImageFusion
    {
        public FastBitmap Fusion(FastBitmap img1, FastBitmap img2)
        {
            Func<byte, byte, byte> method = (pixel1, pixel2) => pixel1 > pixel2 ? pixel1 : pixel2;
            return PixelFusion(img1, img2, method);
        }
    }
}
