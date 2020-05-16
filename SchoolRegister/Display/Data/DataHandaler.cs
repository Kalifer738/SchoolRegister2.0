using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Display.Data
{
    class DataHandaler
    {
        SchoolRegisterContext context;

        public DataHandaler()
        {
            context = new SchoolRegisterContext();
        }

        /// <summary>
        /// Returns all students in a class from the database.
        /// </summary>
        /// <param name="classID">the said calss</param>
        /// <returns>all students in the said class</returns>
        public student[] GetAllStudentsInClass(int classID)
        {
            return context.students.Where(student => student.class_id == classID).ToArray();
        }

        /// <summary>
        /// Adds a new students to the database
        /// </summary>
        /// <param name="firstName">student's first name</param>
        /// <param name="lastName">students's last name</param>
        /// <param name="classID">students's class ID</param>
        public void AddStudent(string firstName, string lastName, int classID)
        {
            student newStudent = new student();
            newStudent.first_name = firstName;
            newStudent.last_name = lastName;
            newStudent.class_id = classID;
            context.students.Add(newStudent);
        }
    }
}