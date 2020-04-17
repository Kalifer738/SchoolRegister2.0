using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnimateControl;

namespace KonstantinControls
{
    class SideMenu
    {
        AnimatePositionControl menuOpenCloseMenuAnimation;

        AnimateResizeControl menuButtonGrowShrinkAnimation;

        Panel menu;

        public Panel Menu
        {
            get
            {
                return menu;
            }
            private set
            {
                menu = value;
            }
        }

        Label[] borders;

        Label classButton;
        Control[] otherClassesButtons;
        PictureBox menuButton;

        Label[] currentClassOptions;

        /// <summary>
        /// Creates a side menu on the left, adding it to the form.
        /// </summary>
        /// <param name="currentForm">The form that will receive the side menu</param>
        /// <param name="opened">Is the menu open upton serialization</param>
        public SideMenu(Form currentForm, bool opened)
        {
            InicializePanel(currentForm);
            InicializeBorders(currentForm);
            InicializeClassButton(currentForm);

            menuOpenCloseMenuAnimation = new AnimatePositionControl(menu, new Point(menu.Location.X - (menu.Width - 60), 0), 0);
            menuButtonGrowShrinkAnimation = new AnimateResizeControl(menuButton, new Size(menuButton.Size.Width + 10, menuButton.Size.Height + 10), 0, true);
        }

        private void InicializePanel(Form form)
        {
            menu = new Panel();
            menu.Location = new Point(0, 0);
            menu.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom);
            menu.Size = new Size(260, form.Size.Height - 39);
            menu.Parent = form;
            menu.BackColor = Color.White;
            menu.BringToFront();
            form.Controls.Add(menu);
        }

        private void InicializeBorders(Form currentForm)
        {
            borders = new Label[6];//WHEN YOU ADD MORE LABELS BIX ITT

            Label currentLabel = new Label();

            //Top label
            Label topLabel = new Label();
            topLabel.Name = "topBorderLabel";
            topLabel.BackColor = Color.Black;
            topLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
            topLabel.Location = menu.Location;
            topLabel.Size = new Size(menu.Size.Width, 1);
            borders[0] = topLabel;

            //Bottom label
            Label bottomLabel = new Label();
            bottomLabel.Name = "bottomBorderLabel";
            bottomLabel.BackColor = Color.Black;
            bottomLabel.Anchor = AnchorStyles.Bottom;
            bottomLabel.Location = new Point(menu.Location.X, menu.Height + menu.Location.Y - 1);
            bottomLabel.Size = new Size(menu.Size.Width, 1);
            borders[1] = bottomLabel;

            //Left label
            Label leftLabel = new Label();
            leftLabel.Name = "leftBorderLabel";
            leftLabel.BackColor = Color.Black;
            leftLabel.Anchor = (/*AnchorStyles.Left |*/ AnchorStyles.Bottom | AnchorStyles.Top);
            leftLabel.Location = menu.Location;
            leftLabel.Size = new Size(1, menu.Size.Height);
            borders[2] = leftLabel;

            //Right label
            Label rightLabel = new Label();
            rightLabel.Name = "rightBorderLabel";
            rightLabel.BackColor = Color.Black;
            rightLabel.Anchor = (/*AnchorStyles.Right |*/ AnchorStyles.Bottom | AnchorStyles.Top);
            rightLabel.Location = new Point(menu.Location.X + menu.Width - 1, menu.Location.Y);
            rightLabel.Size = new Size(1, menu.Size.Height);
            borders[3] = rightLabel;

            //Class label
            Label classLabel = new Label();
            classLabel.Name = "classBorderLabel";
            classLabel.BackColor = Color.Black;
            classLabel.Anchor = AnchorStyles.Top;
            classLabel.Location = new Point(menu.Location.X, menu.Location.Y + 42);
            classLabel.Size = new Size(menu.Size.Width, 1);
            borders[4] = classLabel;

            //Button label
            Label buttonLabel = new Label();
            buttonLabel.Name = "buttonBorderLabel";
            buttonLabel.BackColor = Color.Black;
            buttonLabel.Anchor = AnchorStyles.Top;
            buttonLabel.Location = new Point(menu.Location.X + 200, menu.Location.Y);
            buttonLabel.Size = new Size(1, 43);
            borders[5] = buttonLabel;

            foreach (var item in borders)
            {
                currentForm.Controls.Add(item);
                item.Parent = menu;
            }
        }
        private void InicializeClassButton(Form currentForm) 
        {
            menuButton = new PictureBox();
            menuButton.Location = new Point(211, 10);
            menuButton.SizeMode = PictureBoxSizeMode.Zoom;
            menuButton.Size = new Size(38, 25);

            ResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            //menuButton.Image = ((Image)(resources.GetObject("SideMenuButtonPicture.png")));
            MessageBox.Show("SideMenu.cs line 143, fix resource manager in order to add image to the button");

            menuButton.Click += TriggerMenu;
            menuButton.MouseEnter += TriggerResizeMenuButton;
            menuButton.MouseLeave += TriggerResizeMenuButton;

            currentForm.Controls.Add(menuButton);
            menuButton.Parent = menu;
        }

        private void TriggerMenu(object sender, EventArgs e)
        {
            if (!menuOpenCloseMenuAnimation.CurrentlyActive)
            {
                menuOpenCloseMenuAnimation.Trigger();
            }
        }

        private void TriggerResizeMenuButton(object sender, EventArgs e)
        {
            menuButtonGrowShrinkAnimation.TriggerNow();
        }
    }
}
