using KonstantinControls;
using SchoolRegisterRefactored.Controller;
using KonstantinControls.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolRegisterRefactored.Display
{
    public partial class MainDisplay : Form
    {
        private static RegisterController registerController;
        private static bool sideMenuLabelsDisabled = false;

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
            StartCustomControls();
        }

        private void StartCustomControls()
        {
            foreach (Control control in this.Controls)
            {
                //Type controlType = typeof(SideMenu).GetInterface("ICustomControl");
                Type controlType = control.GetType().GetInterface("ICustomControl");
                if (controlType != null)
                {
                    object[] thisMainDisplay = { this };
                    MethodInfo ICustomControlStartMethod = controlType.GetMethod("Start");
                    ICustomControlStartMethod.Invoke(control, thisMainDisplay);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (sideMenuLabelsDisabled)
            {
                sideMenu.EnableEveryLabel();
            }
            else
            {
                sideMenu.DisableEveryLabel();
            }
            sideMenuLabelsDisabled = !sideMenuLabelsDisabled;
        }
    }
}
