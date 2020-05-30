using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Display.Scripts;
using SchoolRegisterRefactored;
using SchoolRegisterRefactored.Display;
using SchoolRegisterRefactored.Model;

namespace SchoolRegisterRefactored.Controller
{
    public class RegisterController
    {
        readonly MainDisplay display;
        readonly DatabaseModel databaseModel;
        readonly SettingsModel settingsModel;
        private Action onClassChange;

        /// <summary>
        /// Occurs when the current class has been changed
        /// </summary>
        public Action OnClassChanged
        {
            get
            {
                return onClassChange;
            }
            set
            {
                onClassChange = value;
            }
        }


        /// <summary>
        /// Refreshes the classes labels.
        /// </summary>
        public void RefreshClasses()
        {
            display.RefreshClasses();
        }

        /// <summary>
        /// Represents the Controller in the MVC Design Pattern.
        /// </summary>
        /// <param name="display"></param>
        public RegisterController(MainDisplay display)
        {
            this.display = display;
            databaseModel = new DatabaseModel(this);
            settingsModel = new SettingsModel(this);

        }

        #region Classes Methods

        public bool DoesClassExist(string className)
        {
            return databaseModel.DoesClassExist(className);
        }

        /// <summary>
        /// Returns a class.
        /// </summary>
        /// <param name="id">The Class's ID.</param>
        /// <returns></returns>
        public @class GetClass(int classID)
        {
            return databaseModel.GetClass(classID);
        }

        /// <summary>
        /// Returns a class.
        /// </summary>
        /// <param name="id">The Class's name.</param>
        /// <returns></returns>
        public @class GetClass(string className)
        {
            return databaseModel.GetClass(className);
        }

        /// <summary>
        /// Returns all classes expect the current class.
        /// </summary>
        /// <param name="currentClass">The current class.</param>
        /// <returns></returns>
        public string[] GetAllClassesExceptCurrentClass(string currentClass)
        {
            return databaseModel.GetAllClassesExceptCurrentClass(currentClass);
        }

        /// <summary>
        /// Returns an array with classes names.
        /// </summary>
        /// <returns></returns>
        public string[] GetAllClassesNames()
        {
            return databaseModel.GetAllClassesNames();
        }

        /// <summary>
        /// Adds a class to the database.
        /// </summary>
        /// <param name="className">The class's name.</param>
        public void AddClass(string className)
        {
            databaseModel.AddClass(className);
        }

        /// <summary>
        /// Removes a class from the database.
        /// </summary>
        /// <param name="className">The class's name.</param>
        public void RemoveClass(string className)
        {
            databaseModel.RemoveClass(className);
        }

            #endregion

        #region Student Void Methods

        /// <summary>
        /// Adds a new students to the database.
        /// </summary>
        /// <param name="firstName">Student's first name.</param>
        /// <param name="lastName">Students's last name.</param>
        /// <param name="classID">Students's class ID.</param>
        public void AddStudent(string firstName, string lastName, int classID)
        {
            databaseModel.AddStudent(firstName, lastName, classID);
        }

        /// <summary>
        /// Remove a student from the database.
        /// </summary>
        /// <param name="classID">Student's class ID.</param>
        /// <param name="firstName">Student's first name.</param>
        /// <param name="lastName">Student's last name.</param>
        public void RemoveStudent(string firstName, string lastName, int classID)
        {
            databaseModel.RemoveStudent(classID, firstName, lastName);
        }
        #endregion

        /// <summary>
        /// Returns all students in a class from the database.
        /// </summary>
        /// <param name="classID">the class's id.</param>
        /// <returns>all students in the said class</returns>
        public student[] GetAllStudentsInClass(int classID)
        {
            return databaseModel.GetAllStudentsInClass(classID);
        }

        #region Students Updating Methods

        /// <summary>
        /// Updates a students first name.
        /// </summary>
        /// <param name="studentID">students ID.</param>
        /// <param name="value">The value to update to.</param>
        public void UpdateStudentFirstName(int studentID, string value)
        {
            databaseModel.UpdateStudentFirstName(studentID, value);
        }

        /// <summary>
        /// Updates a students last name.
        /// </summary>
        /// <param name="studentID">students ID.</param>
        /// <param name="value">The value to update to.</param>
        public void UpdateStudentLastName(int studentID, string value)
        {
            databaseModel.UpdateStudentLastName(studentID, value);
        }

        /// <summary>
        /// Updates a students absences.
        /// </summary>
        /// <param name="studentID">students ID.</param>
        /// <param name="value">The value to update to.</param>
        public void UpdateStudentAbsences(int studentID, float value)
        {
            databaseModel.UpdateStudentAbsences(studentID, value);
        }

        /// <summary>
        /// Updates a students grades.
        /// </summary>
        /// <param name="studentID">students ID.</param>
        /// <param name="value">The value to update to.</param>
        public void UpdateStudentGrades(int studentID, Dictionary<int, int> value)
        {
            databaseModel.UpdateStudentGrades(studentID, value);
        }

        #endregion

        #region Grade Methods

        public void RemoveGrade(int gradeToAdd, string firstName, string lastName)
        {
            int studentID;
            try
            {
                studentID = MainDisplay.CurrentClass.students.First(x => x.first_name == firstName && x.last_name == lastName).id;
            }
            catch (Exception)
            {
                MessageBox.Show("Student Doesn't Exist!");
                return;
            }

            databaseModel.RemoveGrade(gradeToAdd, studentID);
        }

        public void AddGrade(int gradeToAdd, string firstName, string lastName)
        {
            int studentID;
            try
            {
                studentID = MainDisplay.CurrentClass.students.First(x => x.first_name == firstName && x.last_name == lastName).id;
            }
            catch (Exception)
            {
                MessageBox.Show("Student Doesn't Exist!");
                return;
            }

            databaseModel.AddGrade(gradeToAdd, studentID);
        }



        #endregion

        /// <summary>
        /// Shows a message box with the error message from the exception.
        /// </summary>
        /// <param name="exception">The Exception</param>
        public void ShowError(Exception exception, string caption, bool recommendation)
        {
            display.ShowError(exception, caption, recommendation);
        }

        /// <summary>
        /// Updates the data grid.
        /// </summary>
        public void UpdateDataGrid()
        {
            display.UpdateDataGrid();
        }

        /// <summary>
        /// Saves the settings given to the settings.json file.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void SaveSettings(RegisterSettings settings)
        {
            settingsModel.SaveSettings(settings);
        }

        /// <summary>
        /// Saves all changes done to the database.
        /// </summary>
        public void SaveChangesToDatabase()
        {
            databaseModel.SaveChangesToDB();
        }

        /// <summary>
        /// Exists the application.
        /// </summary>
        public void ExitApplication()
        {
            display.Close();
        }
    }
}
