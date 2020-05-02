using System;

namespace Multispectral_Image_Integration_Library
{
    /// <summary>
    /// Метод слияния двух изображений основанный на усреднении интенсивности пикселей первого изображения с соответствующими пикселями второго изображения.
    /// </summary>
    public class AveragingMethodFusion : PixelByPixelFusion, IImageFusion
    {
        public FastBitmap Fusion(FastBitmap img1, FastBitmap img2)
        {
            Func<byte, byte, byte> method = (pixel1, pixel2) => (byte)((pixel1 + pixel2)/2);
            return PixelFusion(img1, img2, method);
        }
    }
}
