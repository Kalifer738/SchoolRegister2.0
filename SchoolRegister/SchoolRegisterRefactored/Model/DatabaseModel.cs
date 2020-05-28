using Display.Scripts;
using MySql.Data.MySqlClient;
using SchoolRegisterRefactored.Controller;
using SchoolRegisterRefactored.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

namespace SchoolRegisterRefactored.Model
{
    /// <summary>
    /// Represents the model used in MVC design pattern projects.
    /// </summary>
    class DatabaseModel
    {
        readonly RegisterController registerController;

        readonly SchoolRegisterContext context;

        readonly MySqlConnection databaseConnection;

        private DispatcherTimer savingChangesTimer;

        public DatabaseModel(RegisterController registerController)
        {
            this.registerController = registerController;
            savingChangesTimer = new DispatcherTimer();
            context = new SchoolRegisterContext();
            databaseConnection = new MySqlConnection(context.Database.Connection.ConnectionString);
        }

        #region Class Methods

        /// <summary>
        /// Returns true if the class exists in the database.
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public bool DoesClassExist(string className)
        {
            @class classToCheck;
            try
            {
                classToCheck = context.classes.First(@class => @class.name == className);
            }
            catch (System.InvalidOperationException)
            {
                return false;
            }

            if (classToCheck != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns all classes expect the current class.
        /// </summary>
        /// <param name="currentClass">The current class.</param>
        /// <returns></returns>
        public string[] GetAllClassesExceptCurrentClass(string currentClass)
        {
            return context.classes.Where(@class => @class.name != currentClass).Select(x => x.name).ToArray();
        }

        /// <summary>
        /// Returns a class.
        /// </summary>
        /// <param name="id">The Class's ID.</param>
        /// <returns></returns>
        public @class GetClass(int classID)
        {
            return context.classes.First(c => c.id == classID);
        }

        /// <summary>
        /// Returns a class.
        /// </summary>
        /// <param name="id">The Class's name.</param>
        /// <returns></returns>
        public @class GetClass(string className)
        {
            return context.classes.First(c => c.name == className);
        }

        /// <summary>
        /// Returns an array with classes names.
        /// </summary>
        /// <returns></returns>
        public string[] GetAllClassesNames()
        {
            return context.classes.Select(@class => @class.name).ToArray();
        }

        #endregion

        /// <summary>
        /// Returns all students in a class from the database.
        /// </summary>
        /// <param name="classID">the class's id.</param>
        /// <returns>all students in the said class</returns>
        public student[] GetAllStudentsInClass(int classID)
        {
            return context.classes.First(@class => @class.id == classID).students.ToArray();
        }

        #region Students Void Methods

        /// <summary>
        /// Adds a new students to the database.
        /// </summary>
        /// <param name="firstName">Student's first name.</param>
        /// <param name="lastName">Students's last name.</param>
        /// <param name="classID">Students's class ID.</param>
        public void AddStudent(string firstName, string lastName, int classID)
        {
            student newStudent = new student();
            newStudent.first_name = firstName;
            newStudent.last_name = lastName;
            newStudent.class_id = classID;
            context.students.Add(newStudent);
            SaveChangesToDB();
        }

        /// <summary>
        /// Remove a student from the database.
        /// </summary>
        /// <param name="classID">Student's class ID.</param>
        /// <param name="firstName">Student's first name.</param>
        /// <param name="lastName">Student's last name.</param>
        public void RemoveStudent(int classID, string firstName, string lastName)
        {
            try
            {
                context.students.Remove(context.students.First(x => x.first_name == firstName && x.last_name == lastName && x.class_id == classID));
            }
            catch (Exception e)
            {
                if (e.Message == "Sequence contains no elements")
                {
                    MessageBox.Show("Cannot delete a non existing student!", "Invalid Operation!");
                }
                else
                {
                    MainDisplay.RegisterController.ShowError(e);
                }
            }
            context.SaveChangesAsync();
        }

        #endregion

        #region Student Updating Information

        public void UpdateStudentFirstName(int studentID, string value)
        {
            foreach (student registerStudent in context.students)
            {
                if (registerStudent.id == studentID)
                {
                    registerStudent.first_name = value;
                }
            }
            SaveChangedToDBWithDelay();
        }

        public void UpdateStudentLastName(int studentID, string value)
        {
            foreach (student registerStudent in context.students)
            {
                if (registerStudent.id == studentID)
                {
                    registerStudent.last_name = value;
                }
            }
            SaveChangedToDBWithDelay();
        }

        public void UpdateStudentAbsences(int studentID, float value)
        {
            foreach (student registerStudent in context.students)
            {
                if (registerStudent.id == studentID)
                {
                    registerStudent.absences = value;
                }
            }
            SaveChangedToDBWithDelay();
        }

        #region Grade Methods

        public void UpdateStudentGrades(int studentID, Dictionary<int, int> gradesDictinary)
        {
            //Add a methods to remove and add a mass ammout of same grades
            //After implemeting and fixing the side menu go ahead and make sure to test things and then to =>
            //Implement MetroFramework!!! https://thielj.github.io/MetroFramework/#Screenshots
            //It looks so much better than this ugly ass shit

            foreach (int key in gradesDictinary.Keys)
            {
                if (gradesDictinary[key] == 0)
                {
                    continue;
                }
                BeginAddingGrades(key, gradesDictinary[key], studentID);
            }
            SaveChangedToDBWithDelay();
        }

        private void BeginAddingGrades(int gradeType, int timesToAdd, int studentID)
        {
            ThreadStart threadStart = new ThreadStart(delegate
            {
                bool addGrades = true;
                if (timesToAdd < 0)
                {
                    addGrades = false;
                }
                for (int i = 0; i < timesToAdd; i++)
                {
                    if (addGrades)
                    {
                        AddGrade(gradeType, studentID);
                    }
                    else
                    {
                        RemoveGrade(gradeType, studentID);
                    }
                }
            });
            Thread addingGradesThread = new Thread(threadStart);
            addingGradesThread.Start();
        }

        private void RemoveGrade(int gradeType, int studentID)
        {
            bool foundStudent = false;
            MySqlCommand removeStudentGrade = new MySqlCommand("");
            removeStudentGrade.Connection = databaseConnection;

            foreach (student studentt in context.students)
            {
                if (studentt.id == studentID)
                {
                    foundStudent = true;
                    //removeStudentGrade.ExecuteNonQuery();
                    MessageBox.Show($"Found student! But Still cannot remove {gradeType}. Not saved to database!", "Not Implemented");
                    break;
                }
            }

            if (!foundStudent)
            {
                MessageBox.Show($"Error! Cannot find student with studentID({studentID})", "Not Implemented");
            }
        }

        private void AddGrade(int gradeType, int studentID)
        {
            bool foundStudent = false;
            MySqlCommand addGrades = new MySqlCommand("");
            addGrades.Connection = databaseConnection;

            foreach (student studentt in context.students)
            {
                if (studentt.id == studentID)
                {
                    foundStudent = true;
                    //removeStudentGrade.ExecuteNonQuery();
                    MessageBox.Show($"Found student! But Still cannot add {gradeType}. Not saved to database!");
                    break;
                }
            }

            if (!foundStudent)
            {
                MessageBox.Show($"Error! Cannot find student with studentID({studentID})", "Cannot Add Grade!");
            }
        }

        #endregion

        #endregion

        private void AwaitAction(int miliseconds, Action action)
        {
            savingChangesTimer.Interval = TimeSpan.FromMilliseconds(miliseconds);
            savingChangesTimer.Tick += delegate
            {
                action.Invoke();
                savingChangesTimer.Stop();
            };

            savingChangesTimer.Start();
        }

        /// <summary>
        /// Saves all changes done to the database with a delay of 5 seconds.
        /// </summary>
        private void SaveChangedToDBWithDelay()
        {
            if (savingChangesTimer.IsEnabled)
            {
                savingChangesTimer.Interval = TimeSpan.Zero;
            }

            AwaitAction(5000, new Action(SaveChangesToDB));
        }

        /// <summary>
        /// Saves all changes done to the database.
        /// </summary>
        public void SaveChangesToDB()
        {
            context.SaveChanges();
        }
    }
}
