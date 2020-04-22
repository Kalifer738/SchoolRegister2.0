using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Display.Controller_Interface
{
    /// <summary>
    /// Incase a "interface" is needed to abstract the display class to its basic needs so the controller could access it more easily.
    /// </summary>
    class DisplayHandaler
    {
        public void ShowMessage(string message) => ShowMessage(message, null);
        public void ShowMessage(string message, string header)
        {
            MessageBox.Show(message, header);
        }
    }
}
