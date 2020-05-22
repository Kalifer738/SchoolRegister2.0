using SchoolRegisterRefactored.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolRegisterRefactored.Display
{
    public partial class MainDisplay : Form
    {
        private static RegisterController registerController;

        /// <summary>
        /// The main controller of the MVC model.
        /// </summary>
        public static RegisterController RegisterController 
        {
            get
            {
                return registerController;
            }
            private set
            {
                registerController = value;
            }
        }

        public MainDisplay()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            registerController = new RegisterController(this);
            sideMenu.Start(this);
        }
    }
}
