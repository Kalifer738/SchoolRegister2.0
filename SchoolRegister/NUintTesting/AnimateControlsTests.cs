using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnimateControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NUintTesting
{
    [TestClass]
    public class AnimateControlsTests
    {
        Control ctrToBeTested;
        [TestInitialize]
        public void SetUp()
        {
            ctrToBeTested = new Control();
            ctrToBeTested.Size = new Size(0, 0);
            ctrToBeTested.Location = new Point(0, 0);
        }

        [TestMethod]
        public void AnimatePositionControlLocationTest()
        {
            Point activeLocation = new Point(10, 10);
            AnimatePositionControl animatePositionControl = new AnimatePositionControl(ctrToBeTested, activeLocation, 0);
            animatePositionControl.MoveToActivePosition();

            Assert.IsTrue(ctrToBeTested.Location == activeLocation);
            if (ctrToBeTested.Location != activeLocation)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AnimateSizeControlResizeTest()
        {
            Size activeSize = new Size(10, 10);
            AnimateSizeControl animateSizeControl = new AnimateSizeControl(ctrToBeTested, activeSize, 0, true);
            animateSizeControl.ScaleToActiveSize();
            Assert.IsTrue(ctrToBeTested.Size == activeSize);
            if (ctrToBeTested.Size != activeSize)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AnimatePositionControlTriggerTest()
        {
            Point activePosition = new Point(10, 10);
            AnimatePositionControl animatePositionControl = new AnimatePositionControl(ctrToBeTested, activePosition, 0);
            animatePositionControl.Trigger();
            while (animatePositionControl.CurrentlyActive)
            {
                Console.Write(". ");
            }
            Console.WriteLine();
            Console.WriteLine(animatePositionControl.ToString());
            Assert.IsTrue(ctrToBeTested.Location == activePosition);
        }
    }
}
