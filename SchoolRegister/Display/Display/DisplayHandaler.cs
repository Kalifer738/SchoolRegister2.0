using KonstantinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Display.Display
{
    class DisplayHandaler
    {
        Form mainForm;
        SideMenu sideMenu;

        public SideMenu SideMenu
        {
            get
            {
                return sideMenu;
            }
            private set
            {
                sideMenu = value;
            }
        }

        public DisplayHandaler(Form mainMenu)
        {
            this.mainForm = mainMenu;
            AddControls();
        }

        private void AddControls()
        {
            SideMenu = new SideMenu(mainForm, true);
        }
    }
}
