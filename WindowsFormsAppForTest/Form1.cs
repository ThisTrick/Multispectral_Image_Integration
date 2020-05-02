using System;
using System.Drawing;
using System.Windows.Forms;
using Multispectral_Image_Integration_Library;

namespace WindowsFormsAppForTest
{
    public partial class Form1 : Form
    {
        private Bitmap Bitmap1;
        private Bitmap Bitmap2;
        public Form1()
        {
            InitializeComponent();
        }

        private void load_Click(object sender, EventArgs e)
        {
            Bitmap1 = ImageLoad(pictureBox);
        }
        private void load2_Click(object sender, EventArgs e)
        {
            Bitmap2 = ImageLoad(pictureBox);
        }
        private void clickMe_Click(object sender, EventArgs e)
        {
            if (Bitmap1 == null && Bitmap2 == null)
            {
                return;
            }
            FastBitmap img1 = new FastBitmap(Bitmap1);
            FastBitmap img2 = new FastBitmap(Bitmap2);
            IImageFusion fusion = new AveragingMethodFusion();
            var imgResult = fusion.Fusion(img1, img2);
            pictureBox.Image = imgResult.Bitmap;

        }

        /// <summary>
        /// Загружает изображение в PictureBox и переменную.
        /// </summary>
        /// <param name="image">Переменная изображения</param>
        /// <param name="pictureBox">PictureBox</param>
        private Bitmap ImageLoad(PictureBox pictureBox)
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
                    Bitmap image = new Bitmap(path);
                    //Загружаем изображение в PictureBox
                    pictureBox.Image = image;
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


    }
}
