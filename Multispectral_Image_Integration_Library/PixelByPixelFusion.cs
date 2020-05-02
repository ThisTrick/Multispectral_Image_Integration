using System;

namespace Multispectral_Image_Integration_Library
{
    /// <summary>
    /// Класс реализующий попиксельное слияние двух изображений с выбором метода слияния.
    /// </summary>
    public class PixelByPixelFusion
    {
        /// <summary>
        /// Метод реализующий попиксельное слияние двух изображений с выбором метода слияния.
        /// </summary>
        /// <param name="img1">Первое изображение</param>
        /// <param name="img2">Второе изображение</param>
        /// <param name="method">Функция что реализует особенность метода</param>
        /// <returns></returns>
        protected FastBitmap PixelFusion(FastBitmap img1, FastBitmap img2, Func<byte, byte, byte> method, int tabOrderY = 1)
        {
            if (img1.Width != img2.Width || img2.Height != img2.Height)
            {
                throw new ArgumentException("Изображения должны быть одного размера");
            }
            var imgResult = img1.Clone();
            for (int x = 0; x < imgResult.Width; x++)
            {
                for (int y = 0; y < imgResult.Height; y += tabOrderY)
                {
                    for (int color = 0; color < imgResult.DimensionsColor; color++)
                    {
                        imgResult[x, y, color] = method(img1[x, y, color], img2[x, y, color]);
                    }
                }
            }
            return imgResult;
        }
    }
}
