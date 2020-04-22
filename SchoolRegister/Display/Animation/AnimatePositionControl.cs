using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimateControl
{
    class AnimatePositionControl
    {
        private Control controlToBeAnimated;

        private Action onActiveAnimationFinish;
        private Action onStartingAnimationBegin;

        private Point updatedPosition;

        private Point originalLocation;
        private Point activeLocation;

        private bool finishedMovingY;
        private bool finishedMovingX;

        private Timer originalPositionAnimation;
        private Timer activePositionAnimation;

        private bool currentPositionIsTheActiveOne;
        private int startingSpeed;
        private int numberOfTicks;

        private bool currentlyActive;

        public Action OnActiveAnimationFinish
        {
            get 
            {
                return onActiveAnimationFinish;
            }
            set
            {
                onActiveAnimationFinish = value;
            }
        }
        public Action OnStartingAnimationBegin
        {
            get
            {
                return onStartingAnimationBegin;
            }
            set
            {
                onStartingAnimationBegin = value;
            }
        }

        /// <summary>
        /// Current state of the animation.
        /// </summary>
        public bool CurrentPositionIsTheActiveOne 
        {
            get
            {
                return currentPositionIsTheActiveOne;
            }
            private set
            {
                currentPositionIsTheActiveOne = value;
            }
        }

        /// <summary>
        /// Shows wheater the animation is currently running or not.
        /// </summary>
        public bool CurrentlyActive
        {
            get 
            { 
                return currentlyActive; 
            }
            set 
            { 
                currentlyActive = value; 
            }
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
                startingSpeed = value;
            }
        }

        /// <summary>
        /// Move a contorl between its origonal location and the specified one.
        /// </summary>
        /// <param name="ControlToBeAnimated">The contorl to be animated</param>
        /// <param name="ActivePosiiton">The specified location of the control when the animation is active.</param>
        /// <param name="StartingSpeed">The starting speed of the animation.</param>
        public AnimatePositionControl(Control ControlToBeAnimated, Point ActivePosiiton, int StartingSpeed)
        {
            this.controlToBeAnimated = ControlToBeAnimated;
            this.originalLocation = controlToBeAnimated.Location;
            this.activeLocation = ActivePosiiton;
            this.StartingSpeed = StartingSpeed;
            currentPositionIsTheActiveOne = false;

            activePositionAnimation = new Timer();
            activePositionAnimation.Interval = 2;

            originalPositionAnimation = new Timer();
            originalPositionAnimation.Interval = 2;

            bool YSame = false;
            bool XSame = false;

            if (originalLocation.X < ActivePosiiton.X)
            {
                originalPositionAnimation.Tick += MoveNegativeX;
                activePositionAnimation.Tick += MovePositiveX;
            }
            else if (originalLocation.X > ActivePosiiton.X)
            {
                originalPositionAnimation.Tick += MovePositiveX;
                activePositionAnimation.Tick += MoveNegativeX;
            }
            else
            {
                XSame = true;
            }

            if (originalLocation.Y < ActivePosiiton.Y)
            {
                originalPositionAnimation.Tick += MoveNegativeY;
                activePositionAnimation.Tick += MovePositiveY;
            }
            else if (originalLocation.Y > ActivePosiiton.Y)
            {
                originalPositionAnimation.Tick += MovePositiveY;
                activePositionAnimation.Tick += MoveNegativeY;
            }
            else
            {
                YSame = true;
            }

            if (XSame && YSame)
            {
                MessageBox.Show($"CurrentPosition and ActivePosition are the same for item {ControlToBeAnimated.GetType()}\"{ControlToBeAnimated.Name}\"","Animation Warning");
            }
        }

        /// <summary>
        /// Activate the animation and go to the next point.
        /// </summary>
        public void Trigger()
        {
            if (!CurrentlyActive)
            {
                CurrentlyActive = true;
                numberOfTicks = 0;
                finishedMovingX = false;
                finishedMovingY = false;
                if (currentPositionIsTheActiveOne)
                {
                    OnStartingAnimationBegin.Invoke();
                    originalPositionAnimation.Start();
                }
                else
                {
                    activePositionAnimation.Start();
                }
            }
        }

        /// <summary>
        /// Activate the animation and go to the next point even if the animation is currently running.
        /// </summary>
        public void TriggerNow()
        {
            activePositionAnimation.Stop();
            originalPositionAnimation.Stop();
            updatedPosition = controlToBeAnimated.Location;
            CurrentlyActive = false;
            Trigger();
        }

        /// <summary>
        /// Teleport the control to the original position.
        /// </summary>
        public void MoveToActivePosition()
        {
            currentPositionIsTheActiveOne = true;
            controlToBeAnimated.Location = activeLocation;
        }

        /// <summary>
        /// Teleport the contorl to the starting position.
        /// </summary>
        public void MoveToStartingPosition()
        {
            currentPositionIsTheActiveOne = false;
            controlToBeAnimated.Location = originalLocation;
        }

        private void MoveNegativeX(object sender, EventArgs e)
        {
            if (finishedMovingX)
            {
                UpdateControl();
                return;
            }
            updatedPosition = controlToBeAnimated.Location;
            updatedPosition.X = updatedPosition.X - (StartingSpeed + numberOfTicks);

            if (!currentPositionIsTheActiveOne)
            {
                if (updatedPosition.X <= activeLocation.X)
                {
                    updatedPosition = new Point(activeLocation.X, updatedPosition.Y);
                    finishedMovingX = true;
                }
            }
            else
            {
                if (updatedPosition.X <= originalLocation.X)
                { 
                    updatedPosition = new Point(originalLocation.X, updatedPosition.Y);
                    finishedMovingX = true;
                }
            }
            UpdateControl();
        }

        private void MovePositiveX(object sender, EventArgs e)
        {
            if (finishedMovingX)
            {
                return;
            }
            updatedPosition = controlToBeAnimated.Location;
            updatedPosition.X = updatedPosition.X + (StartingSpeed + numberOfTicks);

            if (!currentPositionIsTheActiveOne)
            {
                if (updatedPosition.X >= activeLocation.X)
                {
                    updatedPosition = new Point(activeLocation.X, updatedPosition.Y);
                    finishedMovingX = true;
                }
            }
            else
            {
                if (updatedPosition.X >= originalLocation.X)
                {
                    updatedPosition = new Point(originalLocation.X, updatedPosition.Y);
                    finishedMovingX = true;
                }
            }
            UpdateControl();
        }

        private void MoveNegativeY(object sender, EventArgs e)
        {
            if (finishedMovingY)
            {
                UpdateControl();
                return;
            }
            updatedPosition = controlToBeAnimated.Location;
            updatedPosition.Y = updatedPosition.Y - (StartingSpeed + numberOfTicks);

            if (!currentPositionIsTheActiveOne)
            {
                if (updatedPosition.Y <= activeLocation.Y)
                {
                    updatedPosition = new Point(updatedPosition.X, activeLocation.Y);
                    finishedMovingY = true;
                }
            }
            else
            {
                if (updatedPosition.Y <= originalLocation.Y)
                {
                    updatedPosition = new Point(updatedPosition.X, originalLocation.Y);
                    finishedMovingY = true;
                }
            }
            UpdateControl();
        }

        private void MovePositiveY(object sender, EventArgs e)
        {
            if (finishedMovingY)
            {
                UpdateControl();
                return;
            }
            updatedPosition = controlToBeAnimated.Location;
            updatedPosition.Y = updatedPosition.Y + (StartingSpeed + numberOfTicks);

            if (!currentPositionIsTheActiveOne)
            {
                if (updatedPosition.Y >= activeLocation.Y)
                {
                    updatedPosition = new Point(updatedPosition.X, activeLocation.Y);
                    finishedMovingY = true;
                }
            }
            else
            {
                if (updatedPosition.Y >= originalLocation.Y)
                {
                    updatedPosition = new Point(updatedPosition.X, originalLocation.Y);
                    finishedMovingY = true;
                }
            }
            UpdateControl();
        }

        private void UpdateControl()
        {
            if (updatedPosition == activeLocation && numberOfTicks != 0 && !currentPositionIsTheActiveOne || updatedPosition == originalLocation && numberOfTicks != 0 && currentPositionIsTheActiveOne)
            {
                activePositionAnimation.Stop();
                originalPositionAnimation.Stop();
                currentPositionIsTheActiveOne = !currentPositionIsTheActiveOne;
                CurrentlyActive = false;
                if (currentPositionIsTheActiveOne)
                {
                    OnActiveAnimationFinish.Invoke();
                }
            }
            controlToBeAnimated.Location = updatedPosition;
            numberOfTicks++;
        }
    }
}
