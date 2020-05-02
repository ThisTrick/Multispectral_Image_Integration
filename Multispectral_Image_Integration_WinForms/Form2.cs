using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Multispectral_Image_Integration_Library;


namespace Multispectral_Image_Integration_WinForms
{
    public partial class Form2 : Form
    {
        FastBitmap imgRGB;
        FastBitmap imgTEMP;
        FastBitmap imgReal;
        FastBitmap imgGray;
        FastBitmap imgTEMPGray;

        FastBitmap imgResultColor;
        FastBitmap imgResultGray;
        FastBitmap imgBinarizeReal;
        FastBitmap imgBinarizeTEMP;
        FastBitmap imgBinarizeResultColor;
        FastBitmap imgBinarizeResultGray;
        
        int iterator = 1;
        #region Перетаскивание окна мышью
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion
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

        private void ImageRGB_Click(object sender, EventArgs e)
        {
            imgRGB = ImageLoad(pictureRGB);
            imgGray = imgRGB.Clone();
            imgGray.ToGray();
        }

        private void ImageTEMP_Click(object sender, EventArgs e)
        {
            imgTEMP = ImageLoad(pictureTEMP);
            imgTEMPGray = imgTEMP.Clone();
            imgTEMPGray.ToGray();
        }
        private void PictureResult1_Click(object sender, EventArgs e)
        {
            Save(pictureResult1);
        }

        private void PictureResult2_Click(object sender, EventArgs e)
        {
            Save(pictureResult2);
        }
        /// <summary>
        /// Задает изображения для теста (по кругу).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, EventArgs e)
        {
            Bitmap bitmapRGB, bitmapTEMP, bitmapREAL; 
            if (iterator == 1)
            {
                bitmapRGB = Resource1.TestRGB_1;
                bitmapTEMP = Resource1.TestTEMP_1;
                bitmapREAL = Resource1.TestReal_1;
                iterator++;
            }
            else if (iterator == 2)
            {
                bitmapRGB = Resource1.TestRGB_2;
                bitmapTEMP = Resource1.TestTEMP_2;
                bitmapREAL = Resource1.TestReal_2;
                iterator++;
            }
            else
            {
                bitmapRGB = Resource1.TestRGB_3;
                bitmapTEMP = Resource1.TestTEMP_3;
                bitmapREAL = Resource1.TestReal_3;
                iterator = 1;
            }
            imgRGB = new FastBitmap(bitmapRGB);
            imgTEMP = new FastBitmap(bitmapTEMP);

            imgGray = imgRGB.Clone();
            imgGray.ToGray();
            imgTEMPGray = imgTEMP.Clone();
            imgTEMPGray.ToGray();

            imgReal = new FastBitmap(bitmapREAL);

            pictureRGB.Image = imgRGB.Bitmap;
            pictureTEMP.Image = imgTEMP.Bitmap;
        }
        /// <summary>
        /// MaximumMethod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            IImageFusion fusion = new MaximumMethodFusion();
            CreateFusionImage(fusion);
        }

        private void CreateFusionImage(IImageFusion fusion)
        {
            imgResultColor = fusion.Fusion(imgRGB, imgTEMP);
            imgResultGray = fusion.Fusion(imgGray, imgTEMPGray);
            pictureResult1.Image = imgResultColor?.Bitmap;
            pictureResult2.Image = imgResultGray?.Bitmap;
        }
        /// <summary>
        /// AveragingMethod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            IImageFusion fusion = new AveragingMethodFusion();
            CreateFusionImage(fusion);
        }
        /// <summary>
        /// InterlacingBaseMethod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button3_Click(object sender, EventArgs e)
        {
            IImageFusion fusion = new InterlacingMethodFusion();
            CreateFusionImage(fusion);
        }
        /// <summary>
        /// InterlacingMaximumMethod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click(object sender, EventArgs e)
        {
            IImageFusion fusion = new InterlacingMaximumMethodFusion();
            CreateFusionImage(fusion);
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
        FastBitmap ImageLoad(PictureBox pictureBox)
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
                    FastBitmap image = new FastBitmap(new Bitmap(path));
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
            return new FastBitmap();
        }

        private void buttonSegmentation_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1343, 600);
            label.Visible = true;
            trackBar1.Visible = true;
            label.Text = trackBar1.Value.ToString();

            BinarySegmentator binarySegmentator = new BinarySegmentator();
            byte threshold = (byte)trackBar1.Value;
            Segment(imgBinarizeReal, imgReal, threshold, pictureSegmenReal, binarySegmentator);
            Segment(imgBinarizeTEMP, imgTEMP, threshold, pictureSegmenTEMP, binarySegmentator);
            Segment(imgBinarizeResultColor, imgResultColor, threshold, pictureSegmenResult1, binarySegmentator);
            Segment(imgBinarizeResultGray, imgResultGray, threshold, pictureSegmenResult2, binarySegmentator);

            SegmentationAssessment(imgBinarizeReal, labelRealM1White, labelRealM1Black, labelRealM2White, labelRealM2Black);
            SegmentationAssessment(imgBinarizeTEMP, labelTempM1White, labelTempM1Black, labelTempM2White, labelTempM2Black);
            SegmentationAssessment(imgBinarizeResultColor, labelResult1M1White, labelResult1M1Black, labelResult1M2White, labelResult1M2Black);
            SegmentationAssessment(imgBinarizeResultGray, labelResult2M1White, labelResult2M1Black, labelResult2M2White, labelResult2M2Black);
        }
        private void Segment(FastBitmap resultImg, FastBitmap inputImg, byte threshold, PictureBox pictureBox, BinarySegmentator binarySegmentator)
        {
            resultImg = binarySegmentator.Segmentation(inputImg, threshold, 255);
            pictureBox.Image = resultImg.Bitmap;
        }
        private void SegmentationAssessment(FastBitmap image, Label label1, Label label2, Label label3, Label label4)
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
