using Display.Scripts;
using MySql.Data.MySqlClient;
using SchoolRegisterRefactored.Controller;
using SchoolRegisterRefactored.Display;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
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
    public class DatabaseModel
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
            context.Database.Connection.Close();
            databaseConnection = new MySqlConnection(context.Database.Connection.ConnectionString);
        }

        #region Class Methods

        #region Class Read Methods

        /// <summary>
        /// Returns true if the class exists in the database.
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public bool DoesClassExist(string className)
        {
            context.Database.Connection.Open();
            using (context.Database.Connection)
            {
                if (className == "None")
                {
                    return false;
                }
                @class classToCheck;
                try
                {
                    classToCheck = context.classes.First(@class => @class.name == className);
                }
                catch (System.InvalidOperationException)
                {
                    MainDisplay.RegisterController.ShowError(new Exception($"No class found with the name {className}!"), "Class Not Found!", false);
                    return false;
                }

                if (classToCheck != null)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Returns all classes expect the current class.
        /// </summary>
        /// <param name="currentClass">The current class.</param>
        /// <returns></returns>
        public string[] GetAllClassesExceptCurrentClass(string currentClass)
        {
            context.Database.Connection.Open();
            using (context.Database.Connection)
            {
                return context.classes.Where(@class => @class.name != currentClass).Select(x => x.name).ToArray();
            }
        }

        /// <summary>
        /// Returns a class.
        /// </summary>
        /// <param name="id">The Class's ID.</param>
        /// <returns></returns>
        public @class GetClass(int classID)
        {
            context.Database.Connection.Open();
            @class classToReturn = null;
            using (context.Database.Connection)
            {
                try
                {
                    classToReturn = context.classes.First(c => c.id == classID);
                }
                catch (Exception e)
                {
                    if (e.Message == "Sequence contains no elements")
                    {
                        MainDisplay.RegisterController.ShowError(new Exception($"Class with classID({classID}) does not exist!"), "", false);
                    }
                    else
                    {
                        MainDisplay.RegisterController.ShowError(e, "", true);
                    }
                }
                return classToReturn;
            }
        }

        /// <summary>
        /// Returns a class.
        /// </summary>
        /// <param name="id">The Class's name.</param>
        /// <returns></returns>
        public @class GetClass(string className)
        {
            if (className == "None")
            {
                return null;
            }
            @class classToReturn = null;
            context.Database.Connection.Open();
            using (context.Database.Connection)
            {
                try
                {
                    classToReturn = context.classes.First(c => c.name == className);
                }
                catch (Exception e)
                {
                    if (e.Message == "Sequence contains no elements")
                    {
                        MainDisplay.RegisterController.ShowError(new Exception("Class with classID({classID}) does not exist!"), "", false);
                    }
                    else
                    {
                        MainDisplay.RegisterController.ShowError(e, "", true);
                    }
                }
                return classToReturn;
            }
        }

        /// <summary>
        /// Returns an array with classes names.
        /// </summary>
        /// <returns></returns>
        public string[] GetAllClassesNames()
        {
            context.Database.Connection.Open();
            using (context.Database.Connection)
            {
                return context.classes.Select(@class => @class.name).ToArray();
            }
        }

        #endregion

        /// <summary>
        /// Adds a class to the database.
        /// </summary>
        /// <param name="className">The class's name.</param>
        public void AddClass(string className)
        {
            using (context.Database.Connection)
            {
                @class classToAdd = new @class();
                classToAdd.name = className;

                @class returnedClass = null;
                //Added a check if the class doesn't exist.
                try
                {
                    returnedClass = context.classes.First(x => x.name == className);
                }
                catch (Exception e)
                {
                    if (e.Message == "Sequence contains no elements")
                    {
                        context.classes.Add(classToAdd);
                        SaveChangesToDB();
                        return;
                    }
                    if (returnedClass != null)
                    {
                        MainDisplay.RegisterController.ShowError(new Exception("Class already exists!"), "Cannot Add Class!", false);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            SaveChangesToDB();
        }


        /// <summary>
        /// Removes a class from the database.
        /// </summary>
        /// <param name="className">The class's name.</param>
        public void RemoveClass(string className)
        {
            using (context.Database.Connection)
            {
                try
                {
                    @class classToRemove = context.classes.First(@class => @class.name == className);
                    student[] studentsToRemove = context.students.Where(x => x.class_id == classToRemove.id).ToArray();
                    grade[] gradesToRemove = context.grades.Where(x => x.student.class_id == classToRemove.id).ToArray();

                    context.classes.Remove(classToRemove);
                    context.grades.RemoveRange(gradesToRemove);
                    context.students.RemoveRange(GetAllStudentsInClass(classToRemove.id));

                    foreach (grade gradeToRemove in gradesToRemove)
                    {
                        context.Entry(gradeToRemove).State = EntityState.Deleted;
                    }
                    foreach (student studentToRemove in studentsToRemove)
                    {
                        context.Entry(studentToRemove).State = EntityState.Deleted;
                    }
                    context.Entry(classToRemove).State = EntityState.Deleted;
                }
                catch (System.InvalidOperationException)
                {
                    MainDisplay.RegisterController.ShowError(new Exception($"No Class with name {className} exists!"), "Class Not Found!", false);
                }
            }
            SaveChangesToDB();
        }

        #endregion

        /// <summary>
        /// Returns all students in a class from the database.
        /// </summary>
        /// <param name="classID">the class's id.</param>
        /// <returns>all students in the said class</returns>
        public student[] GetAllStudentsInClass(int classID)
        {
            context.Database.Connection.Open();
            using (context.Database.Connection)
            {
                return context.classes.First(@class => @class.id == classID).students.ToArray();
            }
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
            context.Database.Connection.Open();
            using (context.Database.Connection)
            {
                student newStudent = new student();
                newStudent.first_name = firstName;
                newStudent.last_name = lastName;
                newStudent.class_id = classID;
                context.students.Add(newStudent);
                SaveChangesToDB();
            }
        }

        /// <summary>
        /// Remove a student from the database.
        /// </summary>
        /// <param name="classID">Student's class ID.</param>
        /// <param name="firstName">Student's first name.</param>
        /// <param name="lastName">Student's last name.</param>
        public void RemoveStudent(int classID, string firstName, string lastName)
        {
            if (context.Database.Connection.State == ConnectionState.Open)
            {
                context.Database.Connection.Close();
            }
            context.Database.Connection.Open();
            using (context.Database.Connection)
            {
                try
                {
                    //Forgot to set the entity to deleted...
                    student studentToRemove = context.students.First(x => x.first_name == firstName && x.last_name == lastName && x.class_id == classID);
                    grade[] studentGrades = studentToRemove.grades.ToArray();

                    context.grades.RemoveRange(studentGrades);
                    context.students.Remove(studentToRemove);
                    //And the grade entities of that student to Deleted...
                    context.Entry(studentToRemove).State = EntityState.Deleted;
                    foreach (grade studentGrade in studentGrades)
                    {
                        context.Entry(studentGrade).State = EntityState.Deleted;
                    }
                }
                catch (Exception e)
                {
                    if (e.Message == "Sequence contains no elements")
                    {
                        MainDisplay.RegisterController.ShowError(new Exception("Cannot delete a non existing student!"), "Invalid Operation!", false);
                    }
                    else
                    {
                        MainDisplay.RegisterController.ShowError(e, "", true);
                    }
                }
            }
            SaveChangesToDB();
        }

        #endregion

        #region Student Updating Information

        public void UpdateStudentFirstName(int studentID, string value)
        {
            context.Database.Connection.Open();
            using (context.Database.Connection)
            {
                foreach (student registerStudent in context.students)
                {
                    if (registerStudent.id == studentID)
                    {
                        registerStudent.first_name = value;
                    }
                }
            }
            SaveChangesToDB();
        }

        public void UpdateStudentLastName(int studentID, string value)
        {
            context.Database.Connection.Open();
            using (context.Database.Connection)
            {
                foreach (student registerStudent in context.students)
                {
                    if (registerStudent.id == studentID)
                    {
                        registerStudent.last_name = value;
                    }
                }
            }
            SaveChangesToDB();
        }

        public void UpdateStudentAbsences(int studentID, float value)
        {
            context.Database.Connection.Open();
            using (context.Database.Connection)
            {
                foreach (student registerStudent in context.students)
                {
                    if (registerStudent.id == studentID)
                    {
                        registerStudent.absences = value;
                    }
                }
            }
            SaveChangesToDB();
        }

        #endregion

        #region Grade Methods

        public void UpdateStudentGrades(int studentID, Dictionary<int, int> gradesDictinary)
        {
            foreach (int keyValue in gradesDictinary.Keys)
            {
                if (gradesDictinary[keyValue] == 0)
                {
                    continue;
                }
                BeginAddingGrades(keyValue, gradesDictinary[keyValue], studentID);
            }
        }

        private void BeginAddingGrades(int gradeType, int timesToAdd, int studentID)
        {
            bool addGrades = true;
            if (timesToAdd < 0)
            {
                addGrades = false;
            }
            for (int i = 0; i < Math.Abs(timesToAdd); i++)
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
            SaveChangesToDB();
        }

        /// <summary>
        /// Removes a grade from the student
        /// </summary>
        /// <param name="gradeType">grade number</param>
        /// <param name="studentID">studnet ID</param>
        public void RemoveGrade(int gradeType, int studentID)
        {
            bool foundStudent = false;



            foreach (student studentt in context.students)
            {
                if (studentt.id == studentID)
                {
                    foundStudent = true;
                    try
                    {
                        if (context.Database.Connection.State == ConnectionState.Open)
                        {
                            context.Database.Connection.Close();
                        }
                        context.Database.Connection.Open();
                        using (context.Database.Connection)
                        {
                            grade newGrade = context.students.First(theStudent => theStudent.id == studentID).grades.First(x => x.grade1 == gradeType);
                            context.students.First(theStudent => theStudent.id == studentID).grades.Remove(newGrade);
                            context.Entry(newGrade).State = EntityState.Deleted;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;
                }
            }

            if (!foundStudent)
            {
                MainDisplay.RegisterController.ShowError(new Exception($"Error! Cannot find student with studentID({studentID})"), "Student Doesn't Exist!", false);
            }
        }

        /// <summary>
        /// Adds a grade to the student
        /// </summary>
        /// <param name="gradeType">grade number</param>
        /// <param name="studentID">studnet ID</param>
        public void AddGrade(int gradeType, int studentID)
        {
            bool foundStudent = false;
            //MySqlCommand addGrades = new MySqlCommand("");
            //addGrades.Connection = databaseConnection;
            grade newGrade = new grade();
            newGrade.grade1 = gradeType;
            newGrade.student_id = studentID;

            foreach (student studentt in context.students)
            {
                if (studentt.id == studentID)
                {
                    foundStudent = true;
                    try
                    {
                        if (context.Database.Connection.State == ConnectionState.Open)
                        {
                            context.Database.Connection.Close();
                        }
                        context.Database.Connection.Open();
                        using (context.Database.Connection)
                        {
                            context.students.First(theStudent => theStudent.id == studentID).grades.Add(newGrade);
                        }
                    }
                    catch (Exception e)
                    {
                        if (e.Message == "Sequence contains no elements")
                        {
                            MainDisplay.RegisterController.ShowError(new Exception($"Error! Cannot find student with studentID({studentID})"), "Student Doesn't Exist!", false);
                        }
                        else
                        {
                            MainDisplay.RegisterController.ShowError(e, "", true);
                        }
                        throw;
                    }
                    break;
                }
            }

            if (!foundStudent)
            {
                MainDisplay.RegisterController.ShowError(new Exception($"Error! Cannot find student with studentID({studentID})"), "Student Doesn't Exist!", false);
            }
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
            //Changed how changes are saved to the database
            if (context.Database.Connection.State == ConnectionState.Open)
            {
                context.Database.Connection.Close();
            }
            context.Database.Connection.Open();
            using (context.Database.Connection)
            {
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                }
            }
        }
    }
}
