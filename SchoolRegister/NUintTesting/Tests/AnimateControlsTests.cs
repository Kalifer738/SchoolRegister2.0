using System;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Windows.Forms;
using AnimateControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NUintTesting.Tests
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
            AnimatePositionControl animatePositionControl = new AnimatePositionControl(ctrToBeTested, activeLocation, 0, 1);
            animatePositionControl.MoveToActivePosition();

            Assert.IsTrue(ctrToBeTested.Location == activeLocation);
            if (ctrToBeTested.Location != activeLocation)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AnimateSizeControlResizeToActiveTest()
        {
            Size activeSize = new Size(10, 10);
            AnimateSizeControl animateSizeControl = new AnimateSizeControl(ctrToBeTested, activeSize, 0, true);
            animateSizeControl.ScaleToOriginalSize();
            if (ctrToBeTested.Size != activeSize)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AnimateSizeControlResizeToOriginalTest()
        {
            Size originalSize = ctrToBeTested.Size;
            Size activeSize = new Size(10, 10);
            AnimateSizeControl animateSizeControl = new AnimateSizeControl(ctrToBeTested, activeSize, 0, true);
            animateSizeControl.ScaleToOriginalSize();
            animateSizeControl.ScaleToStartingSize();
            if (ctrToBeTested.Size != originalSize)
            {
                Assert.Fail();
            }
        }

        //Windows forms app doesn't support NUint Testing.
        [TestMethod]
        public void AnimatePositionControlTriggerTest()
        {
            Assert.Fail();
            return;
            //Timer doesn't work and the contorl doesn't get updated
            Point activePosition = new Point(10, 10);
            AnimatePositionControl animatePositionControl = new AnimatePositionControl(ctrToBeTested, activePosition, 0, 1);
            ThreadStart threadStart = new ThreadStart(delegate
            {
                animatePositionControl.Trigger();
            });
            Thread thread = new Thread(threadStart);
            thread.Start();
            thread.Join();

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
