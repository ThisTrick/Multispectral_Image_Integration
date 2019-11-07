using System.Drawing;

namespace Multispectral_Image_Integration
{
    public class InterlacingMaximumMethod
    {
        public InterlacingMaximumMethod(Bitmap imageRGB, Bitmap imageTEMP,
                             out Bitmap result1, out Bitmap result2)
        {
            if (imageRGB != null && imageTEMP != null)
            {
                var width = imageTEMP.Width;
                var heigth = imageTEMP.Height;
                result1 = (Bitmap)imageRGB.Clone();
                result2 = (Bitmap)imageRGB.Clone();

                for (var x = 0; x < width; x += 1)
                {
                    for (var y = 0; y < heigth; y += 2)
                    {
                        Color pixelTEMP = imageTEMP.GetPixel(x, y);
                        Color pixelRGB = imageRGB.GetPixel(x, y);

                        //extract ARGB value from pixel
                        var rTEMP = pixelTEMP.R;
                        var gTEMP = pixelTEMP.G;
                        var bTEMP = pixelTEMP.B;
                        var aRGB = pixelRGB.A;
                        var rRGB = pixelRGB.R;
                        var gRGB = pixelRGB.G;
                        var bRGB = pixelRGB.B;
                        var gTEMPNew = (gTEMP - bTEMP) < 0 ? 0 : (gTEMP - bTEMP);
                        var r = rTEMP > rRGB ? rTEMP : rRGB;
                        var g = gTEMPNew > gRGB ? gTEMPNew : gRGB;

                        result1.SetPixel(x, y, Color.FromArgb(aRGB, r, gRGB, bRGB));
                        result2.SetPixel(x, y, Color.FromArgb(aRGB, r, g, bRGB));
                    }
                }
            }
            else { result1 = null; result2 = null; }
        }
    }
}
