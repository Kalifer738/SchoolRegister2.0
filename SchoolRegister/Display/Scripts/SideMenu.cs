using AnimateControl;
using Display;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KonstantinControls
{
    class SideMenu
    {
        AnimatePositionControl menuOpenCloseMenuAnimation;

        AnimateResizeControl menuButtonGrowShrinkAnimation;

        AnimateResizeControl classSelectionmenu;

        Panel menuPanel;

        Label[] borders;

        Label currentClassGFX;
        Label currentClass;
        Control[] otherClasses;

        PictureBox menuCloseOpenButton;
        Label menuCloseOpenButtonGFX;

        Label[] sideMenuOptions;

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
        /// Creates a side menu on the left, adding it to the form.
        /// </summary>
        /// <param name="currentForm">The form that will receive the side menu</param>
        /// <param name="opened">Is the menu open upton serialization</param>
        public SideMenu(Form currentForm, bool opened)
        {
            InicializePanel(currentForm);
            InicializeBorders();
            InicializeClassButton();
            InicializeSideMenuOptions();
            InicializeCurrentClass();
            SetControlsCorrectOrder();

            menuOpenCloseMenuAnimation = new AnimatePositionControl(menuPanel, new Point(menuPanel.Location.X - (menuPanel.Width - 60), 0), 0);
            menuButtonGrowShrinkAnimation = new AnimateResizeControl(menuCloseOpenButton, new Size(menuCloseOpenButton.Size.Width + 10, menuCloseOpenButton.Size.Height + 10), 0, true);
            classSelectionmenu = new AnimateResizeControl(currentClassGFX, new Size(currentClassGFX.Size.Width, currentClassGFX.Height + 500), 5, false);

            menuOpenCloseMenuAnimation.OnActiveAnimationFinish += ExtendButtonLabel;
            menuOpenCloseMenuAnimation.OnStartingAnimationBegin += CollapseButtonLabel;

            if (!opened)
            {
                menuOpenCloseMenuAnimation.MoveToActivePosition();
            }
        }

        private void CollapseButtonLabel()
        {
            borders[5].Size = new Size(1, 43);
        }

        private void ExtendButtonLabel()
        {
            borders[5].Size = new Size(1, menuPanel.Height);
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
            borders = new Label[6];

            #region Top label
            Label topLabel = new Label();
            topLabel.Name = "topBorderLabel";
            topLabel.BackColor = Color.Black;
            topLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
            topLabel.Location = menuPanel.Location;
            topLabel.Size = new Size(menuPanel.Size.Width, 1);
            borders[0] = topLabel;
            #endregion

            #region Bottom Label
            Label bottomLabel = new Label();
            bottomLabel.Name = "bottomBorderLabel";
            bottomLabel.BackColor = Color.Black;
            bottomLabel.Anchor = AnchorStyles.Bottom;
            bottomLabel.Location = new Point(menuPanel.Location.X, menuPanel.Height + menuPanel.Location.Y - 1);
            bottomLabel.Size = new Size(menuPanel.Size.Width, 1);
            borders[1] = bottomLabel;
            #endregion

            #region Left label
            Label leftLabel = new Label();
            leftLabel.Name = "leftBorderLabel";
            leftLabel.BackColor = Color.Black;
            leftLabel.Anchor = (/*AnchorStyles.Left |*/ AnchorStyles.Bottom | AnchorStyles.Top);
            leftLabel.Location = menuPanel.Location;
            leftLabel.Size = new Size(1, menuPanel.Size.Height);
            borders[2] = leftLabel;
            #endregion

            #region Right label
            Label rightLabel = new Label();
            rightLabel.Name = "rightBorderLabel";
            rightLabel.BackColor = Color.Black;
            rightLabel.Anchor = (/*AnchorStyles.Right |*/ AnchorStyles.Bottom | AnchorStyles.Top);
            rightLabel.Location = new Point(menuPanel.Location.X + menuPanel.Width - 1, menuPanel.Location.Y);
            rightLabel.Size = new Size(1, menuPanel.Size.Height);
            borders[3] = rightLabel;
            #endregion

            #region Class label
            Label classLabel = new Label();
            classLabel.Name = "classBorderLabel";
            classLabel.BackColor = Color.Black;
            classLabel.Anchor = AnchorStyles.Top;
            classLabel.Location = new Point(menuPanel.Location.X, menuPanel.Location.Y + 42);
            classLabel.Size = new Size(menuPanel.Size.Width, 1);
            borders[4] = classLabel;
            #endregion

            #region Button label
            Label buttonLabel = new Label();
            buttonLabel.Name = "buttonBorderLabel";
            buttonLabel.BackColor = Color.Black;
            buttonLabel.Anchor = AnchorStyles.Top;
            buttonLabel.Location = new Point(menuPanel.Location.X + 200, menuPanel.Location.Y);
            buttonLabel.Size = new Size(1, 43);
            borders[5] = buttonLabel;
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

            menuCloseOpenButtonGFX = new Label();
            menuCloseOpenButtonGFX.Location = borders[5].Location;
            menuCloseOpenButtonGFX.Size = new Size(menuPanel.Width - borders[5].Location.X, borders[5].Height);
            menuCloseOpenButtonGFX.BackColor = Color.Orange;

            menuCloseOpenButton.Image = SideMenuResources.SideMenuButtonPicture;

            menuCloseOpenButton.Click += CloseOpenSideMenu;
            menuCloseOpenButton.MouseEnter += ResizeMenuButton;
            menuCloseOpenButton.MouseLeave += ResizeMenuButton;

            menuCloseOpenButtonGFX.Click += CloseOpenSideMenu;
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
        private void InicializeCurrentClass()
        {
            #region GFX
            currentClassGFX = new Label();
            currentClassGFX.Location = MenuPanel.Location;
            currentClassGFX.Size = new Size(MenuPanel.Width, MenuPanel.Location.Y + borders[4].Location.Y);
            currentClassGFX.BackColor = Color.Orange;

            currentClassGFX.Parent = MenuPanel;
            currentClassGFX.SendToBack();
            currentClassGFX.Click += OpenCloseClassSelection;
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
            #endregion


            otherClasses = new Label[1];
            Point labelCurrentLocation = new Point(MenuPanel.Location.X + 11, MenuPanel.Location.Y + 54);
            int spacingBetweenLabelsY = 32;
            Color labelBackColor = Color.Transparent;
            AnchorStyles labelAnchorStyle = (AnchorStyles.Left | AnchorStyles.Top);
            Font labelFont = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
        }

        //Continue to work on this
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

        private void AddGrades(object sender, EventArgs e)
        {
            MessageBox.Show("Adding grades not implemented", "Not Implemented");
        }
        private void AddClassNote(object sender, EventArgs e)
        {
            MessageBox.Show("Adding class notes not implemented", "Not Implemented");
        }
        private void AddStudent(object sender, EventArgs e)
        {
            MessageBox.Show("Adding students not implemented", "Not Implemented");
        }
        private void Settings(object sender, EventArgs e)
        {
            MessageBox.Show("Settings not implemented", "Not Implemented");
        }
        private void ExitApplication(object sender, EventArgs e)
        {
            MessageBox.Show("Exiting is not implemented", "Not Implemented");
        }
    }
}
