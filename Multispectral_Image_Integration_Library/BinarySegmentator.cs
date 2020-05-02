namespace Multispectral_Image_Integration_Library
{
    /// <summary>
    /// Класс с реализацией бинарной пороговой сегментации. 
    /// </summary>
    public class BinarySegmentator
    {
        /// <summary>
        /// Бинарная пороговая сегментация. 
        /// </summary>
        /// <param name="img">Объект изображения типа FastBitmap</param>
        /// <param name="threshold">Значение порога</param>
        /// <param name="maxValue">Максимальная интенсивность изображения после сегментации</param>
        /// <returns>Результат бинарной сегментации</returns>
        public FastBitmap Segmentation(FastBitmap img, byte threshold, byte maxValue)
        {
            var imgResult = img.Clone();
            if (!imgResult.IsGray)
            {
                imgResult.ToGray();
            }
            for (int x = 0; x < imgResult.Width; x++)
            {
                for (int y = 0; y < imgResult.Height; y++)
                {
                    byte pixelValue = imgResult[x, y, 0] > threshold ? (byte)0 : maxValue;
                    for (int color = 0; color < imgResult.DimensionsColor; color++)
                    {
                        imgResult[x, y, color] = pixelValue;
                    }
                }
            }
            return imgResult;
        }
    }
}
