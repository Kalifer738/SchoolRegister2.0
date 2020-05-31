using Display.Scripts;
using SchoolRegisterRefactored;
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

        public void Inicialize()
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
            bool classRetrieved = true;
            RegisterSettings settings = new RegisterSettings();
            if (!LoadLastClassCheckBox.Checked)
            {
                string className = (string)ClassToLoadComboBox.SelectedItem;
                @class classToLoad = MainDisplay.RegisterController.GetClass(className);
                if (classToLoad == null)
                {
                    classRetrieved = false;
                }
                else
                {
                    settings.ClassToLoadName = className;
                    settings.ClassToLoadID = classToLoad.id;
                }
            }
            if(!classRetrieved)
            {
                settings.ClassToLoadName = "None";
                ClassToLoadComboBox.SelectedItem = 0;
                ClassToLoadComboBox.SelectedIndex = 0;
            }
            settings.ShowMessages = ShowMessagesLabel.Checked;
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
