using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Display.Scripts
{
    class Settings
    {
        int lastClass;
        bool openSideMenuOnLunch;

        public static Settings Default 
        {
            get
            {
                Settings settings = new Settings();
                settings.LastClass = 1;
                settings.OpenSideMenuOnLunch = true;
                return settings;
            }
        }
         
        public int LastClass
        {
            get
            {
                return lastClass;
            }
            set
            {
                lastClass = value;
            }
        }

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
    }
}
