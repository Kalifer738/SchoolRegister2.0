using SchoolRegisterRefactored.Display;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolRegisterRefactored.View
{
    public partial class AddClassForm : Form
    {
        public AddClassForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void addClassButton_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(classNameTextBox.Text))
            {
                errorMessage += "You need to enter a class name!" + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Empty Fields!");
                return;
            }
            MainDisplay.RegisterController.AddClass(classNameTextBox.Text);
            MainDisplay.RegisterController.RefreshClasses();
        }
    }
}
