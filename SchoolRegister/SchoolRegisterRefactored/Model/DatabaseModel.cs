using SchoolRegisterRefactored.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private DispatcherTimer savingChangesTimer;

        public DatabaseModel(RegisterController registerController)
        {
            this.registerController = registerController;
            savingChangesTimer = new DispatcherTimer();
            context = new SchoolRegisterContext();
        }

        /// <summary>
        /// Saves all changes done to the database.
        /// </summary>
        public void SaveChangesToDB()
        {
            context.SaveChanges();
            student studenT = context.students.First(x => x.id == 1);
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

        #region Students Methods

        /// <summary>
        /// Returns all students in a class from the database.
        /// </summary>
        /// <param name="classID">the class's id.</param>
        /// <returns>all students in the said class</returns>
        public student[] GetAllStudentsInClass(int classID)
        {
            return context.classes.First(@class => @class.id == classID).students.ToArray();
        }

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
        }

        /// <summary>
        /// Remove a student from the database.
        /// </summary>
        /// <param name="classID">The class the students is in.</param>
        /// <param name="studentID">The student's id.</param>
        public void RemoveStudent(int classID, int studentID)
        {
            student studentToBeRemoved = context.students.First(student => student.id == classID && student.id == studentID);
            context.students.Remove(studentToBeRemoved);
            context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove a student from the database.
        /// </summary>
        /// <param name="classID">Student's class ID.</param>
        /// <param name="firstName">Student's first name.</param>
        /// <param name="lastName">Student's last name.</param>
        public void RemoveStudent(int classID, string firstName, string lastName)
        {
            student studentToBeRemoved = context.students.First(student => student.first_name == firstName && student.last_name == lastName && student.class_id == classID);
            context.students.Remove(studentToBeRemoved);
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
            SaveLastChangesToDB();
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
            SaveLastChangesToDB();
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
            SaveLastChangesToDB();
        }

        public void UpdateStudentGrades(int studentID, int[] value)
        {
            MessageBox.Show("Not Implemented! What you type is only seen on your side");
            return;
            grade[] newGrades = new grade[value.Count()];

            for (int i = 0; i < value.Count(); i++)
            {
                newGrades[i] = new grade();
                newGrades[i].grade1 = value[i];
                newGrades[i].id = value[i] - 1;
            }

            context.Database.ExecuteSqlCommand(
                ""
                );

            //foreach (student registerStudent in context.students)
            //{
            //    if (registerStudent.id == studentID)
            //    {
            //        foreach (grade newGrade in grades)
            //        {
            //            registerStudent.grades = grades;
            //        }
            //        break;
            //    }
            //}
            SaveLastChangesToDB();
        }

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

        private void SaveLastChangesToDB()
        {
            if (savingChangesTimer.IsEnabled)
            {
                savingChangesTimer.Interval = TimeSpan.Zero;
            }

            AwaitAction(5000, new Action(SaveChangesToDB));
        }
    }
}
