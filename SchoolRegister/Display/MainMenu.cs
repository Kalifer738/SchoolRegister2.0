using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnimateControl;
using Display.Animation;
using KonstantinControls;

namespace Display
{
    public partial class MainMenu : Form
    {
        SideMenu sideMenu;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            sideMenu = new SideMenu(this, true);
            RotateImage.Test(this);
            this.MouseWheel += RandomizeImage;
        }

        private void RandomizeImage(object sender, MouseEventArgs e)
        {
            Bitmap randomImage = RotateImage.MakeRandomImage((Bitmap)RotateImage.myPictureBox.Image);
            RotateImage.myPictureBox.Image = randomImage;
        }
    }
}
