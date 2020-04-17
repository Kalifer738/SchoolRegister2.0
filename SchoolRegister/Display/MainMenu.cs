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
            sideMenu = new SideMenu(this, false);
        }
    }
}
