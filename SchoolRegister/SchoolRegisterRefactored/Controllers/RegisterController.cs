using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Returns all students in a class from the database.
        /// </summary>
        /// <param name="classID">the class's id.</param>
        /// <returns>all students in the said class</returns>
        public student[] GetAllStudentsInClass(int classID)
        {
            return databaseModel.GetAllStudentsInClass(classID);
        }

        #endregion

        #region Students Methods

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
        /// <param name="classID">The class the students is in.</param>
        /// <param name="studentID">The student's id.</param>
        public void RemoveStudent(int classID, int studentID)
        {
            databaseModel.RemoveStudent(classID, studentID);
        }

        /// <summary>
        /// Remove a student from the database.
        /// </summary>
        /// <param name="classID">Student's class ID.</param>
        /// <param name="firstName">Student's first name.</param>
        /// <param name="lastName">Student's last name.</param>
        public void RemoveStudent(int classID, string firstName, string lastName)
        {
            databaseModel.RemoveStudent(classID, firstName, lastName);
        }

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
        public void UpdateStudentGrades(int studentID, int[] value)
        {
            databaseModel.UpdateStudentGrades(studentID, value);
        }

        #endregion

        #region Settings Methods

        public void SaveSettings(RegisterSettings settings)
        {
            settingsModel.SaveSettings(settings);
        }

        #endregion
    }
}
