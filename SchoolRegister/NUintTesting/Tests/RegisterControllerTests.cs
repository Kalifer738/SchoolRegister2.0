using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolRegisterRefactored.Controller;
using SchoolRegisterRefactored.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NUintTesting.Tests
{
    [TestClass]
    public class RegisterControllerTests
    {
        RegisterController controller;

        [TestInitialize]
        public void SetUp()
        {
            controller = new RegisterController(new MainDisplay());
        }

        [TestMethod]
        public void TestController()
        {
            //No database to test against.
            bool myBool = controller.DoesClassExist("11г");
        }
    }
}
