using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Display.Scripts
{
    /// <summary>
    /// Represents the object to be serialized and saved on energy independent storage, which object stores settings for the application.
    /// </summary>
    public class RegisterSettings
    {
        bool openSideMenuOnLunch;
        bool loadLastOpenedClassOnLunch;
        string classToLoadName;
        int classToLoadID;
        static RegisterSettings currentSettings;

        /// <summary>
        /// Default settings of the application
        /// </summary>
        public static RegisterSettings Default 
        {
            get
            {
                RegisterSettings settings = new RegisterSettings();
                settings.ClassToLoadID = -1;
                settings.LoadLastOpenedClassOnLunch = true;
                settings.OpenSideMenuOnLunch = true;
                settings.classToLoadName = "None";
                return settings;
            }
        }
       
        /// <summary>
        /// Represents the class's name to load.
        /// </summary>
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

        public int ClassToLoadID
        {
            get
            {
                return classToLoadID;
            }
            set
            {
                if (value <= 0)
                {
                    classToLoadID = -1;
                }
                else
                {
                    classToLoadID = value;
                }
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
