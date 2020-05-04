using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnimateControl;
using Register.Animation;
using KonstantinControls;
using Display.Display;
using Display.Controller;

namespace Register
{
    public partial class MainMenu : Form
    {
        ControllerHandaler controllerHandaler;

        #region Debugging Variables

        AnimatePositionControl debugAnimationTest;
        #endregion

        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            bool debugging = false;
            controllerHandaler = new ControllerHandaler(this, debugging);
            if (debugging)
            {
                Debug();
            }
        }

        #region Debug Methods

        private void Debug()
        {
            Label informationLabel = new Label();
            informationLabel.Name = "informationLabel";
            informationLabel.Location = new Point(0, 0);
            informationLabel.BackColor = Color.White;
            informationLabel.AutoSize = true;

            Label testLabel = new Label();
            testLabel.Size = new Size(50, 50);
            testLabel.Location = new Point(300, 150);
            testLabel.BackColor = Color.Red;
            testLabel.Click += TriggerViaFormClick;

            this.Click += TriggerViaControlClick;
            this.MouseMove += TriggerViaFormMove;
            this.Controls.Add(testLabel);
            this.Controls.Add(informationLabel);

            debugAnimationTest = new AnimatePositionControl(testLabel, new Point(testLabel.Location.X + 80, testLabel.Location.Y+ 80), 0);
        }

        private void TriggerViaFormMove(object sender, MouseEventArgs e)
        {
            this.Controls["informationLabel"].Text = debugAnimationTest.ToString();
        }

        private void TriggerViaControlClick(object sender, EventArgs e)
        {
            debugAnimationTest.TriggerNow();
        }

        private void TriggerViaFormClick(object sender, EventArgs e)
        {
            
        }
        #endregion
    }
}
