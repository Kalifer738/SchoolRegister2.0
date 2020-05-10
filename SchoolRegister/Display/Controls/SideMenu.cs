using AnimateControl;
using Register;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KonstantinControls
{
    class SideMenu : System.Windows.Forms.Panel
    {
        #region Debugging stuff
        private int debugInt;
        [Category("DebugInt"), Description("An int used for easier debugging as the via the properties")]
        public int DebugInt
        {
            get
            {
                return debugInt;
            }
            set
            {
                debugInt = value;
            }
        }
        #endregion

        private AnimatePositionControl animSideMenu;
        private AnimateSizeControl animClassDropdownMenu;
        private AnimateSizeControl animButtonHover;

        private Label currentClass;
        private Label[] currentClassOptions;
        private Control[] linesBetweenOptions;
        private PictureBox buttonGFX;
        private Control classDropdown;
        private Control[] innerGFX;
        private Control buttonClickArea;

        private Color currentClassColor;
        private int spacingBetweenOptions;


        [Category("CurrentClass"), Description("Text for the currently selected class")]
        public Label CurrentClass
        {
            get
            {
                return currentClass;
            }
            set
            {
                currentClass = value;
                Invalidate();
            }
        }

        [Category("CurrentClass Color")]
        public Color CurrentClassColor
        {
            get
            {
                return currentClassColor;
            }
            set
            {
                currentClassColor = value;
                UpdateColor();
            }
        }

        [Category("Spacing Between Options")]
        public int SpacingBetweenOptions
        {
            get
            {
                return spacingBetweenOptions;
            }
            set
            {
                spacingBetweenOptions = value;
                UpdateSpacingBetweenOptions();
            }
        }


        public SideMenu()
        {
            this.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            this.Resize += SideMenu_SizeChanged;
            this.BackColor = Color.White;

            InicializeCurrentClass();
            InicializebuttonGFX();
            InicializeClassDropdown();
            InicializeInsideBorders();
            InicializeCurrentClassOptions();
            InicializeLinesBetweenOptions();
            InicializeAnimationScripts();
            OrderControls();
        }

        private void OrderControls()
        {
            foreach (var option in currentClassOptions)
            {
                option.SendToBack();
            }
        }

        private void InicializeLinesBetweenOptions()
        {
            linesBetweenOptions = new Control[currentClassOptions.Length];

            for (int line = 0; line < linesBetweenOptions.Length; line++)
            {
                linesBetweenOptions[line] = new Control();
                linesBetweenOptions[line].BackColor = Color.Black;
                linesBetweenOptions[line].Parent = this;
                linesBetweenOptions[line].SendToBack();
            }
        }

        private void InicializeCurrentClassOptions()
        {
            currentClassOptions = new Label[7];

            currentClassOptions[0] = new Label();
            currentClassOptions[0].AutoSize = true;
            currentClassOptions[0].Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
            currentClassOptions[0].Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            currentClassOptions[0].BackColor = Color.Transparent;
            currentClassOptions[0].Parent = this;
            currentClassOptions[0].Location = new Point(CurrentClass.Location.X, innerGFX[0].Location.Y + 8);
            currentClassOptions[0].SendToBack();

            for (int option = 1; option < currentClassOptions.Length; option++)
            {
                currentClassOptions[option] = new Label();
                currentClassOptions[option].AutoSize = true;
                currentClassOptions[option].Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
                currentClassOptions[option].Anchor = (AnchorStyles.Left | AnchorStyles.Top);
                currentClassOptions[option].BackColor = Color.Transparent;
                currentClassOptions[option].Parent = this;
                currentClassOptions[option].SendToBack();
            }

            currentClassOptions[0].Text = "Add Grade";
            currentClassOptions[1].Text = "Remove Grade";
            currentClassOptions[2].Text = "Add Absence";
            currentClassOptions[3].Text = "Remove Absence";
            currentClassOptions[4].Text = "Add Student";
            currentClassOptions[5].Text = "Remove Student";
            currentClassOptions[6].Text = "Exit";

            currentClassOptions[0].Click += AddGrade;
            currentClassOptions[1].Click += RemoveGrade;
            currentClassOptions[2].Click += AddAbsence;
            currentClassOptions[3].Click += RemoveAbsence;
            currentClassOptions[4].Click += AddStudent;
            currentClassOptions[5].Click += RemoveStudent;
            currentClassOptions[6].Click += ExitApplication;
        }

        private void InicializebuttonGFX()
        {
            buttonGFX = new PictureBox();
            buttonGFX.Size = new Size(38, 25);
            buttonGFX.SizeMode = PictureBoxSizeMode.Zoom;
            buttonGFX.Location = new Point(this.Size.Width - 49, 10);
            buttonGFX.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            buttonGFX.Image = SideMenuResources.SideMenuButtonPicture;
            buttonGFX.Parent = this;
            buttonGFX.BackColor = CurrentClassColor;

            buttonGFX.Click += TriggerSideMenu;
            buttonGFX.DoubleClick += TriggerSideMenu;

            buttonClickArea = new Control();
            buttonClickArea.Location = new Point(this.Width, 1);
            buttonClickArea.Size = new Size(this.Width - (this.Width - 59), 44);
            buttonClickArea.BackColor = CurrentClassColor;
            buttonClickArea.Parent = this;
            buttonClickArea.Click += TriggerSideMenu;
            buttonClickArea.DoubleClick += TriggerSideMenu;
        }

        private void InicializeCurrentClass()
        {
            CurrentClass = new Label();
            CurrentClass.Name = "currentClass";
            CurrentClass.Text = "No Class Selected";
            CurrentClass.Parent = this;
            CurrentClass.Location = new Point(11, 10);
            CurrentClass.BackColor = CurrentClassColor;
            CurrentClass.Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
            CurrentClass.Size = new Size(190, 23);
            CurrentClass.AutoEllipsis = true;

            CurrentClass.Click += TriggerDropdownMenu;
            CurrentClass.DoubleClick += TriggerDropdownMenu;
        }

        private void InicializeClassDropdown()
        {
            classDropdown = new Control();
            classDropdown.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            classDropdown.Location = new Point(1, 1);
            classDropdown.Size = new Size(this.Width - 2, 44);
            classDropdown.BackColor = CurrentClassColor;
            classDropdown.Parent = this;
            classDropdown.SendToBack();

            classDropdown.Click += TriggerDropdownMenu;
            classDropdown.DoubleClick += TriggerDropdownMenu;
        }

        private void InicializeInsideBorders()
        {
            //Class border
            innerGFX = new Control[2];
            innerGFX[0] = new Control();
            innerGFX[0].Size = new Size(this.Width, 1);
            innerGFX[0].Location = new Point(0, 45);
            innerGFX[0].BackColor = Color.Black;
            innerGFX[0].Parent = this;
            innerGFX[0].BringToFront();

            //Button border
            innerGFX[1] = new Control();
            innerGFX[1].Size = new Size(1, 45);
            innerGFX[1].Location = new Point(this.Size.Width - 60, 0);
            innerGFX[1].BackColor = Color.Black;
            innerGFX[1].Parent = this;
            innerGFX[1].BringToFront();
        }

        private void InicializeAnimationScripts()
        {
            animSideMenu = new AnimatePositionControl(this, new Point(-200, 0), 0);
            animClassDropdownMenu = new AnimateSizeControl(classDropdown, new Size(258, 463), 0, false);
            animButtonHover = new AnimateSizeControl(buttonGFX, new Size(buttonGFX.Width + 5, buttonGFX.Height + 5), 0, true);

            animSideMenu.OnActiveAnimationEnds += ExtendButtonLabel;
            animSideMenu.OnDefaultAnimationStarts += ShrinkButtonLabel;
        }


        private void TriggerDropdownMenu(object sender, EventArgs e)
        {
            if (animSideMenu.CurrentlyActive && !animSideMenu.CurrentPositionIsTheActiveOne)
            {
                return;
            }
            animClassDropdownMenu.TriggerNow();
            if (animSideMenu.CurrentlyActive && !animSideMenu.CurrentPositionIsTheActiveOne)
            {
                animSideMenu.TriggerNow();
            }
        }

        private void TriggerSideMenu(object sender, EventArgs e)
        {
            animSideMenu.TriggerNow();
            if (!animSideMenu.CurrentPositionIsTheActiveOne && animClassDropdownMenu.CurrentSizeIsTheActiveOne)
            {
                animClassDropdownMenu.TriggerNow();
            }
            else if (animClassDropdownMenu.CurrentlyActive && !animClassDropdownMenu.CurrentSizeIsTheActiveOne)
            {
                animClassDropdownMenu.TriggerNow();
            }
        }

        private void ExtendButtonLabel()
        {
            innerGFX[1].Size = new Size(1, this.Height);
        }

        private void ShrinkButtonLabel()
        {
            innerGFX[1].Size = new Size(1, 45);
        }


        private void UpdateColor()
        {
            buttonClickArea.BackColor = CurrentClassColor;
            CurrentClass.BackColor = CurrentClassColor;
            buttonGFX.BackColor = CurrentClassColor;
            classDropdown.BackColor = CurrentClassColor;
        }

        private void UpdateSpacingBetweenOptions()
        {
            currentClassOptions[0].Location = new Point(CurrentClass.Location.X, innerGFX[0].Location.Y + 8);
            currentClassOptions[0].Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
            for (int currentOption = 1; currentOption < currentClassOptions.Length; currentOption++)
            {
                currentClassOptions[currentOption].Location = new Point(currentClassOptions[currentOption - 1].Location.X, currentClassOptions[currentOption - 1].Location.Y + spacingBetweenOptions);
                linesBetweenOptions[currentOption].Location = new Point(0, currentClassOptions[currentOption].Location.Y);
                linesBetweenOptions[currentOption].Size = new Size(this.Width, 1);
                currentClassOptions[currentOption].Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
            }
        }


        private void ExitApplication(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RemoveStudent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddStudent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RemoveAbsence(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddAbsence(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RemoveGrade(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddGrade(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        private void SideMenu_SizeChanged(object sender, EventArgs e)
        {
            animClassDropdownMenu.ActiveSize = new Size(this.Width - 2, this.Height);
            animClassDropdownMenu.OriginalSize = new Size(this.Width - 2, 44);
            if (animClassDropdownMenu.CurrentSizeIsTheActiveOne)
            {
                animClassDropdownMenu.ScaleToActiveSize();
            }
            else
            {
                animClassDropdownMenu.ScaleToStartingSize();
            }

            //Update inner borders
            innerGFX[0].Size = new Size(this.Width, 1);
            innerGFX[1].Location = new Point(this.Size.Width - 60, 0);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen borderPen = new Pen(Color.Black);

            //Outside Borders
            Point[] borderPoitns = { new Point(0, 0), new Point(Size.Width - 1, 0),
                                     new Point(0, 0), new Point(0, Size.Height - 1),
                                     new Point(0, Size.Height - 1), new Point(Size.Width - 1, Size.Height - 1),
                                     new Point(Size.Width - 1, 0), new Point(Size.Width - 1, Size.Height - 1)
            };
            e.Graphics.DrawLines(borderPen, borderPoitns);
        }
    }
}
