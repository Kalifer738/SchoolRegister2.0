using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimateControl
{
    public class AnimatePositionControl
    {
        #region Variables

        readonly private Control controlToBeAnimated;

        private Action onActiveAnimationStarts;
        private Action onActiveAnimationEnds;
        private Action onDefaultAnimationStarts;
        private Action onDefaultAnimationEnds;

        private Point updatedPosition;

        private Point originalLocation;
        private Point activeLocation;

        private bool finishedMovingY;
        private bool finishedMovingX;

        readonly private Timer originalPositionAnimation;
        readonly private Timer activePositionAnimation;

        private bool currentPositionIsTheActiveOne;
        private int startingSpeed;
        private int currentSpeed;
        private int speedOverTime;

        private bool currentlyActive;

        #endregion

        #region Properties

        /// <summary>
        /// Occurs when the active animations starts.
        /// </summary>
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

        /// <summary>
        /// Occurs when the active animations ends.
        /// </summary>
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

        /// <summary>
        /// Occurs when the original animations starts.
        /// </summary>
        public Action OnOriginalAnimationStarts
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

        /// <summary>
        /// Occurs when the origonal animations ends.
        /// </summary>
        public Action OnOriginalAnimationEnds
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
            private set 
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
        /// The increasing speed of the control over time.
        /// </summary>
        public int SpeedOverTime
        {
            get
            {
                return speedOverTime;
            }
            set
            {
                if (value <= 0)
                {
                    SpeedOverTime = 1;
                }
                else
                {
                    speedOverTime = value;
                }
            }
        }

        #endregion

        /// <summary>
        /// Move a contorl between its origonal location and the specified one.
        /// </summary>
        /// <param name="ControlToBeAnimated">The contorl to be animated</param>
        /// <param name="ActivePosiiton">The specified location of the control when the animation is active.</param>
        /// <param name="StartingSpeed">The starting speed of the animation.</param>
        /// <param name="SpeedOverTime">The increasing speed of the control over time.</param>
        public AnimatePositionControl(Control ControlToBeAnimated, Point ActivePosiiton, int StartingSpeed, int SpeedOverTime)
        {
            this.controlToBeAnimated = ControlToBeAnimated;
            this.originalLocation = controlToBeAnimated.Location;
            this.activeLocation = ActivePosiiton;
            this.StartingSpeed = StartingSpeed;
            this.SpeedOverTime = SpeedOverTime;
            currentPositionIsTheActiveOne = false;

            activePositionAnimation = new Timer();
            activePositionAnimation.Interval = 8;

            originalPositionAnimation = new Timer();
            originalPositionAnimation.Interval = 8;

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
                currentSpeed = 0;
                finishedMovingX = false;
                finishedMovingY = false;
                if (currentPositionIsTheActiveOne)
                {
                    if (OnOriginalAnimationStarts != null)
                    {
                        OnOriginalAnimationStarts.Invoke();
                    }
                    originalPositionAnimation.Start();
                }
                else
                {
                    if (OnActiveAnimationStarts != null)
                    {
                        OnActiveAnimationStarts.Invoke();
                    }
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

        #region Movement Methods

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
                UpdateControlPosition();
                return;
            }
            updatedPosition = controlToBeAnimated.Location;
            updatedPosition.X -= (StartingSpeed + currentSpeed);

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
            UpdateControlPosition();
        }

        private void MovePositiveX(object sender, EventArgs e)
        {
            if (finishedMovingX)
            {
                return;
            }
            updatedPosition = controlToBeAnimated.Location;
            updatedPosition.X += (StartingSpeed + currentSpeed);

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
            UpdateControlPosition();
        }

        private void MoveNegativeY(object sender, EventArgs e)
        {
            if (finishedMovingY)
            {
                UpdateControlPosition();
                return;
            }
            updatedPosition = controlToBeAnimated.Location;
            updatedPosition.Y -= (StartingSpeed + currentSpeed);

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
            UpdateControlPosition();
        }

        private void MovePositiveY(object sender, EventArgs e)
        {
            if (finishedMovingY)
            {
                UpdateControlPosition();
                return;
            }
            updatedPosition = controlToBeAnimated.Location;
            updatedPosition.Y += (StartingSpeed + currentSpeed);

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
            UpdateControlPosition();
        }

        #endregion

        private void UpdateControlPosition()
        {
            if (updatedPosition == activeLocation && currentSpeed != 0 && !currentPositionIsTheActiveOne || updatedPosition == originalLocation && currentSpeed != 0 && currentPositionIsTheActiveOne)
            {
                activePositionAnimation.Stop();
                originalPositionAnimation.Stop();
                currentPositionIsTheActiveOne = !currentPositionIsTheActiveOne;
                CurrentlyActive = false;
                if (currentPositionIsTheActiveOne && onActiveAnimationEnds != null)
                {
                    OnActiveAnimationEnds.Invoke();
                }
                else if (!currentPositionIsTheActiveOne && OnOriginalAnimationEnds != null)
                {
                    OnOriginalAnimationEnds.Invoke();
                }
            }
            controlToBeAnimated.Location = updatedPosition;
            currentSpeed += SpeedOverTime;
        }

        /// <summary>
        /// Returns a string with debugging information about the object.
        /// </summary>
        /// <returns>Debugging Information</returns>
        public override string ToString()
        {
            return $"Positon- X:{updatedPosition.X} Y:{updatedPosition.Y} {Environment.NewLine}" +
                   $"Active Animation Running: {originalPositionAnimation.Enabled} {Environment.NewLine}" +
                   $"Original Animation Running: {activePositionAnimation.Enabled} {Environment.NewLine}" +
                   $"Number of moves: {currentSpeed}";
        }
    }
}
