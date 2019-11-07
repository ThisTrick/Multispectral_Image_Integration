using System.Drawing;

namespace Multispectral_Image_Integration
{
    public class InterlacingMethod
    {
        public InterlacingMethod(Bitmap imageRGB, Bitmap imageTEMP,
                             out Bitmap result1, out Bitmap result2)
        {
            if (imageRGB != null && imageTEMP != null)
            {
                var width = imageTEMP.Width;
                var heigth = imageTEMP.Height;
                result1 = (Bitmap)imageRGB.Clone();
                result2 = (Bitmap)imageRGB.Clone();

                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < heigth; y += 2)
                    {
                        Color pixelTEMP2 = imageTEMP.GetPixel(x, y);
                        Color pixelRGB2 = imageRGB.GetPixel(x, y);

                        //extract ARGB value from pixel
                        var rTEMP = pixelTEMP2.R;
                        var gTEMP = pixelTEMP2.G;
                        var bTEMP = pixelTEMP2.B;
                        var aRGB = pixelRGB2.A;
                        var gRGB = pixelRGB2.G;
                        var bRGB = pixelRGB2.B;
                        var gTEMPNew = (gTEMP - bTEMP) < 0 ? 0 : (gTEMP - bTEMP);

                        result1.SetPixel(x, y, Color.FromArgb(aRGB, rTEMP, gRGB, bRGB));
                        result2.SetPixel(x, y, Color.FromArgb(aRGB, rTEMP, gTEMPNew, bRGB));
                    }
                }
            }
            else { result1 = null; result2 = null; }
        }
    }
}
