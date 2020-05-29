using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolRegisterRefactored;
using SchoolRegisterRefactored.Model;
using SchoolRegisterRefactored.Controller;
using Display.Scripts;
using System.Net.Configuration;

namespace NUintTesting.Tests
{
    [TestClass]
    public class SettingsModelTests
    {
        SettingsModel model;
        RegisterSettings settings;
        [TestInitialize]
        public void Setup()
        {
            settings = new RegisterSettings();
            model = new SettingsModel(null);
        }

        [TestMethod]
        public void SavingAndReadingSettings()
        {
            settings.ClassToLoadID = 1;
            settings.ClassToLoadName = "Test";
            settings.LoadLastOpenedClassOnLunch = false;
            settings.OpenSideMenuOnLunch = true;
            settings.ShowMessages = false;
            model.SaveSettings(settings);

            if (AreSettingsDifferent(settings, RegisterSettings.CurrentSettings))
            {
                Console.WriteLine(RegisterSettings.CurrentSettings.ClassToLoadID);
                Console.WriteLine(RegisterSettings.CurrentSettings.ClassToLoadName);
                Console.WriteLine(RegisterSettings.CurrentSettings.LoadLastOpenedClassOnLunch);
                Console.WriteLine(RegisterSettings.CurrentSettings.OpenSideMenuOnLunch);
                Console.WriteLine(RegisterSettings.CurrentSettings.ShowMessages);
                Console.WriteLine();
                Console.WriteLine(settings.ClassToLoadID);
                Console.WriteLine(settings.ClassToLoadName);
                Console.WriteLine(settings.LoadLastOpenedClassOnLunch);
                Console.WriteLine(settings.OpenSideMenuOnLunch);
                Console.WriteLine(settings.ShowMessages);
                Assert.Fail();
            }
        }

        private bool AreSettingsDifferent(RegisterSettings settings1, RegisterSettings settings2)
        {
            if (settings1.ClassToLoadID == settings2.ClassToLoadID)
            {
                if (settings1.ClassToLoadName == settings2.ClassToLoadName)
                {
                    if (settings1.LoadLastOpenedClassOnLunch == settings2.LoadLastOpenedClassOnLunch)
                    {
                        if (settings1.OpenSideMenuOnLunch == settings2.OpenSideMenuOnLunch)
                        {
                            if (settings1.ShowMessages == settings2.ShowMessages)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }   
    }       
}           
            