using Display.Data;
using Display.Scripts;
using Register;
using Register.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Display
{
    public partial class SettingsForm : Form
    {

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsPanel_Load(object sender, EventArgs e)
        {
            this.Size = new Size(250, 500);
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - this.Size.Height / 2);
            
            OpenSideMenuCheckBox.Checked = RegisterSettings.CurrentSettings.OpenSideMenuOnLunch;
            string[] classesNames = DataHandaler.LoadedClasses.Select(c => c.name).ToArray();
            ClassToLoadComboBox.Items.AddRange(classesNames);
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //RegisterSettings settings = new RegisterSettings();
            //settings.LastClassID = ClassToLoadComboBox.SelectedIndex
            //Program.RegisterControllerHandaler.SaveRegisterSettings();
        }
    }
}
