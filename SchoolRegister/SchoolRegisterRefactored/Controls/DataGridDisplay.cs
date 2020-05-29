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
        bool inicialized;
        bool ignoreCellUpdate;
        string gradesBeforeGettingEdited;

        /// <summary>
        /// Represents the currently selected class in the DataGrid
        /// </summary>
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
                        {
                            int[] editedGrades = GradesToArray(this[e.ColumnIndex, e.RowIndex].Value.ToString());
                            
                            ignoreCellUpdate = true;
                            this[e.ColumnIndex, e.RowIndex].Value = GradesToString(editedGrades);
                            ignoreCellUpdate = false;

                            Dictionary<int, int> differenceBetweenOldAndNewGrades = ArrayDifferenceCalculator.GetDifferenceDictinary(GradesToArray(gradesBeforeGettingEdited), editedGrades);

                            MainDisplay.RegisterController.UpdateStudentGrades(studentID, differenceBetweenOldAndNewGrades); break;
                        }
                    default: throw new Exception("You've Edited a non existant column..." + Environment.NewLine + "How...? How is that possible???? REPORT THIS!");
                }
            }
        }

        private void DataGridCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (this.Columns[e.ColumnIndex].Name == "grades")
            {
                gradesBeforeGettingEdited = this[e.ColumnIndex, e.RowIndex].Value.ToString();
            }
        }

        private void DataGridError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message == "Input string was not in the correct format" || this.Columns[e.ColumnIndex].Name == "absences")
            {
                MessageBox.Show("You cannot place any symbols other than '.' in the Absences columns!", "Invalid input!");
            }
            MainDisplay.RegisterController.ShowError(e.Exception);

            e.ThrowException = false;
        }

        #endregion

        #region Grade Methods

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
            int[] sortedGrades = grades.ToArray();
            Array.Sort(sortedGrades);
            return sortedGrades;
        }

        private string GradesToString(grade[] grades)
        {
            if (grades.Count() <= 0)
            {
                return "No Grades";
            }

            int[] sortedGrades = grades.Select(x => x.grade1).ToArray();
            Array.Sort(sortedGrades);
            string currentGrade = "";

            for (int i = 0; i < sortedGrades.Count() - 1; i++)
            {
                currentGrade += sortedGrades[i] + ", ";
            }

            if (sortedGrades.Count() > 1)
            {
                currentGrade += sortedGrades[sortedGrades.Count() - 1];
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

        #endregion

        private void LoadStudents()
        {
            ignoreCellUpdate = true;
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
            ignoreCellUpdate = false;
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
        }

        private void UpdateCurrentClass()
        {
            if (MainDisplay.CurrentClass == null)
            {
                return;
            }
            CurrentClass = MainDisplay.CurrentClass;
        }

        /// <summary>
        /// Updates the data grid.
        /// </summary>
        public void UpdateDataGrid()
        {
            LoadStudents();
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
    }
}
