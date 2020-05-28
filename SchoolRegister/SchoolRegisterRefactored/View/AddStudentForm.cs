﻿using SchoolRegisterRefactored.Display;
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
    public partial class AddStudentForm : Form
    {
        private bool addedStudent;
        public AddStudentForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            addedStudent = false;
            FormClosed += UpdateDataGrid;
        }

        private void UpdateDataGrid(object sender, FormClosedEventArgs e)
        {
            if (addedStudent)
            {
                MainDisplay.RegisterController.UpdateDataGrid();
            }
        }

        private void addStudentButton_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(firstNameTextBox.Text))
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
            if (MainDisplay.CurrentClass == null)
            {
                MessageBox.Show("You need to select a class before you can add students!", "No Class Selected!");
                return;
            }
            addedStudent = true;
            MainDisplay.RegisterController.AddStudent(firstNameTextBox.Text, lastNameTextBox.Text, MainDisplay.CurrentClass.id);
        }
    }
}