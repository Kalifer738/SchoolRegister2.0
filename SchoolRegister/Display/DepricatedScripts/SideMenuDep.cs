using AnimateControl;
using Register;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KonstantinControls
{
    class SideMenuDep
    {
        Action onAddGrades;
        Action onAddStudent;
        Action onSettings;
        Action onExitApplication;

        readonly AnimatePositionControl menuOpenCloseMenuAnimation;

        readonly AnimateSizeControl menuButtonGrowShrinkAnimation;

        readonly AnimateSizeControl classSelectionmenu;

        Panel menuPanel;

        Control[] borders;

        Control currentClassGFX;
        Label currentClass;
        Control[] otherClasses;

        PictureBox menuCloseOpenButton;
        Control menuCloseOpenButtonGFX;

        Label[] sideMenuOptions;

        public Action OnAddGrades
        {
            get
            {
                return onAddGrades;
            }
            set
            {
                onAddGrades += value;
            }
        }
        public Action OnAddStudent
        {
            get
            {
                return onAddStudent;
            }
            set
            {
                onAddStudent += value;
            }
        }
        public Action OnSettings
        {
            get
            {
                return onSettings;
            }
            set
            {
                onSettings += value;
            }
        }
        public Action OnExitApplication
        {
            get
            {
                return onExitApplication;
            }
            set
            {
                onExitApplication += value;
            }
        }

        /// <summary>
        /// current class label
        /// </summary>
        public Label CurrentClass
        {
            get
            {
                return currentClass;
            }
            set
            {
                currentClass = value;
            }
        }
        /// <summary>
        /// Represents the panel that holds all the controls for the side menu.
        /// </summary>
        public Panel MenuPanel
        {
            get
            {
                return menuPanel;
            }
            private set
            {
                menuPanel = value;
            }
        }

        /// <summary>
        /// The inner and outer boders of the control
        /// </summary>
        public Control[] Borders
        {
            get
            {
                return borders;
            }
            set
            {
                borders = value;
            }
        }

        /// <summary>
        /// Creates a side menu on the left, adding it to the form.
        /// </summary>
        /// <param name="currentForm">The form that will receive the side menu</param>
        /// <param name="opened">Is the menu open upton serialization</param>
        public SideMenuDep(Form currentForm, bool opened)
        {
            InicializePanel(currentForm);
            InicializeBorders();
            InicializeClassButton();
            InicializeSideMenuOptions();
            InicializeCurrentClass();
            SetControlsCorrectOrder();

            menuOpenCloseMenuAnimation = new AnimatePositionControl(menuPanel, new Point(menuPanel.Location.X - (menuPanel.Width - 60), 0), 0);
            menuButtonGrowShrinkAnimation = new AnimateSizeControl(menuCloseOpenButton, new Size(menuCloseOpenButton.Size.Width + 10, menuCloseOpenButton.Size.Height + 10), 0, true);
            classSelectionmenu = new AnimateSizeControl(currentClassGFX, new Size(currentClassGFX.Size.Width, currentClassGFX.Height + 500), 5, false);

            menuOpenCloseMenuAnimation.OnActiveAnimationEnds += ExtendButtonLabel;
            menuOpenCloseMenuAnimation.OnDefaultAnimationStarts += CollapseButtonLabel;

            if (!opened)
            {
                menuOpenCloseMenuAnimation.MoveToActivePosition();
            }
        }

        private void InicializePanel(Form currentForm)
        {
            menuPanel = new Panel();
            menuPanel.Location = new Point(0, 0);
            menuPanel.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom);
            menuPanel.Size = new Size(260, currentForm.Size.Height - 39);
            menuPanel.Parent = currentForm;
            menuPanel.BackColor = Color.White;
            menuPanel.BringToFront();
            currentForm.Controls.Add(menuPanel);
        }
        private void InicializeBorders()
        {
            borders = new Control[6];

            #region Top Control
            Control topControl = new Control();
            topControl.Name = "topBorderControl";
            topControl.BackColor = Color.Black;
            topControl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
            topControl.Location = menuPanel.Location;
            topControl.Size = new Size(menuPanel.Size.Width, 1);
            borders[0] = topControl;
            #endregion

            #region Bottom Control
            Control bottomControl = new Control();
            bottomControl.Name = "bottomBorderControl";
            bottomControl.BackColor = Color.Black;
            bottomControl.Anchor = AnchorStyles.Bottom;
            bottomControl.Location = new Point(menuPanel.Location.X, menuPanel.Height + menuPanel.Location.Y - 1);
            bottomControl.Size = new Size(menuPanel.Size.Width, 1);
            borders[1] = bottomControl;
            #endregion

            #region Left Control
            Control leftControl = new Control();
            leftControl.Name = "leftBorderControl";
            leftControl.BackColor = Color.Black;
            leftControl.Anchor = (/*AnchorStyles.Left |*/ AnchorStyles.Bottom | AnchorStyles.Top);
            leftControl.Location = menuPanel.Location;
            leftControl.Size = new Size(1, menuPanel.Size.Height);
            borders[2] = leftControl;
            #endregion

            #region Right Control
            Control rightControl = new Control();
            rightControl.Name = "rightBorderControl";
            rightControl.BackColor = Color.Black;
            rightControl.Anchor = (/*AnchorStyles.Right |*/ AnchorStyles.Bottom | AnchorStyles.Top);
            rightControl.Location = new Point(menuPanel.Location.X + menuPanel.Width - 1, menuPanel.Location.Y);
            rightControl.Size = new Size(1, menuPanel.Size.Height);
            borders[3] = rightControl;
            #endregion

            #region Class Control
            Control classControl = new Control();
            classControl.Name = "classBorderControl";
            classControl.BackColor = Color.Black;
            classControl.Anchor = AnchorStyles.Top;
            classControl.Location = new Point(menuPanel.Location.X, menuPanel.Location.Y + 42);
            classControl.Size = new Size(menuPanel.Size.Width, 1);
            borders[4] = classControl;
            #endregion

            #region Button Control
            Control buttonControl = new Control();
            buttonControl.Name = "buttonBorderControl";
            buttonControl.BackColor = Color.Black;
            buttonControl.Anchor = AnchorStyles.Top;
            buttonControl.Location = new Point(menuPanel.Location.X + 200, menuPanel.Location.Y);
            buttonControl.Size = new Size(1, 43);
            borders[5] = buttonControl;
            #endregion

            foreach (var item in borders)
            {
                item.Parent = menuPanel;
            }
        }
        private void InicializeClassButton()
        {
            menuCloseOpenButton = new PictureBox();
            menuCloseOpenButton.Location = new Point(211, 10);
            menuCloseOpenButton.SizeMode = PictureBoxSizeMode.Zoom;
            menuCloseOpenButton.Size = new Size(38, 25);
            menuCloseOpenButton.BackColor = Color.Orange;

            menuCloseOpenButtonGFX = new Control();
            menuCloseOpenButtonGFX.Location = borders[5].Location;
            menuCloseOpenButtonGFX.Size = new Size(menuPanel.Width - borders[5].Location.X, borders[5].Height);
            menuCloseOpenButtonGFX.BackColor = Color.Orange;

            menuCloseOpenButton.Image = SideMenuResources.SideMenuButtonPicture;

            menuCloseOpenButton.Click += CloseOpenSideMenu;
            menuCloseOpenButton.DoubleClick += CloseOpenSideMenu;
            menuCloseOpenButton.MouseEnter += ResizeMenuButton;
            menuCloseOpenButton.MouseLeave += ResizeMenuButton;

            menuCloseOpenButtonGFX.Click += CloseOpenSideMenu;
            menuCloseOpenButtonGFX.DoubleClick += CloseOpenSideMenu;
            menuCloseOpenButtonGFX.MouseEnter += ResizeMenuButton;
            menuCloseOpenButtonGFX.MouseLeave += ResizeMenuButton;

            menuCloseOpenButtonGFX.Parent = menuPanel;
            menuCloseOpenButton.Parent = menuPanel;

            menuCloseOpenButtonGFX.SendToBack();
            menuCloseOpenButton.BringToFront();
        }
        private void InicializeSideMenuOptions()
        {
            sideMenuOptions = new Label[5];
            Point labelCurrentLocation = new Point(menuPanel.Location.X + 11, menuPanel.Location.Y + 54);

            int spacingBetweenLabelsY = 32;
            Color labelBackColor = Color.Transparent;
            AnchorStyles labelAnchorStyle = (AnchorStyles.Left | AnchorStyles.Top);
            Font labelFont = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);

            #region AddGradesLabel
            Label addGradesLabel = new Label();
            addGradesLabel.Name = "addGradesLabel";
            addGradesLabel.BackColor = labelBackColor;
            addGradesLabel.Anchor = labelAnchorStyle;
            addGradesLabel.Location = labelCurrentLocation;
            addGradesLabel.AutoSize = true;
            addGradesLabel.Text = "Add Grades";
            addGradesLabel.Font = labelFont;
            labelCurrentLocation.Y += spacingBetweenLabelsY;
            #endregion

            #region Add Class Note
            Label addClassNoteLabel = new Label();
            addClassNoteLabel.Name = "addClassNoteLabel";
            addClassNoteLabel.BackColor = labelBackColor;
            addClassNoteLabel.Anchor = labelAnchorStyle;
            addClassNoteLabel.Location = labelCurrentLocation;
            addClassNoteLabel.AutoSize = true;
            addClassNoteLabel.Text = "Add Class Note";
            addClassNoteLabel.Font = labelFont;
            labelCurrentLocation.Y += spacingBetweenLabelsY;
            #endregion

            #region Add Student Label
            Label addStudentLabel = new Label();
            addStudentLabel.Name = "addStudentLabel";
            addStudentLabel.BackColor = labelBackColor;
            addStudentLabel.Anchor = labelAnchorStyle;
            addStudentLabel.Location = labelCurrentLocation;
            addStudentLabel.AutoSize = true;
            addStudentLabel.Text = "Add Student";
            addStudentLabel.Font = labelFont;
            labelCurrentLocation.Y += spacingBetweenLabelsY;
            #endregion

            #region Settings
            Label settingsLabel = new Label();
            settingsLabel.Name = "settingsLabel";
            settingsLabel.BackColor = labelBackColor;
            settingsLabel.Anchor = labelAnchorStyle;
            settingsLabel.Location = labelCurrentLocation;
            settingsLabel.AutoSize = true;
            settingsLabel.Text = "Settings";
            settingsLabel.Font = labelFont;
            labelCurrentLocation.Y += spacingBetweenLabelsY;
            #endregion

            #region ExitLabel
            Label exitLabel = new Label();
            exitLabel.Name = "exitLabel";
            exitLabel.BackColor = labelBackColor;
            exitLabel.Anchor = labelAnchorStyle;
            exitLabel.Location = labelCurrentLocation;
            exitLabel.AutoSize = true;
            exitLabel.Text = "Exit";
            exitLabel.Font = labelFont;
            labelCurrentLocation.Y += spacingBetweenLabelsY;
            #endregion

            sideMenuOptions[0] = addGradesLabel;
            sideMenuOptions[1] = addClassNoteLabel;
            sideMenuOptions[2] = addStudentLabel;
            sideMenuOptions[3] = settingsLabel;
            sideMenuOptions[4] = exitLabel;

            addGradesLabel.Click += AddGrades;
            addClassNoteLabel.Click += AddClassNote;
            addStudentLabel.Click += AddStudent;
            settingsLabel.Click += Settings;
            exitLabel.Click += ExitApplication;

            foreach (var item in sideMenuOptions)
            {
                item.Parent = menuPanel;
            }
        }
        //Finish the InicializeCurrentClass when you connect the DB
        private void InicializeCurrentClass()
        {
            #region GFX
            currentClassGFX = new Control();
            currentClassGFX.Location = MenuPanel.Location;
            currentClassGFX.Size = new Size(MenuPanel.Width, MenuPanel.Location.Y + borders[4].Location.Y);
            currentClassGFX.BackColor = Color.Orange;

            currentClassGFX.Parent = MenuPanel;
            currentClassGFX.SendToBack();
            currentClassGFX.Click += OpenCloseClassSelection;
            currentClassGFX.DoubleClick += OpenCloseClassSelection;
            #endregion

            #region Current Class
            currentClass = new Label();
            currentClass.Location = new Point(MenuPanel.Location.X + 11, 10);
            currentClass.Name = "currentClassLabel";
            currentClass.BackColor = Color.Transparent;
            currentClass.Text = "No class was retrieved";
            currentClass.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            currentClass.Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
            currentClass.Size = new Size(190, 23);
            currentClass.AutoEllipsis = true;

            currentClass.Parent = currentClassGFX;
            currentClass.BringToFront();
            currentClass.Click += OpenCloseClassSelection;
            currentClass.DoubleClick += OpenCloseClassSelection;
            #endregion


            otherClasses = new Label[1];
            Point labelCurrentLocation = new Point(MenuPanel.Location.X + 11, MenuPanel.Location.Y + 54);
            //int spacingBetweenLabelsY = 32;
            Color labelBackColor = Color.Transparent;
            //AnchorStyles labelAnchorStyle = (AnchorStyles.Left | AnchorStyles.Top);
            Font labelFont = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
        }

        //Fix the order if needed
        private void SetControlsCorrectOrder()
        {
            foreach (var item in sideMenuOptions)
            {
                item.SendToBack();
            }
        }

        private void CloseOpenSideMenu(object sender, EventArgs e)
        {
            menuOpenCloseMenuAnimation.Trigger();
            if (!menuOpenCloseMenuAnimation.CurrentPositionIsTheActiveOne && classSelectionmenu.CurrentSizeIsTheActiveOne)
            {
                OpenCloseClassSelection(this, EventArgs.Empty);
            }
        }

        private void ResizeMenuButton(object sender, EventArgs e)
        {
            menuButtonGrowShrinkAnimation.TriggerNow();
        }

        private void OpenCloseClassSelection(object sender, EventArgs e)
        {
            classSelectionmenu.TriggerNow();
        }

        private void CollapseButtonLabel()
        {
            borders[5].Size = new Size(1, 43);
        }

        private void ExtendButtonLabel()
        {
            borders[5].Size = new Size(1, menuPanel.Height);
        }

        private void AddGrades(object sender, EventArgs e)
        {
            if (OnAddGrades != null)
            {
                OnAddGrades.Invoke();
            }
        }
        private void AddClassNote(object sender, EventArgs e)
        {

        }
        private void AddStudent(object sender, EventArgs e)
        {
            if (OnAddStudent != null)
            {
                OnAddStudent.Invoke();
            }
        }
        private void Settings(object sender, EventArgs e)
        {
            if (OnSettings != null)
            {
                OnSettings.Invoke();
            }
        }
        private void ExitApplication(object sender, EventArgs e)
        {
            if (OnExitApplication != null)
            {
                OnExitApplication.Invoke();
            }
        }
    }
}
