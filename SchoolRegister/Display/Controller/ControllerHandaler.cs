using Display.Data;
using Display.Display;
using Display.Model;
using Display.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Display.Controller
{
    class ControllerHandaler
    {
        DataHandaler dataHandaler;
        ModelHandaler modelHandaler;
        DisplayHandaler displayHandaler;

        public ControllerHandaler(Form currentForm, bool debugging)
        {
            dataHandaler = new DataHandaler();
            modelHandaler = new ModelHandaler();
            displayHandaler = new DisplayHandaler(currentForm, debugging);
        }

        /// <summary>
        /// Returns all students in a class from the database.
        /// </summary>
        /// <param name="classID">the said calss</param>
        /// <returns>all students in the said class</returns>
        public student[] GetAllStudentsInClass(int classID)
        {
            return dataHandaler.GetAllStudentsInClass(classID);
        }

        public @class GetClassName(int id)
        {
            return dataHandaler.GetClass(id);
        }

        public void SaveChanges()
        {
            dataHandaler.SaveChanges();
        }

        public void SaveRegisterSettings(RegisterSettings settings)
        {
            dataHandaler.SaveRegisterSettings(settings);
        }
    }
}
