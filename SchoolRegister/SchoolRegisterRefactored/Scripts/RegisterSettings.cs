using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Display.Scripts
{
    public class RegisterSettings
    {
        //int lastClassID;
        bool openSideMenuOnLunch;
        bool loadLastOpenedClassOnLunch;
        string classToLoadName;
        static RegisterSettings currentSettings;

        public static RegisterSettings Default 
        {
            get
            {
                RegisterSettings settings = new RegisterSettings();
                //settings.LastClassID = 1;
                settings.LoadLastOpenedClassOnLunch = true;
                settings.OpenSideMenuOnLunch = true;
                settings.classToLoadName = "None";
                return settings;
            }
        }
        
        ///// <summary>
        ///// Represetns the last loaded class.
        ///// </summary>
        //public int LastClassID
        //{
        //    get
        //    {
        //        return lastClassID;
        //    }
        //    set
        //    {
        //        lastClassID = value;
        //    }
        //}

        public string ClassToLoadName
        {
            get
            {
                return classToLoadName;
            }
            set
            {
                classToLoadName = value;
            }
        }

        /// <summary>
        /// Represents wheater to open the last loaded class or to open a specific class on lunch
        /// </summary>
        public bool LoadLastOpenedClassOnLunch
        {
            get
            {
                return loadLastOpenedClassOnLunch;
            }
            set
            {
                loadLastOpenedClassOnLunch = value;
            }
        }

        /// <summary>
        /// Wheater to have the side menu opened or not on lunch.
        /// </summary>
        public bool OpenSideMenuOnLunch
        {
            get
            {
                return openSideMenuOnLunch;
            }
            set
            {
                openSideMenuOnLunch = value;
            }
        }

        /// <summary>
        /// Represents the currently loaded settings.
        /// </summary>
        public static RegisterSettings CurrentSettings
        {
            get
            {
                return currentSettings;
            }
            set
            {
                currentSettings = value;
            }
        }
    }
}
