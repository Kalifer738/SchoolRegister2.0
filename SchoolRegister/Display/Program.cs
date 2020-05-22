using Display.Controller;
using Display.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register
{
    static class Program
    {
        static bool debugging;
        static Form mainForm;
        static ControllerHandaler registerControllerHandaler = new ControllerHandaler(mainForm, debugging);

        public static bool Debugging
        {
            get
            {
                return debugging;
            }
            private set
            {
                debugging = value;
            }
        }
        
        public static Form MainForm
        {
            get
            {
                return mainForm;
            }
            private set
            {
                mainForm = value;
            }
        }

        public static ControllerHandaler RegisterControllerHandaler
        {
            get
            {
                return registerControllerHandaler;
            }
            private set
            {
                registerControllerHandaler = value;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            debugging = false;
            mainForm = new MainForm();
            Application.Run(mainForm);

            RegisterControllerHandaler.SaveChanges();
        }
    }
}
