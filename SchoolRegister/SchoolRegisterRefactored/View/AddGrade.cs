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
    public partial class AddGradeForm : Form
    {
        public AddGradeForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void removeClassButton_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(gradeTextBox.Text))
            {
                errorMessage += "You need to enter a grade!" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(fistNameTextBox.Text))
            {
                errorMessage += "You need to enter a first name!" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(lastNameTextBox.Text))
            {
                errorMessage += "You need to enter a last name!" + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Empty Fields!");
                return;
            }
            int gradeToAdd = 0;
            foreach (char letter in gradeTextBox.Text)
            {
                if (Char.IsDigit(letter))
                {
                    gradeToAdd = int.Parse(letter.ToString());
                    if (gradeToAdd > 6)
                    {
                        gradeToAdd = 6;
                    }
                    else if (gradeToAdd < 2)
                    {
                        gradeToAdd = 2;
                    }
                }
            }
            if (gradeToAdd == 0)
            {
                MessageBox.Show("You cannot do that!", "Error!");
            }
            MainDisplay.RegisterController.AddGrade(gradeToAdd, fistNameTextBox.Text, lastNameTextBox.Text);
            MainDisplay.RegisterController.UpdateDataGrid();
        }
    }
}
