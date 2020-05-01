using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Multispectral_Image_Integration_Library
{
    public class FastBitmap
    {
        private Bitmap bitmap;
        private int bytesPerPixel;
        private int byteCount;
        private byte[] pixels;
        private IntPtr ptrFirstPixel;
        private int heightInPixels;
        private int widthInBytes;

        public Bitmap Bitmap { get => GetBitmap(bitmap); private set => bitmap = SetBitmap(value); }
        public byte[,,] Data { get; set; }

        public FastBitmap(Bitmap bitmap)
        {
            Data = new byte[bitmap.Width, bitmap.Height, 3];
            this.Bitmap = bitmap;
        }
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

        public byte this[int x, int y, int color]
        {
            get => Data[x, y, color];
            set => Data[x, y, color] = value;
        }
    }
}
