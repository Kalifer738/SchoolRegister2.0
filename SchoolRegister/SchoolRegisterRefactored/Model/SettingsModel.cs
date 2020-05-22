using Display.Scripts;
using Newtonsoft.Json;
using SchoolRegisterRefactored.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegisterRefactored.Model
{
    class SettingsModel
    {
        RegisterController controller;

        string settingsPath;

        public SettingsModel(RegisterController controller)
        {
            this.controller = controller;
            settingsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\settings.json";
            UpdateSettings();
        }

        private void UpdateSettings()
        {
            RegisterSettings settings;
            if (!File.Exists(settingsPath))
            {
                File.WriteAllText(settingsPath, JsonConvert.SerializeObject(RegisterSettings.Default));
            }
            string settingsJson = File.ReadAllText(settingsPath);

            if (settingsJson == null)
            {
                settings = RegisterSettings.Default;
            }
            else
            {
                settings = JsonConvert.DeserializeObject<RegisterSettings>(settingsJson);
            }
            RegisterSettings.CurrentSettings = settings;
        }

        public void SaveSettings(RegisterSettings settings)
        {
            using (FileStream fs = File.Create(settingsPath)) 
            { 
            
            }
            JsonSerializer json = new JsonSerializer();
            using (StreamWriter fs = new StreamWriter(settingsPath))
            {
                json.Serialize(fs, settings);
            }

            UpdateSettings();
        }
    }
}
