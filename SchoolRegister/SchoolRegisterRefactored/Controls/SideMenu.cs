using AnimateControl;
using Display;
using Display.Scripts;
using Register;
using SchoolRegisterRefactored.Controller;
using SchoolRegisterRefactored.Display;
using KonstantinControls.Interfaces;
using SchoolRegisterRefactored.Resources;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace KonstantinControls
{
    class SideMenu : System.Windows.Forms.Panel, ICustomControl
    {
            
        #region Variables

        #region Debugging REMOVE BEFORE FINISHING PRODUCT!!!
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
                if (!inicialized)
                {
                    UpdateExamples();
                }
            }
        }
        #endregion

        #region Example Variables
        //Variables to showcase editing in the design view.

        private Label[] exampleOptionLabels;
        private Label exampleleCurrentClassLabel;
        private Control exampleCurrentClassDrodown;
        private Label[] exampleOutsideBorders;
        private Label[] exampleInsideBorders;

        #endregion

        private SettingsForm settingsForm;

        private AnimatePositionControl animSideMenu;
        private AnimateSizeControl animClassDropdownMenu;
        private AnimateSizeControl animButtonHover;

        private Label[] outsideBorders;
        private Label currentClass;
        private Label[] classOptions;
        private Label[] classes;
        private PictureBox buttonGFX;
        private Control classDropdown;
        private Control[] innerGFX;
        private Control buttonClickArea;

        private FontStyle allLabelsFontStyle;
        private Font allLabelsFont;

        private Color currentClassColor;
        private int spacingBetweenOptionsAndClasses;
        private bool inicialized;

        #endregion

        #region Properties

        private Label CurrentClass
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

        [Category("All Labels Font")]
        public Font AllLabelsFont
        {
            get
            {
                return allLabelsFont; 
            }
            set
            {
                allLabelsFont = value;
                UpdateExamples();
                if (inicialized)
                {
                    UpdateFonts();
                }
            }
        }

        [Category("All Labels Font Style")]
        public FontStyle AllLabelsFontStyle
        {
            get
            {
                return allLabelsFontStyle;
            }
            set
            {
                allLabelsFontStyle = value;
                UpdateExamples();
                if (inicialized)
                {
                    UpdateFonts();
                }
            }
        }

        [Category("Current Class Background Color")]
        public Color CurrentClassBackgroundColor
        {
            get
            {
                return currentClassColor;
            }
            set
            {
                currentClassColor = value;
                UpdateExamples();
                if (inicialized)
                {
                    UpdateColor();
                }
            }
        }

        [Category("Spacing Between Options And Classes")]
        public int SpacingBetweenOptionsAndClasses
        {
            get
            {
                return spacingBetweenOptionsAndClasses;
            }
            set
            {
                spacingBetweenOptionsAndClasses = value;
                UpdateExamples();
                if (inicialized)
                {
                    UpdateSpacingBetweenClasses();
                    UpdateSpacingBetweenOptions();
                }
            }
        }

        #endregion

        /// <summary>
        /// A control that sits on the side of the form.
        /// </summary>
        public SideMenu()
        {
            this.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            this.Resize += SideMenu_SizeChanged;
            this.BackColor = Color.White;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - this.Size.Height / 2);
            this.Location = new Point(0, 0);

            settingsForm = new SettingsForm();
            inicialized = false;

            InicializeExampleControls();
        }

        #region Inicializing Methods

        private void InicializeOutsideBorders()
        {
            outsideBorders = new Label[4];
            outsideBorders = outsideBorders.Select(border => border = new Label()).ToArray();

            foreach (Label border in outsideBorders)
            {
                border.Parent = this;
                border.BackColor = Color.Black;
            }
            //Top
            outsideBorders[0].Location = new Point(0, 0);
            outsideBorders[0].Size = new Size(this.Size.Width, 1);

            //Left
            outsideBorders[1].Location = new Point(0, 0);
            outsideBorders[1].Size = new Size(1, this.Size.Height);
            outsideBorders[1].Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);

            //Bottom
            outsideBorders[2].Location = new Point(1, this.Size.Height - 1);
            outsideBorders[2].Size = new Size(this.Size.Width, 1);
            outsideBorders[2].Anchor = AnchorStyles.Bottom;

            //Right
            outsideBorders[3].Location = new Point(this.Size.Width - 1, 1);
            outsideBorders[3].Size = new Size(this.Size.Width, this.Size.Height);
            outsideBorders[3].Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
        }

        private void InicializeClasses()
        {
            string[] classesNames = MainDisplay.RegisterController.GetAllClassesExceptCurrentClass(currentClass.Text);

            classes = new Label[classesNames.Count()];

            classes[0] = new Label();
            classes[0].AutoSize = true;
            classes[0].Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
            classes[0].Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            classes[0].BackColor = Color.Transparent;
            classes[0].Parent = classDropdown;
            classes[0].Location = new Point(CurrentClass.Location.X, innerGFX[0].Location.Y + 8);
            classes[0].SendToBack();

            SetCurrentClass(RegisterSettings.CurrentSettings.ClassToLoadName);
        }

        private void InicializeExampleControls()
        {



            exampleOutsideBorders = Enumerable.Repeat(new Label() { BackColor = Color.Black, Parent = this }, 4).ToArray();
            exampleOutsideBorders[0].Anchor = AnchorStyles.Top;//Top
            exampleOutsideBorders[1].Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);//Left
            exampleOutsideBorders[2].Anchor = AnchorStyles.Bottom;//Bottom
            exampleOutsideBorders[3].Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);//Right

            exampleInsideBorders = Enumerable.Repeat(new Label() { BackColor = Color.Black, Parent = this }, 2).ToArray();
            exampleInsideBorders[0].Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
            exampleInsideBorders[1].Anchor = (AnchorStyles.Left | AnchorStyles.Right);

            exampleCurrentClassDrodown = new Control();
            exampleCurrentClassDrodown.Name = "exampleCurrentClassDrodown";
            exampleCurrentClassDrodown.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            exampleCurrentClassDrodown.Location = new Point(1, 1);
            exampleCurrentClassDrodown.Size = new Size(this.Width + 58, 44);
            exampleCurrentClassDrodown.BackColor = CurrentClassBackgroundColor;
            exampleCurrentClassDrodown.Parent = this;

            exampleleCurrentClassLabel = new Label();
            exampleleCurrentClassLabel.Location = new Point(10, 10);
            exampleleCurrentClassLabel.AutoSize = true;
            exampleleCurrentClassLabel.Text = "Class Example";
            exampleleCurrentClassLabel.BackColor = Color.Transparent;
            exampleleCurrentClassLabel.Parent = exampleCurrentClassDrodown;

            exampleOptionLabels = new Label[4];
            exampleOptionLabels = Enumerable.Repeat(new Label() { AutoSize = true, Anchor = (AnchorStyles.Left | AnchorStyles.Top), BackColor = Color.Transparent, Parent = this }, 4).ToArray();
            exampleOptionLabels.Select(exampleOpt => { exampleOpt.SendToBack(); exampleOpt.Text = "Option Name"; return true; });
        }

        private void InicializeCurrentClass()
        {
            CurrentClass = new Label();
            CurrentClass.Name = "currentClass";
            CurrentClass.Parent = this;
            CurrentClass.Location = new Point(11, 10);
            CurrentClass.BackColor = CurrentClassBackgroundColor;
            CurrentClass.Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
            CurrentClass.Size = new Size(190, 23);
            CurrentClass.AutoEllipsis = true;

            CurrentClass.Click += TriggerDropdownMenu;
            CurrentClass.DoubleClick += TriggerDropdownMenu;
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
            buttonGFX.BackColor = CurrentClassBackgroundColor;

            buttonGFX.Click += TriggerSideMenu;
            buttonGFX.DoubleClick += TriggerSideMenu;

            buttonClickArea = new Control();
            buttonClickArea.Location = new Point(this.Width - 59, 1);
            buttonClickArea.Size = new Size(this.Width - (this.Width - 58), 44);
            buttonClickArea.BackColor = CurrentClassBackgroundColor;
            buttonClickArea.Parent = this;

            buttonClickArea.Click += TriggerSideMenu;
            buttonClickArea.DoubleClick += TriggerSideMenu;
        }

        private void InicializeClassDropdown()
        {
            classDropdown = new Control();
            classDropdown.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            classDropdown.Location = new Point(1, 1);
            classDropdown.Size = new Size(this.Width - 2, 44);
            classDropdown.BackColor = CurrentClassBackgroundColor;
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

        private void InicializeOptions()
        {
            classOptions = new Label[8];

            classOptions[0] = new Label();
            classOptions[0].AutoSize = true;
            classOptions[0].Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
            classOptions[0].Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            classOptions[0].BackColor = Color.Transparent;
            classOptions[0].Parent = this;
            classOptions[0].Location = new Point(CurrentClass.Location.X, innerGFX[0].Location.Y + 8);
            classOptions[0].SendToBack();

            for (int option = 1; option < classOptions.Length; option++)
            {
                classOptions[option] = new Label();
                classOptions[option].AutoSize = true;
                classOptions[option].Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
                classOptions[option].Anchor = (AnchorStyles.Left | AnchorStyles.Top);
                classOptions[option].BackColor = Color.Transparent;
                classOptions[option].Parent = this;
                classOptions[option].SendToBack();
            }

            classOptions[0].Text = "Add Grade";
            classOptions[1].Text = "Add Absence";
            classOptions[2].Text = "Add Student";
            classOptions[3].Text = "Remove Grade";
            classOptions[4].Text = "Remove Absence";
            classOptions[5].Text = "Remove Student";
            classOptions[6].Text = "Settings";
            classOptions[7].Text = "Exit";

            classOptions[0].Click += AddGrade;
            classOptions[1].Click += AddAbsence;
            classOptions[2].Click += AddStudent;
            classOptions[3].Click += RemoveGrade;
            classOptions[4].Click += RemoveAbsence;
            classOptions[5].Click += RemoveStudent;
            classOptions[6].Click += ShowSettings;
            classOptions[7].Click += ExitApplication;
            UpdateSpacingBetweenOptions();
        }

        private void InicializeAnimationScripts()
        {
            animSideMenu = new AnimatePositionControl(this, new Point(-200, 0), 0, 1);
            animClassDropdownMenu = new AnimateSizeControl(classDropdown, new Size(classDropdown.Width, this.Height - 1), 0, false);
            animButtonHover = new AnimateSizeControl(buttonGFX, new Size(buttonGFX.Width + 5, buttonGFX.Height + 5), 0, true);

            animSideMenu.OnActiveAnimationEnds += ExtendButtonLabel;
            animSideMenu.OnOriginalAnimationStarts += ShrinkButtonLabel;
            animClassDropdownMenu.OnOriginalAnimationEnds += DisableClassesLabels;
            animClassDropdownMenu.OnActiveAnimationStarts += EnableClassesLabels;
        }

        private void OrderControls()
        {
            foreach (var option in classOptions)
            {
                option.SendToBack();
            }
        }

        #endregion

        #region GFX methods
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
            if (!animSideMenu.CurrentPositionIsTheActiveOne && animClassDropdownMenu.CurrentSizeIsTheActiveOne && !animClassDropdownMenu.CurrentlyActive)
            {
                animClassDropdownMenu.TriggerNow();
            }
            else if (animClassDropdownMenu.CurrentlyActive && !animClassDropdownMenu.CurrentSizeIsTheActiveOne)
            {
                animClassDropdownMenu.TriggerNow();
            }
        }

        private void EnableClassesLabels()
        {
            foreach (Label @class in classes)
            {
                @class.Visible = true;
            }
        }

        private void DisableClassesLabels()
        {
            foreach (Label @class in classes)
            {
                @class.Visible = false;
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

        private void UpdateFonts()
        {
            foreach (Label option in classOptions)
            {
                option.Font = new Font(allLabelsFont.FontFamily, allLabelsFont.Size, allLabelsFontStyle);
            }
            foreach (Label @class in classes)
            {
                @class.Font = new Font(allLabelsFont.FontFamily, allLabelsFont.Size, allLabelsFontStyle);
            }
            currentClass.Font = new Font(allLabelsFont.FontFamily, allLabelsFont.Size, allLabelsFontStyle);
        }

        private void UpdateExamples()
        {
            #region Update Positions for all controls

            //Class Line
            exampleInsideBorders[0].Size = new Size(this.Width, 1);
            exampleInsideBorders[0].Location = new Point(0, 45 + debugInt);

            //Button Line
            exampleInsideBorders[1].Size = new Size(1, 45);
            exampleInsideBorders[1].Location = new Point(this.Size.Width - 60, 0);

            //Top Outside
            exampleOutsideBorders[0].Location = new Point(0, 0);
            exampleOutsideBorders[0].Size = new Size(this.Size.Width, 1);
            //Left Outside
            exampleOutsideBorders[1].Location = new Point(0, 0);
            exampleOutsideBorders[1].Size = new Size(1, this.Size.Height);
            //Bottom Outside
            exampleOutsideBorders[2].Location = new Point(1, this.Size.Height - 1);
            exampleOutsideBorders[2].Size = new Size(this.Size.Width, 1);
            //Right Outside
            exampleOutsideBorders[3].Location = new Point(this.Size.Width - 1, 1);
            exampleOutsideBorders[3].Size = new Size(1, this.Size.Height);

            exampleOptionLabels[0].Location = new Point(exampleleCurrentClassLabel.Location.X, exampleOutsideBorders[0].Location.Y + 8);

            #endregion

            #region Update Fonts for all labels

            exampleCurrentClassDrodown.BackColor = CurrentClassBackgroundColor;
            exampleleCurrentClassLabel.Font = new Font(allLabelsFont.FontFamily, allLabelsFont.Size, allLabelsFontStyle);
            exampleOptionLabels.Select(x => x.Font = new Font(allLabelsFont.FontFamily, allLabelsFont.Size, allLabelsFontStyle));

            #endregion

            #region Update Z order for all controls

            exampleOutsideBorders.Select(x => { x.BringToFront(); return true; });
            exampleInsideBorders.Select(x => { x.BringToFront(); return true; });

            #endregion
        }

        private void UpdateColor()
        {
            buttonClickArea.BackColor = CurrentClassBackgroundColor;
            CurrentClass.BackColor = CurrentClassBackgroundColor;
            buttonGFX.BackColor = CurrentClassBackgroundColor;
            classDropdown.BackColor = CurrentClassBackgroundColor;
        }

        private void UpdateSpacingBetweenOptions()
        {
            classOptions[0].Location = new Point(CurrentClass.Location.X, innerGFX[0].Location.Y + 8);
            for (int currentOption = 1; currentOption < classOptions.Length; currentOption++)
            {
                classOptions[currentOption].Location = new Point(classOptions[currentOption - 1].Location.X, classOptions[currentOption - 1].Location.Y + spacingBetweenOptionsAndClasses);
            }
        }

        private void UpdateSpacingBetweenClasses()
        {
            for (int currentClass = 1; currentClass < classes.Length; currentClass++)
            {
                classes[currentClass].Location = new Point(classes[currentClass - 1].Location.X, classes[currentClass - 1].Location.Y + spacingBetweenOptionsAndClasses);
            }
        }

        #endregion

        #region Events

        private void ClassLabelClick(object sender, EventArgs e)
        {
            Label senderLabel = (Label)sender;
            MessageBox.Show($"HAHA {senderLabel.Text} ARE LOSERS");
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            if (settingsForm.IsDisposed == true)
            {
                settingsForm = new SettingsForm();
                settingsForm.Inicilize();
            }
            settingsForm.Show();
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
            if (!inicialized)
            {
                return;
            }
            animClassDropdownMenu.ActiveSize = new Size(this.Width - 2, this.Height);
            animClassDropdownMenu.OriginalSize = new Size(this.Width - 2, 44);
            if (animClassDropdownMenu.CurrentSizeIsTheActiveOne)
            {
                animClassDropdownMenu.ScaleToOriginalSize();
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

        #endregion

        /// <summary>
        /// Requared Method to setup the controls. Gets called only once when everything is already inicialized.
        /// </summary>
        public void Start(Form currentForm)
        {
            this.Parent = currentForm;

            InicializeCurrentClass();
            InicializebuttonGFX();
            InicializeClassDropdown();
            InicializeInsideBorders();
            InicializeOptions();
            InicializeAnimationScripts();
            InicializeClasses();
            InicializeOutsideBorders();

            OrderControls();
            DisbleExmaples();

            //Disabiling visibility of classes because they're not visible to the user yet.
            foreach (Label @class in classes)
            {
                @class.Visible = false;
            }

            settingsForm.Inicilize();

            inicialized = true;
        }

        private void SetCurrentClass(string className)
        {
            if (!MainDisplay.RegisterController.DoesClassExist(className))
            {
                MessageBox.Show($"The class \"{className}\" doesn't exist in the database!");
                currentClass.Text = "No Class Selected!";
            }
            else
            {
                currentClass.Text = className;
            }

            string[] classesNames = MainDisplay.RegisterController.GetAllClassesExceptCurrentClass(className);
            classes = new Label[classesNames.Count()];

            classes[0] = new Label();
            classes[0].AutoSize = true;
            classes[0].Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
            classes[0].Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            classes[0].BackColor = Color.Transparent;
            classes[0].Parent = classDropdown;
            classes[0].Text = classesNames[0];
            classes[0].Location = new Point(CurrentClass.Location.X, innerGFX[0].Location.Y + 8);
            classes[0].SendToBack();
            classes[0].Click += ClassLabelClick;
            classes[0].DoubleClick += ClassLabelClick;

            if (classesNames.Count() != 1)
            {
                for (int i = 1; i < classesNames.Count(); i++)
                {
                    classes[i] = new Label();
                    classes[i].AutoSize = true;
                    classes[i].Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
                    classes[i].Anchor = (AnchorStyles.Left | AnchorStyles.Top);
                    classes[i].BackColor = Color.Transparent;
                    classes[i].Parent = classDropdown;
                    classes[i].Text = classesNames[i];
                    classes[i].SendToBack();
                    classes[i].Click += ClassLabelClick;
                    classes[i].DoubleClick += ClassLabelClick;
                }
            }

            UpdateSpacingBetweenClasses();
        }

        /// <summary>
        /// Disabled visibility of every label on the sidemenu, used for debugging purposes.
        /// </summary>
        public void DisableEveryLabel()
        {
            foreach (var item in classes)
            {
                item.Visible = false;
            }
            currentClass.Visible = false;
            foreach (var item in classOptions)
            {
                item.Visible = false;
            }
            buttonGFX.Visible = false;
        }

        /// <summary>
        /// Enables visibility of every label on the sidemenu, used for debugging purposes.
        /// </summary>
        public void EnableEveryLabel()
        {
            foreach (var item in classes)
            {
                item.Visible = true;
            }
            currentClass.Visible = true;
            foreach (var item in classOptions)
            {
                item.Visible = true;
            }
            buttonGFX.Visible = true;
        }

        private void DisbleExmaples()
        {
            exampleCurrentClassDrodown.Dispose();
            exampleleCurrentClassLabel.Dispose();
            exampleOutsideBorders.All(x => { x.Dispose(); return true; });
            exampleOptionLabels.All(x => { x.Dispose(); return true; });
            exampleInsideBorders.All(x => { x.Dispose(); return true; });
        }
    }
}
