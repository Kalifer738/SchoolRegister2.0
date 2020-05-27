using Display.Scripts;
using SchoolRegisterRefactored.Display;
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

        public void Inicilize()
        {
            this.Size = new Size(250, 500);
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - this.Size.Height / 2);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            LoadSettingsIntoUI();
        }

        private void LoadSettingsIntoUI()
        {

            bool loadLastOpenedClassOnLunch = RegisterSettings.CurrentSettings.LoadLastOpenedClassOnLunch;

            OpenSideMenuCheckBox.Checked = RegisterSettings.CurrentSettings.OpenSideMenuOnLunch;
            LoadLastClassCheckBox.Checked = loadLastOpenedClassOnLunch;
            ClassToLoadComboBox.Items.Add("None");
            if (loadLastOpenedClassOnLunch)
            {
                ClassToLoadComboBox.Enabled = false;
                ClassToLoadComboBox.SelectedIndex = 0;
                ClassToLoadComboBox.SelectedItem = 0;
            }
            
            ClassToLoadComboBox.Items.AddRange(MainDisplay.RegisterController.GetAllClassesNames());
            ClassToLoadComboBox.SelectedItem = RegisterSettings.CurrentSettings.ClassToLoadName;
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            RegisterSettings settings = new RegisterSettings();
            if (!LoadLastClassCheckBox.Checked)
            {
                string className = (string)ClassToLoadComboBox.SelectedItem;
                settings.ClassToLoadID = MainDisplay.RegisterController.GetClass(className).id;
                settings.ClassToLoadName = className;
            }
            else
            {
                settings.ClassToLoadName = "None";
                ClassToLoadComboBox.SelectedItem = 0;
                ClassToLoadComboBox.SelectedIndex = 0;
            }
            settings.OpenSideMenuOnLunch = OpenSideMenuCheckBox.Checked;
            settings.LoadLastOpenedClassOnLunch = LoadLastClassCheckBox.Checked;
            MainDisplay.RegisterController.SaveSettings(settings);
        }

        private void LoadLastClassCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (LoadLastClassCheckBox.Checked)
            {
                ClassToLoadComboBox.Enabled = false;
            }
            else
            {
                ClassToLoadComboBox.Enabled = true;
            }
        }
    }
}
