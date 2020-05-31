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
using Display.Scripts;
using SchoolRegisterRefactored.Model;
using SchoolRegisterRefactored.Properties;

namespace SchoolRegisterRefactored.Display
{
    public partial class MainDisplay : Form
    {
        private static RegisterController registerController;
        private static @class currentClass;

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


        /// <summary>
        /// Updates the data grid.
        /// </summary>
        public void UpdateDataGrid()
        {
            dataGridDisplay1.UpdateDataGrid();
        }

        public static @class CurrentClass
        {
            get
            {
                return currentClass;
            }
            set
            {
                currentClass = value;
                if (RegisterController.OnClassChanged != null)
                {
                    RegisterController.OnClassChanged.Invoke();
                }

            }
        }

        public MainDisplay()
        {
            InitializeComponent();
            this.FormClosing += ExitApplication;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            registerController = new RegisterController(this);
            if (RegisterSettings.CurrentSettings.ClassToLoadID > 0)
            {
                CurrentClass = RegisterController.GetClass(RegisterSettings.CurrentSettings.ClassToLoadID);
            }
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

        private void ExitApplication(object sender, FormClosingEventArgs e)
        {
            if (RegisterSettings.CurrentSettings.ShowMessages)
            {
                DialogResult buttonPressed = MessageBox.Show("Save changes before exiting?", "Are you sure you wanna exit?", MessageBoxButtons.YesNo);
                if (buttonPressed == DialogResult.Yes)
                {
                    RegisterController.SaveChangesToDatabase();
                    return;
                }
            }
            RegisterController.SaveChangesToDatabase();
        }

        /// <summary>
        /// Shows a message box with the error message from the exception.
        /// </summary>
        /// <param name="exception">The Exception</param>
        public void ShowError(Exception exception, string caption , bool recommendation)
        {
            if (string.IsNullOrEmpty(caption))
            {
                caption = "Error!";
            }
            if (recommendation)
            {
                MessageBox.Show("We recommend that you close the application and opening it before continuing!" + Environment.NewLine
                + "Exception Message: " + exception.Message + Environment.NewLine
                + "Inner Exception Message:" + exception.InnerException.Message, caption);
            }
            else
            {
                if (exception.InnerException != null)
                {
                    MessageBox.Show("Exception Message: " + exception.Message + Environment.NewLine
                    + "Inner Exception Message:" + exception.InnerException.Message, caption);
                }
                else
                {
                    MessageBox.Show("Exception Message: " + exception.Message, caption);
                }
            }
        }

        /// <summary>
        /// Refreshes the classes labels.
        /// </summary>
        public void RefreshClasses()
        {
            sideMenu.RefreshClasses();
        }
    }
}
