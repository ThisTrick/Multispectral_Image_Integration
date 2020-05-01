using System;
using System.Drawing;
using System.Windows.Forms;
using Multispectral_Image_Integration_Library;

namespace WindowsFormsAppForTest
{
    public partial class Form1 : Form
    {
        private Bitmap Bitmap;
        public Form1()
        {
            InitializeComponent();
        }

        private void load_Click(object sender, EventArgs e)
        {
            Bitmap = ImageLoad(pictureBox);
        }

        private void clickMe_Click(object sender, EventArgs e)
        {
            if (Bitmap == null)
            {
                return;
            }
            FastBitmap image = new FastBitmap(Bitmap);
            image.ToGray();
            pictureBox.Image = image.Bitmap;

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
