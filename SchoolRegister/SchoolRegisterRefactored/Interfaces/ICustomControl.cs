using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KonstantinControls.Interfaces
{
    /// <summary>
    /// Represents a interface used for each customly built control, that requares inicialization right before the main form is shown.
    /// </summary>
    interface ICustomControl
    {
        void Start(Form mainForm);
    }
}
