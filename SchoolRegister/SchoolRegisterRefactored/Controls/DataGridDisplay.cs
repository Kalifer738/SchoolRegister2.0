using Display.Scripts;
using KonstantinControls.Interfaces;
using SchoolRegisterRefactored.Display;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolRegisterRefactored.Controls
{
    class DataGridDisplay : DataGridView, ICustomControl
    {
        Form mainForm;
        @class currentClass;
        Point lastSelectedCellPosition;
        bool inicialized;
        bool ignoreCellUpdate;

        public @class CurrentClass 
        {
            get
            {
                return currentClass;
            }
            set
            {
                currentClass = value;
                if (inicialized)
                {
                    ignoreCellUpdate = true;
                    LoadStudents();
                    ignoreCellUpdate = false;
                }
            }
        }

        public DataGridDisplay()
        {
            inicialized = false;
        }

        public void InicializeDataGrid()
        {
            this.Size = new Size(mainForm.Width - 2, mainForm.Width - 1);
            this.Location = new Point(19, 0);

            this.Name = "DataGridStudents";
            this.DataError += DataGridError;
            this.CellBeginEdit += DataGridCellBeginEdit;
            this.CellValueChanged += DataGridCellValueChanged;
            this.AutoGenerateColumns = true;
            this.AllowUserToResizeRows = false;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToAddRows = false;
            this.AllowDrop = false;
            mainForm.Controls.Add(this);

            int currentClassID = RegisterSettings.CurrentSettings.ClassToLoadID;
            if (currentClassID <= 0)
            {
                return;
            }
            CurrentClass = MainDisplay.RegisterController.GetClass(currentClassID);
        }

        #region Events

        private void DataGridCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!ignoreCellUpdate)
            {
                int studentID = (int)(this["id", e.RowIndex].Value);
                switch (this.Columns[e.ColumnIndex].Name)
                {
                    case "first_name": MainDisplay.RegisterController.UpdateStudentFirstName(studentID, this[e.ColumnIndex, e.RowIndex].Value.ToString()); break;
                    case "last_name": MainDisplay.RegisterController.UpdateStudentLastName(studentID, this[e.ColumnIndex, e.RowIndex].Value.ToString()); break;
                    case "absences": MainDisplay.RegisterController.UpdateStudentAbsences(studentID, (float)this[e.ColumnIndex, e.RowIndex].Value); break;
                    case "grades":
                        int[] grades = GradesToArray(this[e.ColumnIndex, e.RowIndex].Value.ToString());
                        ignoreCellUpdate = true;
                        this[e.ColumnIndex, e.RowIndex].Value = GradesToString(grades);
                        ignoreCellUpdate = false;
                        MainDisplay.RegisterController.UpdateStudentGrades(studentID, grades); break;
                    default: throw new Exception("You've Edited a non existant column..." + Environment.NewLine + "How...? How is that possible???? REPORT THIS!");
                }
            }
        }

        private int[] GradesToArray(string gradesString)
        {
            List<int> grades = new List<int>();
            foreach (char letter in gradesString)
            {
                if (Char.IsDigit(letter))
                {
                    int grade = int.Parse(letter.ToString());
                    if (grade > 6)
                    {
                        grade = 6;
                    }
                    else if (grade < 2)
                    {
                        grade = 2;
                    }
                    grades.Add(grade);
                }
            }

            return grades.ToArray();
        }

        private string GradesToString(grade[] grades)
        {
            string currentGrade = "";

            if (grades.Count() <= 0)
            {
                return "No Grades";
            }

            for (int i = 0; i < grades.Count() - 1; i++)
            {
                currentGrade += grades[i].grade1 + ", ";
            }

            if (grades.Count() > 1)
            {
                currentGrade += grades[grades.Count() - 1].grade1;
            }
            return currentGrade;
        }

        private string GradesToString(int[] grades)
        {
            string currentGrade = "";

            if (grades.Count() <= 0)
            {
                return "No Grades";
            }

            for (int i = 0; i < grades.Count() - 1; i++)
            {
                currentGrade += grades[i] + ", ";
            }

            if (grades.Count() > 1)
            {
                currentGrade += grades[grades.Count() - 1];
            }
            return currentGrade;
        }

        private void DataGridCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            lastSelectedCellPosition = new Point(e.ColumnIndex, e.RowIndex);
        }

        private void DataGridError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message == "Input string was not in the correct format" || this.Columns[e.ColumnIndex].Name == "absences")
            {
                MessageBox.Show("You cannot place any symbols other than '.' in the Absences columns!", "Invalid input!");
            }
            MessageBox.Show("We recommend that you close the application and opening it before continuing!" + Environment.NewLine
                + "Exception Message: " + e.Exception.Message, "System Datagrid Error Exception!");
            e.ThrowException = false;
        }

        #endregion

        private void LoadStudents()
        {
            if (CurrentClass.students == null)
            {
                return;
            }
            this.DataSource = null;
            this.DataSource = CurrentClass.students.ToArray();


            for (int i = 0; i < this.Columns.Count; i++)
            {
                switch (this.Columns[i].Name)
                {
                    case "first_name": break;
                    case "last_name": break;
                    case "absences": break;
                    case "id": this.Columns[i].Visible = false; break;
                    default: Columns.Remove(this.Columns[i].Name); i--; break;
                }
            }

            this.Columns[0].HeaderText = "First Name";
            this.Columns[1].HeaderText = "Last Name";
            this.Columns[2].HeaderText = "Absences";
            this.Columns.Add("grades", "Grades");



            foreach (DataGridViewColumn column in this.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            InsertCurrentStudentsGrades();
        }

        private void InsertCurrentStudentsGrades()
        {
            student[] students = currentClass.students.ToArray();

            if (students.Count() == 1)
            {
                this[this.Columns.Count - 1, 0].Value = GradesToString(students[0].grades.ToArray());
                return;
            }

            for (int i = 0; i < students.Count(); i++)
            {
                this[this.Columns.Count - 1, i].Value = GradesToString(students[i].grades.ToArray());
            }
            //if (students.Count() == 1)
            //{
                //int[] currentStudentGrades;
                //string currentGrade = "";
                //currentStudentGrades = students[0].grades.Select(x => x.grade1).ToArray();

                //if (currentStudentGrades.Count() <= 0)
                //{
                //    this[this.Columns.Count - 1, 0].Value = "No Grades";
                //    return;
                //}

                //for (int i = 0; i < currentStudentGrades.Count() - 1; i++)
                //{
                //    currentGrade += currentStudentGrades[i] + ", ";
                //}
                //currentGrade += currentStudentGrades[0];
                //this[this.Columns.Count - 1, 0].Value = currentGrade;
                //return;
            //}

            //for (int i = 0; i < students.Count(); i++)
            //{
            //    int[] currentStudentGrades;
            //    string currentGrade = "";
            //    currentStudentGrades = students[i].grades.Select(x => x.grade1).ToArray();

            //    if (currentStudentGrades.Count() <= 0)
            //    {
            //        this[this.Columns.Count - 1, 0].Value = "No Grades";
            //        return;
            //    }

            //    for (int i2 = 0; i2 < currentStudentGrades.Count() - 1; i2++)
            //    {
            //        currentGrade += currentStudentGrades[i2] + ", ";
            //    }

            //    currentGrade += currentStudentGrades[currentStudentGrades.Count() - 1];
            //    this[this.Columns.Count - 1, i].Value = currentGrade;
            //}
        }

        /// <summary>
        /// Requared Method to setup the control. Gets called only once when everything is already inicialized.
        /// </summary>
        public void Start(Form mainForm)
        {
            if (inicialized)
            {
                return;
            }

            this.mainForm = mainForm;
            inicialized = true;
            InicializeDataGrid();
            MainDisplay.RegisterController.OnClassChanged += UpdateCurrentClass;
            UpdateCurrentClass();
        }

        private void UpdateCurrentClass()
        {
            if (MainDisplay.CurrentClass == null)
            {
                return;
            }
            CurrentClass = MainDisplay.CurrentClass;
        }
    }
}
