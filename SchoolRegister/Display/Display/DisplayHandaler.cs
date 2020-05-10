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
        SideMenuDep sideMenu;

        public SideMenuDep SideMenu
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

        public DisplayHandaler(Form mainMenu, bool debugging)
        {
            if (debugging)
            {
                return;
            }
            this.mainForm = mainMenu;
            AddControls();
        }

        private void AddControls()
        {
            SideMenu = new SideMenuDep(mainForm, true);
        }
    }
}
