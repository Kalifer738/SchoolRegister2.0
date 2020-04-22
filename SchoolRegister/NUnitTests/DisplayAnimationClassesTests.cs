using NUnit.Framework;
using AnimateControl;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using System;

namespace NUnitTests
{
    public class Tests
    {
        Control ctrToBeTested;

        [SetUp]
        public void Setup()
        {
            Point ctrToBeTestedPosition = new Point(0, 0);
            Size ctrToBeTestedSize = new Size(10, 10);
            ctrToBeTested = new Control();
            ctrToBeTested.Location = ctrToBeTestedPosition;
            ctrToBeTested.Size = ctrToBeTestedSize;
        }

        [Test]
        public void AnimatePositionControlLocationTest()
        {
            Point activeLocation = new Point(10, 10);
            AnimatePositionControl animatePositionControl = new AnimatePositionControl(ctrToBeTested, activeLocation, 0);
            animatePositionControl.MoveToActivePosition();
            if (ctrToBeTested.Location == activeLocation)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}