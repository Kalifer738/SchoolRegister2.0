using Display.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Display.Data
{
    class DataHandaler
    {
        SchoolRegisterContext context;
        Settings settings;
        string settingsPath;

        public DataHandaler()
        {
            context = new SchoolRegisterContext();
            settingsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\settings.json";
            GetSettings();
        }

        private void GetSettings()
        {
            if (!File.Exists(settingsPath))
            {
                File.WriteAllText(settingsPath, JsonConvert.SerializeObject(Settings.Default));
            }
            string settingsJson = File.ReadAllText(settingsPath);

            if (settingsJson == null)
            {
                settings = Settings.Default;
            }
            else
            {
                settings = JsonConvert.DeserializeObject<Settings>(settingsJson);
            }
        }

        /// <summary>
        /// Saves all changes to the database and settings.
        /// </summary>
        internal void SaveChanges()
        {
            context.SaveChanges();
            File.WriteAllLines(settingsPath, File.ReadLines(settingsPath).Where(line => line != "dontremovethis").ToList());
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