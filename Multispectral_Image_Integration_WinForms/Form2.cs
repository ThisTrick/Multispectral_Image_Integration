using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Multispectral_Image_Integration_WinForms
{
    public partial class Form2 : Form
    {

        //TODO: Оценка сегментации

        Image<Bgr, Byte> imgRGB;
        Image<Bgr, Byte> imgTEMP;
        Image<Bgr, Byte> imgReal;
        Image<Bgr, Byte> imgResultColor;
        Image<Gray, Byte> imgResultGray;
        Image<Gray, Byte> imgBinarizeReal;
        Image<Gray, Byte> imgBinarizeTEMP;
        Image<Gray, Byte> imgBinarizeResultColor;
        Image<Gray, Byte> imgBinarizeResultGray;

        int iterator = 1;

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);
        public Form2()
        {
            InitializeComponent();

        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyData == Keys.Q)
            {
                Button4_Click(this, new EventArgs());
            }
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void ImageRGB_Click(object sender, EventArgs e)
        {
            imgRGB = ImageLoad(pictureRGB);
        }

        private void ImageTEMP_Click(object sender, EventArgs e)
        {
            imgTEMP = ImageLoad(pictureTEMP);
        }
        private void PictureResult1_Click(object sender, EventArgs e)
        {
            Save(pictureResult1);
        }

        private void PictureResult2_Click(object sender, EventArgs e)
        {
            Save(pictureResult2);
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            //Задает изображения для теста(по кругу)
            if (iterator == 1)
            {
                imgRGB = new Image<Bgr, Byte>(Resource1.TestRGB_1);
                imgTEMP = new Image<Bgr, Byte>(Resource1.TestTEMP_1);
                imgReal = new Image<Bgr, byte>(Resource1.TestReal_1);
                iterator++;
            }
            else if (iterator == 2)
            {
                imgRGB = new Image<Bgr, Byte>(Resource1.TestRGB_2);
                imgTEMP = new Image<Bgr, Byte>(Resource1.TestTEMP_2);
                imgReal = new Image<Bgr, byte>(Resource1.TestReal_2);
                iterator++;
            }
            else
            {
                imgRGB = new Image<Bgr, Byte>(Resource1.TestRGB_3);
                imgTEMP = new Image<Bgr, Byte>(Resource1.TestTEMP_3);
                imgReal = new Image<Bgr, byte>(Resource1.TestReal_3);
                iterator = 1;
            }
            pictureRGB.Image = imgRGB.Bitmap;
            pictureTEMP.Image = imgTEMP.Bitmap;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            //MaximumMethod
            imgResultColor = MaximumMethodColor(imgRGB, imgTEMP);
            imgResultGray = MaximumMethodGray(imgRGB, imgTEMP);
            pictureResult1.Image = imgResultColor?.Bitmap;
            pictureResult2.Image = imgResultGray?.Bitmap;
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            //AveragingMethod
            imgResultColor =  AveragingMethodColor(imgRGB, imgTEMP);
            imgResultGray = AveragingMethodGray(imgRGB, imgTEMP);
            pictureResult1.Image = imgResultColor?.Bitmap;
            pictureResult2.Image = imgResultGray?.Bitmap;
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            //InterlacingBaseMethod
            imgResultColor = InterlacingMethod(imgRGB, imgTEMP);
            imgResultGray = InterlacingMethodGray(imgRGB, imgTEMP);
            pictureResult1.Image = imgResultColor?.Bitmap;
            pictureResult2.Image = imgResultGray?.Bitmap;
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            //InterlacingMaximumMethod
            imgResultColor = InterlacingMaximumMethod(imgRGB, imgTEMP);
            imgResultGray = InterlacingMaximumMethodGray(imgRGB, imgTEMP);
            pictureResult1.Image = imgResultColor?.Bitmap;
            pictureResult2.Image = imgResultGray?.Bitmap;
        }



        /// <summary>
        /// Сохраняет изображение в PictureBox.
        /// </summary>
        /// <param name="pictureBox">PictureBox</param>
        void Save(PictureBox pictureBox)
        {
            if (pictureBox.Image != null) //если в pictureBox есть изображение
            {
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                savedialog.OverwritePrompt = true;
                //отображать ли предупреждение, если пользователь указывает несуществующий путь
                savedialog.CheckPathExists = true;
                //список форматов файла, отображаемый в поле "Тип файла"
                savedialog.Filter = "Image Files(*.JPG)|*.JPG|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                //отображается ли кнопка "Справка" в диалоговом окне
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                {
                    try
                    {
                        pictureBox.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        /// <summary>
        /// Загружает изображение в PictureBox и переменную.
        /// </summary>
        /// <param name="image">Переменная изображения</param>
        /// <param name="pictureBox">PictureBox</param>
        Image<Bgr, Byte> ImageLoad(PictureBox pictureBox)
        {
            //создание диалогового окна "Открыть изображение", для сохранения изображения
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Открыть изображение";
            //отображать ли предупреждение, если пользователь указывает несуществующее название файла
            openDialog.CheckFileExists = true;
            //отображать ли предупреждение, если пользователь указывает несуществующий путь
            openDialog.CheckPathExists = true;
            //отображается ли кнопка "Справка" в диалоговом окне
            openDialog.ShowHelp = true;
            //список форматов файла, отображаемый в поле "Тип файла"
            openDialog.Filter = "Image Files(*.JPG)|*.JPG|Image Files(*.PNG)|*.PNG";
            if (openDialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
            {
                try
                {
                    //Получаем путь к файлу
                    var path = openDialog.FileName;
                    //Получаем изображение 
                    Image<Bgr, Byte> image = new Image<Bgr, Byte>(path);
                    //Загружаем изображение в PictureBox
                    pictureBox.Image = image.Bitmap;
                    return image;
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть изображение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;
        }


        Image<Bgr, Byte> MaximumMethodColor(Image<Bgr, Byte> imgRGB, Image<Bgr, Byte> imgTEMP)
        {
            if (imgRGB != null && imgTEMP != null)
            {
                if (imgRGB.Height == imgTEMP.Height && imgRGB.Width == imgTEMP.Width)
                {
                    imgResultColor = imgRGB.Clone();

                    for (var y = 0; y < imgRGB.Height; y++)
                    {
                        for (var x = 0; x < imgRGB.Width; x++)
                        {
                            int gRGB = imgRGB.Data[y, x, 1];
                            int rRGB = imgRGB.Data[y, x, 1];

                            int bTEMP = imgTEMP.Data[y, x, 0];
                            int gTEMP = imgTEMP.Data[y, x, 1];
                            int rTEMP = imgTEMP.Data[y, x, 2];

                            int gNew = (gTEMP - bTEMP) < 0 ? 0 : (gTEMP - bTEMP);

                            int r = rTEMP > rRGB ? rTEMP : rRGB;
                            int g = gNew > gRGB ? gNew : gRGB;

                            imgResultColor.Data[y, x, 2] = (byte)r;
                            imgResultColor.Data[y, x, 1] = (byte)g;
                        }
                    }
                    return imgResultColor;
                }
                else
                {
                    MessageBox.Show("Изображения должны быть одинакового размера", "Ошибка",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            else { return null; }

        }
        Image<Bgr, Byte> AveragingMethodColor(Image<Bgr, Byte> imgRGB, Image<Bgr, Byte> imgTEMP)
        {
            if (imgRGB != null && imgTEMP != null)
            {
                if (imgRGB.Height == imgTEMP.Height && imgRGB.Width == imgTEMP.Width)
                {
                    imgResultColor = imgRGB.Clone();

                    for (var y = 0; y < imgRGB.Height; y++)
                    {
                        for (var x = 0; x < imgRGB.Width; x++)
                        {
                            int gRGB = imgRGB.Data[y, x, 1];
                            int rRGB = imgRGB.Data[y, x, 1];

                            int bTEMP = imgTEMP.Data[y, x, 0];
                            int gTEMP = imgTEMP.Data[y, x, 1];
                            int rTEMP = imgTEMP.Data[y, x, 2];

                            int gNew = (gTEMP - bTEMP) < 0 ? 0 : (gTEMP - bTEMP);

                            int r = (rTEMP + rRGB) / 2;
                            int g = (gNew + gRGB) / 2;

                            imgResultColor.Data[y, x, 2] = (byte)r;
                            imgResultColor.Data[y, x, 1] = (byte)g;
                           
                        }
                    }
                    return imgResultColor;
                }
                else
                {
                    MessageBox.Show("Изображения должны быть одинакового размера", "Ошибка",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            else { return null; }
        }
        Image<Gray, Byte> MaximumMethodGray(Image<Bgr, Byte> imgRGB, Image<Bgr, Byte> imgTEMP)
        {
            if (imgRGB != null && imgTEMP != null)
            {
                if (imgRGB.Height == imgTEMP.Height && imgRGB.Width == imgTEMP.Width)
                {
                    Image<Gray, Byte> imgRGBGray = imgRGB.Convert<Gray, Byte>();
                    Image<Gray, Byte> imgTEMPGray = imgTEMP.Convert<Gray, Byte>();
                    imgResultGray = imgRGBGray.Clone();

                    for (var y = 0; y < imgRGBGray.Height; y++)
                    {
                        for (var x = 0; x < imgRGBGray.Width; x++)
                        {
                            int pixelRGB = imgRGBGray.Data[y, x, 0];
                            int pixelTENP = imgTEMPGray.Data[y, x, 0];
                            if (pixelTENP > pixelRGB)
                            {
                                imgResultGray.Data[y, x, 0] = (byte)pixelTENP;
                            }
                        }
                    }
                    return imgResultGray;
                }
                else
                {
                    MessageBox.Show("Изображения должны быть одинакового размера", "Ошибка",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            else { return null; }

        }
        Image<Gray, Byte> AveragingMethodGray(Image<Bgr, Byte> imgRGB, Image<Bgr, Byte> imgTEMP)
        {
            if (imgRGB != null && imgTEMP != null)
            {
                if (imgRGB.Height == imgTEMP.Height && imgRGB.Width == imgTEMP.Width)
                {
                    Image<Gray, Byte> imgRGBGray = imgRGB.Convert<Gray, Byte>();
                    Image<Gray, Byte> imgTEMPGray = imgTEMP.Convert<Gray, Byte>();
                    imgResultGray = imgRGBGray.Clone();

                    for (var y = 0; y < imgRGBGray.Height; y++)
                    {
                        for (var x = 0; x < imgRGBGray.Width; x++)
                        {
                            int pixelRGB = imgRGBGray.Data[y, x, 0];
                            int pixelTEMP = imgTEMPGray.Data[y, x, 0];


                            int pixel = (pixelRGB + pixelTEMP) / 2;

                            imgResultGray.Data[y, x, 0] = (byte)pixel;
                        }
                    }
                    return imgResultGray;
                }
                else
                {
                    MessageBox.Show("Изображения должны быть одинакового размера", "Ошибка",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            else { return null; }

        }
        Image<Bgr, Byte> InterlacingMethod(Image<Bgr, Byte> imgRGB, Image<Bgr, Byte> imgTEMP)
        {
            if (imgRGB != null && imgTEMP != null)
            {
                if (imgRGB.Height == imgTEMP.Height && imgRGB.Width == imgTEMP.Width)
                {
                    imgResultColor = imgRGB.Clone();

                    for (var x = 0; x < imgRGB.Width; x++)
                    {
                        for (var y = 0; y < imgRGB.Height; y += 2)
                        {
                            int bTEMP = imgTEMP.Data[y, x, 0];
                            int gTEMP = imgTEMP.Data[y, x, 1];
                            int rTEMP = imgTEMP.Data[y, x, 2];

                            int gNew = (gTEMP - bTEMP) < 0 ? 0 : (gTEMP - bTEMP);

                            imgResultColor.Data[y, x, 2] = (byte)rTEMP;
                            imgResultColor.Data[y, x, 1] = (byte)gNew;
                        }
                    }
                    return imgResultColor;
                }
                else
                {
                    MessageBox.Show("Изображения должны быть одинакового размера", "Ошибка",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            else { return null; }
        }
        Image<Bgr, Byte> InterlacingMaximumMethod(Image<Bgr, Byte> imgRGB, Image<Bgr, Byte> imgTEMP)
        {
            if (imgRGB != null && imgTEMP != null)
            {
                if (imgRGB.Height == imgTEMP.Height && imgRGB.Width == imgTEMP.Width)
                {
                    imgResultColor = imgRGB.Clone();

                    for (var x = 0; x < imgRGB.Width; x++)
                    {
                        for (var y = 0; y < imgRGB.Height; y += 2)
                        {



                            int gRGB = imgRGB.Data[y, x, 1];
                            int rRGB = imgRGB.Data[y, x, 1];

                            int bTEMP = imgTEMP.Data[y, x, 0];
                            int gTEMP = imgTEMP.Data[y, x, 1];
                            int rTEMP = imgTEMP.Data[y, x, 2];

                            int gNew = (gTEMP - bTEMP) < 0 ? 0 : (gTEMP - bTEMP);

                            var r = rTEMP > rRGB ? rTEMP : rRGB;
                            var g = gNew > gRGB ? gNew : gRGB;

                            imgResultColor.Data[y, x, 2] = (byte)r;
                            imgResultColor.Data[y, x, 1] = (byte)g;

                        }
                    }
                    return imgResultColor;
                }
                else
                {
                    MessageBox.Show("Изображения должны быть одинакового размера", "Ошибка",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            else { return null; }
        }
        Image<Gray, Byte> InterlacingMethodGray(Image<Bgr, Byte> imgRGB, Image<Bgr, Byte> imgTEMP)
        {
            if (imgRGB != null && imgTEMP != null)
            {
                if (imgRGB.Height == imgTEMP.Height && imgRGB.Width == imgTEMP.Width)
                {
                    Image<Gray, Byte> imgRGBGray = imgRGB.Convert<Gray, Byte>();
                    Image<Gray, Byte> imgTEMPGray = imgTEMP.Convert<Gray, Byte>();
                    imgResultGray = imgRGBGray.Clone();

                    for (var x = 0; x < imgRGB.Width; x++)
                    {
                        for (var y = 0; y < imgRGB.Height; y += 2)
                        {
                            int pixelTEMP = imgTEMPGray.Data[y, x, 0];

                            imgResultGray.Data[y, x, 0] = (byte)pixelTEMP;
                        }
                    }
                    return imgResultGray;
                }
                else
                {
                    MessageBox.Show("Изображения должны быть одинакового размера", "Ошибка",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            else { return null; }
        }
        Image<Gray, Byte> InterlacingMaximumMethodGray(Image<Bgr, Byte> imgRGB, Image<Bgr, Byte> imgTEMP)
        {
            if (imgRGB != null && imgTEMP != null)
            {
                if (imgRGB.Height == imgTEMP.Height && imgRGB.Width == imgTEMP.Width)
                {
                    Image<Gray, Byte> imgRGBGray = imgRGB.Convert<Gray, Byte>();
                    Image<Gray, Byte> imgTEMPGray = imgTEMP.Convert<Gray, Byte>();
                    imgResultGray = imgRGBGray.Clone();

                    for (var x = 0; x < imgRGBGray.Width; x++)
                    {
                        for (var y = 0; y < imgRGBGray.Height; y += 2)
                        {



                            int pixelRGB = imgRGBGray.Data[y, x, 0];
                            int pixelTEMP = imgTEMPGray.Data[y, x, 0];

                            if(pixelTEMP > pixelRGB)
                            {
                                imgResultGray.Data[y, x, 0] = (byte)pixelTEMP;
                            }
                        }
                    }
                    return imgResultGray;
                }
                else
                {
                    MessageBox.Show("Изображения должны быть одинакового размера", "Ошибка",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            else { return null; }
        }
        private void buttonSegmentation_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1343, 600);
            label.Visible = true;
            trackBar1.Visible = true;
            label.Text = trackBar1.Value.ToString();

            imgBinarizeReal = BinarySegmentation(imgReal, pictureSegmenReal);
            imgBinarizeTEMP = BinarySegmentation(imgTEMP, pictureSegmenTEMP);
            imgBinarizeResultColor = BinarySegmentation(imgResultColor, pictureSegmenResult1);
            imgBinarizeResultGray = BinarySegmentation(imgResultGray, pictureSegmenResult2);

            SegmentationAssessment(imgBinarizeReal, labelRealM1White, labelRealM1Black, labelRealM2White, labelRealM2Black);
            SegmentationAssessment(imgBinarizeTEMP, labelTempM1White, labelTempM1Black, labelTempM2White, labelTempM2Black);
            SegmentationAssessment(imgBinarizeResultColor, labelResult1M1White, labelResult1M1Black, labelResult1M2White, labelResult1M2Black);
            SegmentationAssessment(imgBinarizeResultGray, labelResult2M1White, labelResult2M1Black, labelResult2M2White, labelResult2M2Black);
        }

        Image<Gray, Byte> BinarySegmentation(Image<Bgr, Byte> image, PictureBox pictureBox)
        {
            if (image != null)
            {
                Image<Gray, Byte> imgGray = image.Convert<Gray, Byte>();
                CvInvoke.Threshold(imgGray, imgGray, trackBar1.Value, 255, ThresholdType.Binary);
                pictureBox.Image = imgGray.Bitmap;
                return imgGray;
            }
            return null;
        }
        Image<Gray, Byte> BinarySegmentation(Image<Gray, Byte> image, PictureBox pictureBox)
        {
            if (image != null)
            {
                CvInvoke.Threshold(image, image, trackBar1.Value, 255, ThresholdType.Binary);
                pictureBox.Image = image.Bitmap;
                return image;
            }
            return null;
        }

        void SegmentationAssessment(Image<Gray, Byte> image, Label label1, Label label2, Label label3, Label label4)
        {
            if (image != null)
            {
                double totalPixels = image.Width * image.Height;

                double whitePixelsReal = 0;
                double blackPixelsReal = 0;

                double whitePixelsСountedСorrectly = 0;
                double blackPixelsСountedСorrectly = 0;

                double whitePixelsСountedMistake = 0;
                double blackPixelsСountedMistake = 0;

                double whiteM1, blackM1, whiteM2, blackM2;

                for (var y = 0; y < imgBinarizeReal.Height; y++)
                {
                    for (var x = 0; x < imgBinarizeReal.Width; x++)
                    {
                        int pixelReal = imgBinarizeReal.Data[y, x, 0];
                        int pixelImage = image.Data[y, x, 0];


                        if (pixelReal == 255)
                        {
                            whitePixelsReal += 1;
                        }
                        else
                        {
                            blackPixelsReal += 1;
                        }

                        if ((pixelReal == 255) && (pixelImage == 255))
                        {
                            whitePixelsСountedСorrectly += 1;
                        }
                        else if ((pixelReal == 0) && (pixelImage == 255))
                        {
                            whitePixelsСountedMistake += 1;
                        }
                        else if ((pixelReal == 0) && (pixelImage == 0))
                        {
                            blackPixelsСountedСorrectly += 1;
                        }
                        else if ((pixelReal == 255) && (pixelImage == 0))
                        {
                            blackPixelsСountedMistake += 1;
                        }

                    }
                }

                whiteM1 = Math.Abs(Math.Round((whitePixelsReal - whitePixelsСountedСorrectly) / whitePixelsReal * 100, 1));
                blackM1 = Math.Abs(Math.Round((blackPixelsReal - blackPixelsСountedСorrectly) / blackPixelsReal * 100, 1));
                whiteM2 = Math.Abs(Math.Round(whitePixelsСountedMistake / (totalPixels - whitePixelsReal) * 100, 1));
                blackM2 = Math.Abs(Math.Round(blackPixelsСountedMistake / (totalPixels - blackPixelsReal) * 100, 1));

                label1.Text = $"M1 = {whiteM1} %";
                label2.Text = $"M1 = {blackM1} %";
                label3.Text = $"M2 = {whiteM2} %";
                label4.Text = $"M2 = {blackM2} %";

            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            buttonSegmentation_Click(this, e);
        }
    }
}
