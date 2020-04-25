using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register.Animation
{
    class RotateImage
    {
        public static PictureBox myPictureBox;
        public static void Test(Form currentForm)
        {
            myPictureBox = new PictureBox();
            myPictureBox.Location = new Point(350, 100);
            myPictureBox.Size = new Size(300, 300);
            myPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            Bitmap image = MakeRandomImage(new Bitmap(300, 300));

            myPictureBox.Image = image;
            myPictureBox.Click += RandomizeImage;
            myPictureBox.DoubleClick += RandomizeImage;
            currentForm.Controls.Add(myPictureBox);

        }

        private static void RandomizeImage(object sender, EventArgs e)
        {
            //Bitmap myBitmap = (Bitmap)myPictureBox.Image;
            //myBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //myPictureBox.Image = myBitmap;
            myPictureBox.Image = MakeRandomImage((Bitmap)myPictureBox.Image);
        }

        public static Bitmap MakeRandomImage(Bitmap bitmap)
        {
            Random myRandom = new Random();
            for (int i1 = 0; i1 < bitmap.Height; i1++)
            {
                for (int i2 = 0; i2 < bitmap.Width; i2++)
                {
                    bitmap.SetPixel(i1, i2, Color.FromArgb(myRandom.Next(0, 255), myRandom.Next(0, 255), myRandom.Next(0, 255)));
                }
            }
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int i2 = 0; i2 < 4; i2++)
            //    {
            //        bitmap.SetPixel(i, i2, Color.Black);
            //    }
            //}
            return bitmap;
        }
    }
}
