using SchoolRegisterRefactored.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegisterRefactored.Model
{
    class DatabaseModel
    {
        RegisterController registerController;

        SchoolRegisterContext context;

        public DatabaseModel(RegisterController registerController)
        {
            this.registerController = registerController;
            context = new SchoolRegisterContext();
        }

        /// <summary>
        /// Saves all changes done to the database.
        /// </summary>
        public void SaveChangesToDB()
        {
            context.SaveChanges();
        }

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
            string[] classesNames = new string[context.classes.Count()];
            for (int i = 0; i < context.classes.Count(); i++)
            {
                classesNames[i] = context.classes.First(@class => @class.id == i + 1).name;
            }
            return classesNames;
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
    }
}
