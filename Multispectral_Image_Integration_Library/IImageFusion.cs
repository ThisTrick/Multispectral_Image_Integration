namespace Multispectral_Image_Integration_Library
{
    /// <summary>
    /// Интерфейс классов для объедения изображений.
    /// </summary>
    public interface IImageFusion
    {
        /// <summary>
        /// Метод для слияния двух изображений типа FastBitmap
        /// </summary>
        /// <returns>Объеденное изображение типа FastBitmap</returns>
        FastBitmap Fusion(FastBitmap img1, FastBitmap img2);
    }
}
