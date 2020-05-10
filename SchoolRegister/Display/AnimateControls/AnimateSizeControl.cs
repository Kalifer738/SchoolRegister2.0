using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimateControl
{
    public class AnimateSizeControl
    {
        private Control controlToBeAnimated;

        private Action onActiveAnimationStarts;
        private Action onActiveAnimationEnds;
        private Action onDefaultAnimationStarts;
        private Action onDefaultAnimationEnds;

        Point centerPointOfControl;
        Point updatedPoint;
        Size updatedSize;

        Size originalSize;
        Size activeSize;

        private bool finishedScalingW;
        private bool finishedScalingH;

        private Timer originalSizeAnimation;
        private Timer activeSizeAnimation;

        private bool currentSizeIsTheActiveOne;
        private int startingSpeed;
        private int numberOfTicks;

        private bool keepControlCetered;
        private bool currentlyActive;

        public Action OnActiveAnimationStarts
        {
            get
            {
                return onActiveAnimationStarts;
            }
            set
            {
                onActiveAnimationStarts = value;
            }
        }
        public Action OnActiveAnimationEnds
        {
            get
            {
                return onActiveAnimationEnds;
            }
            set
            {
                onActiveAnimationEnds = value;
            }
        }
        public Action OnDefaultAnimationStarts
        {
            get
            {
                return onDefaultAnimationStarts;
            }
            set
            {
                onDefaultAnimationStarts = value;
            }
        }
        public Action OnDefaultAnimationEnds
        {
            get
            {
                return onDefaultAnimationEnds;
            }
            set
            {
                onDefaultAnimationEnds = value;
            }
        }

        /// <summary>
        /// Current state of the animation.
        /// </summary>
        public bool CurrentSizeIsTheActiveOne
        {
            get
            {
                return currentSizeIsTheActiveOne;
            }
            private set
            {
                currentSizeIsTheActiveOne = value;
            }
        }

        /// <summary>
        /// Shows wheater the animation is currently running or not.
        /// </summary>
        public bool CurrentlyActive
        {
            get { return currentlyActive; }
            set { currentlyActive = value; }
        }

        /// <summary>
        /// The starting speed of the control.
        /// </summary>
        public int StartingSpeed
        {
            get
            {
                return startingSpeed;
            }
            set
            {
                if (value < 0)
                {
                    MessageBox.Show($"Animation speed cannot be negative ({value}), setting animation speed to 0 for {controlToBeAnimated.ToString()}");
                }
                startingSpeed = value;
            }
        }

        /// <summary>
        /// Size of the control when the animation is active
        /// </summary>
        public Size ActiveSize
        {
            get
            {
                return activeSize;
            }
            set
            {
                activeSize = value;
            }
        }

        /// <summary>
        /// Size of the control when the animation isn't ative
        /// </summary>
        public Size OriginalSize
        {
            get
            {
                return originalSize;
            }
            set
            {
                originalSize = value;
            }
        }

        /// <summary>
        /// Resize a contorl between its origonal size and the specified one.
        /// </summary>
        /// <param name="ControlToBeAnimated">The contorl to be animated.</param>
        /// <param name="ActiveSize">The specified size of the control when the animation is active.</param>
        /// <param name="StartingSpeed">The starting speed of the animation.</param>
        /// <param name="KeepControlCetered">Wheater to try and keep the contorl in the same positon as its being resized</param>
        public AnimateSizeControl(Control ControlToBeAnimated, Size ActiveSize, int StartingSpeed, bool KeepControlCetered)
        {
            controlToBeAnimated = ControlToBeAnimated;
            activeSize = ActiveSize;
            this.StartingSpeed = StartingSpeed;
            originalSize = controlToBeAnimated.Size;
            activeSize = ActiveSize;
            keepControlCetered = KeepControlCetered;

            activeSizeAnimation = new Timer();
            activeSizeAnimation.Interval = 2;

            originalSizeAnimation = new Timer();
            originalSizeAnimation.Interval = 10;

            bool WSame = false;
            bool HSame = false;

            if (originalSize.Width < activeSize.Width)
            {
                originalSizeAnimation.Tick += ScaleNegativeW;
                activeSizeAnimation.Tick += ScalePositiveW;
            }
            else if (originalSize.Width > activeSize.Width)
            {
                originalSizeAnimation.Tick += ScalePositiveW;
                activeSizeAnimation.Tick += ScaleNegativeW;
            }
            else
            {
                WSame = true;
            }

            if (originalSize.Height < activeSize.Height)
            {
                originalSizeAnimation.Tick += ScaleNegativeH;
                activeSizeAnimation.Tick += ScalePositiveH;
            }
            else if (originalSize.Height > activeSize.Height)
            {
                originalSizeAnimation.Tick += ScalePositiveH;
                activeSizeAnimation.Tick += ScaleNegativeH;
            }
            else
            {
                HSame = true;
            }

            if (KeepControlCetered)
            {
                centerPointOfControl = new Point(controlToBeAnimated.Location.X + (controlToBeAnimated.Size.Width / 2), controlToBeAnimated.Location.Y + (controlToBeAnimated.Size.Height / 2));
            }

            originalSizeAnimation.Tick += UpdateControl;
            activeSizeAnimation.Tick += UpdateControl;

            if (WSame && HSame)
            {
                MessageBox.Show($"CurrentSize and ActiveSize are the same for item {ControlToBeAnimated.GetType()}\"{ControlToBeAnimated.Name}\"", "Animation Warning");
                Environment.Exit(1);
            }
        }


        /// <summary>
        /// Activate the animation and scale to the next size.
        /// </summary>
        public void Trigger()
        {
            if (!currentlyActive)
            {
                CurrentlyActive = true;
                numberOfTicks = 0;
                finishedScalingW = false;
                finishedScalingH = false;
                updatedSize = controlToBeAnimated.Size;
                if (keepControlCetered)
                {
                    updatedPoint = controlToBeAnimated.Location;
                }
                if (currentSizeIsTheActiveOne)
                {
                    originalSizeAnimation.Start();
                }
                else
                {
                    activeSizeAnimation.Start();
                }
            }
        }

        /// <summary>
        /// Activate the animation and scale to the next size even if the animation is currently running.
        /// </summary>
        public void TriggerNow()
        {
            if (CurrentlyActive)
            {
                activeSizeAnimation.Stop();
                originalSizeAnimation.Stop();
                currentSizeIsTheActiveOne = !currentSizeIsTheActiveOne;
                CurrentlyActive = false;
            }
            Trigger();
        }

        public void ScaleToActiveSize()
        {
            currentSizeIsTheActiveOne = true;
            controlToBeAnimated.Size = activeSize;
            if (keepControlCetered)
            {
                Point activePoint = new Point(centerPointOfControl.X - (activeSize.Width / 2), centerPointOfControl.Y - (activeSize.Height / 2));
                controlToBeAnimated.Location = activePoint;
            }
        }

        public void ScaleToStartingSize()
        {
            currentSizeIsTheActiveOne = false;
            controlToBeAnimated.Size = originalSize;
            if (keepControlCetered)
            {
                Point startingPoint = new Point(centerPointOfControl.X - (originalSize.Width / 2), centerPointOfControl.Y - (originalSize.Height / 2));
                controlToBeAnimated.Location = startingPoint;
            }
        }

        private void ScaleNegativeW(object sender, EventArgs e)
        {
            if (finishedScalingW)
            {
                return;
            }
            updatedSize.Width = updatedSize.Width - (StartingSpeed + numberOfTicks);

            if (!currentSizeIsTheActiveOne)
            {
                if (updatedSize.Width <= activeSize.Width)
                {
                    updatedSize = new Size(activeSize.Width, updatedSize.Height);
                    finishedScalingW = true;
                }
            }
            else
            {
                if (updatedSize.Width <= originalSize.Width)
                {
                    updatedSize = new Size(originalSize.Width, updatedSize.Height);
                    finishedScalingW = true;
                }
            }
        }

        private void ScalePositiveW(object sender, EventArgs e)
        {
            if (finishedScalingW)
            {
                return;
            }
            updatedSize.Width = updatedSize.Width + (StartingSpeed + numberOfTicks);

            if (!currentSizeIsTheActiveOne)
            {
                if (updatedSize.Width >= activeSize.Width)
                {
                    updatedSize = new Size(activeSize.Width, updatedSize.Height);
                    finishedScalingW = true;
                }
            }
            else
            {
                if (updatedSize.Width >= originalSize.Width)
                {
                    updatedSize = new Size(originalSize.Width, updatedSize.Height);
                    finishedScalingW = true;
                }
            }
        }

        private void ScaleNegativeH(object sender, EventArgs e)
        {
            if (finishedScalingH)
            {
                return;
            }
            updatedSize.Height = updatedSize.Height - (StartingSpeed + numberOfTicks);

            if (!currentSizeIsTheActiveOne)
            {
                if (updatedSize.Height <= activeSize.Height)
                {
                    updatedSize = new Size(updatedSize.Width, activeSize.Height);
                    finishedScalingH = true;
                }
            }
            else
            {
                if (updatedSize.Height <= originalSize.Height)
                {
                    updatedSize = new Size(updatedSize.Width, originalSize.Height);
                    finishedScalingH = true;
                }
            }
        }

        private void ScalePositiveH(object sender, EventArgs e)
        {
            if (finishedScalingH)
            {
                return;
            }
            updatedSize.Height = updatedSize.Height + (StartingSpeed + numberOfTicks);

            if (!currentSizeIsTheActiveOne)
            {
                if (updatedSize.Height >= activeSize.Height)
                {
                    updatedSize = new Size(updatedSize.Width, activeSize.Height);
                    finishedScalingH = true;
                }
            }
            else
            {
                if (updatedSize.Height >= originalSize.Height)
                {
                    updatedSize = new Size(updatedSize.Width, originalSize.Height);
                    finishedScalingH = true;
                }
            }
        }

        private void CenterControl()
        {
            controlToBeAnimated.Location = new Point(centerPointOfControl.X - (controlToBeAnimated.Size.Width / 2), centerPointOfControl.Y - (controlToBeAnimated.Size.Height / 2));
        }

        private void UpdateControl(object sender, EventArgs e)
        {
            if (updatedSize == activeSize && numberOfTicks != 0 && !currentSizeIsTheActiveOne || updatedSize == originalSize && numberOfTicks != 0 && currentSizeIsTheActiveOne)
            {
                activeSizeAnimation.Stop();
                originalSizeAnimation.Stop();
                currentSizeIsTheActiveOne = !currentSizeIsTheActiveOne;
                CurrentlyActive = false;
            }
            controlToBeAnimated.Size = updatedSize;
            if (keepControlCetered)
            {
                CenterControl();
            }
            numberOfTicks++;
        }
    }
}
