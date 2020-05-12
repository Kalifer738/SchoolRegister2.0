using KonstantinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Display.Display
{
    class DisplayHandaler
    {
        Form mainForm;

        public DisplayHandaler(Form mainMenu, bool debugging)
        {
            if (debugging)
            {
                return;
            }
            this.mainForm = mainMenu;
        }

        private void StartControls()
        {

        }
    }
}
