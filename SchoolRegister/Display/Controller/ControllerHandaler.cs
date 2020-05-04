using Display.Data;
using Display.Display;
using Display.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
