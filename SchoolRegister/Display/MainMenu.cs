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
using Register.Animation;
using KonstantinControls;
using Display.Display;
using Display.Controller;

namespace Register
{
    public partial class MainMenu : Form
    {
        ControllerHandaler controllerHandaler;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            controllerHandaler = new ControllerHandaler(this);
        }
    }
}
