using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Multispectral_Image_Integration_Library
{
    /// <summary>
    /// Класс описывающий изображение.
    /// </summary>
    public class FastBitmap : ICloneable
    {
        private Bitmap bitmap;
        private int bytesPerPixel;
        private int byteCount;
        private byte[] pixels;
        private IntPtr ptrFirstPixel;
        private int heightInPixels;
        private int widthInBytes;

        /// <summary>
        /// Свойство для объекта Bitmap изображения.
        /// </summary>
        public Bitmap Bitmap { get => GetBitmap(bitmap); private set => bitmap = SetBitmap(value); }
        /// <summary>
        /// Массив значений интенсивностей пикселей.
        /// </summary>
        public byte[,,] Data { get; set; }
        /// <summary>
        /// Ширина изображения.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Высота изображения.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Количество цветовых измерений
        /// </summary>
        public int DimensionsColor { get; private set; }
        /// <summary>
        /// Флаг говорящей о цветовой гамме изображения.
        /// true - черно-белое, false - цветное.
        /// </summary>
        public bool IsGray { get; private set; }
        /// <summary>
        /// Основной конструктор класса.
        /// </summary>
        /// <param name="bitmap">Базовый объект Bitmap</param>
        public FastBitmap(Bitmap bitmap, bool isGray = false)
        {
            this.Data = new byte[bitmap.Width, bitmap.Height, 3];
            this.Bitmap = bitmap;
            this.Width = bitmap.Width;
            this.Height = bitmap.Height;
            this.IsGray = isGray;
            this.DimensionsColor = 3;
        }
        public FastBitmap() { }
        /// <summary>
        /// Быстрое преобразование Bitmap в byte[,,]
        /// </summary>
        /// <param name="processedBitmap">Базовый объект Bitmap</param>
        /// <returns>Не измененный базовый объект Bitmap </returns>
        private Bitmap SetBitmap(Bitmap processedBitmap)
        {
            BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);
            bytesPerPixel = Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
            byteCount = bitmapData.Stride * processedBitmap.Height;
            pixels = new byte[byteCount];
            ptrFirstPixel = bitmapData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);

            // Processing
            heightInPixels = bitmapData.Height;
            widthInBytes = bitmapData.Width * bytesPerPixel;
            int _y = 0;
            for (int y = 0; y < heightInPixels; y++)
            {
                int _x = 0;
                int CurrentLine = y * bitmapData.Stride;
                for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                {
                    Data[_x, _y, 2] = pixels[CurrentLine + x];
                    Data[_x, _y, 1] = pixels[CurrentLine + x + 1];
                    Data[_x, _y, 0] = pixels[CurrentLine + x + 2];
                    _x++;
                }
                _y++;
            }
            processedBitmap.UnlockBits(bitmapData);
            return processedBitmap;
        }
        /// <summary>
        /// Быстрое преобразование byte[,,] в Bitmap
        /// </summary>
        /// <param name="processedBitmap">Базовый объект Bitmap</param>
        /// <returns>Готовый объект Bitmap</returns>
        private Bitmap GetBitmap(Bitmap processedBitmap)
        {
            // Bitmap To Array
            BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

            // Processing
            int _y = 0;
            for (int y = 0; y < heightInPixels; y++)
            {
                int _x = 0;
                int CurrentLine = y * bitmapData.Stride;
                for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                {
                    pixels[CurrentLine + x] = Data[_x, _y, 2];
                    pixels[CurrentLine + x + 1] = Data[_x, _y, 1];
                    pixels[CurrentLine + x + 2] = Data[_x, _y, 0];
                    _x++;
                }
                _y++;
            }
            // Array To Bitmap
            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            processedBitmap.UnlockBits(bitmapData);
            return processedBitmap;
        }
        /// <summary>
        /// Преобразование цветного изображения в черно-белое.
        /// </summary>
        public void ToGray()
        {
            for(var x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    byte grayPixel = (byte)(0.3 * Data[x, y, 0] + 0.59 * Data[x, y, 1] + 0.11 * Data[x, y, 2]);
                    Data[x, y, 0] = grayPixel;
                    Data[x, y, 1] = grayPixel;
                    Data[x, y, 2] = grayPixel;
                }
            }
            this.IsGray = true;
        }
        /// <summary>
        /// Создает копию объекта с типом FastBitmap 
        /// </summary>
        /// <returns></returns>
        public FastBitmap Clone()
        {
            /// Реализация какая-то кривая нужно попытаться переделать 
            var newFastBitmap = new FastBitmap((Bitmap)bitmap.Clone(), this.IsGray);
            newFastBitmap.Data = Data.Clone() as byte[,,];
            return newFastBitmap;
        }
        /// <summary>
        /// Создает копию объекта с типом Object
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        /// <summary>
        /// Быстрый доступ к пикселям изображения.
        /// </summary>
        /// <param name="x">Координата x изображения</param>
        /// <param name="y">Координата y изображения</param>
        /// <param name="color">Цвет пикселя (red - 0, blue - 1, green - 2)</param>
        /// <returns>Интенсивность пикселя</returns>
        public byte this[int x, int y, int color]
        {
            get => Data[x, y, color];
            set => Data[x, y, color] = value;
        }
    }
}
