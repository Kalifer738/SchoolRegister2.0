namespace SchoolRegisterRefactored.Display
{
    partial class MainDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.sideMenu = new KonstantinControls.SideMenu();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(500, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // sideMenu
            // 
            this.sideMenu.AllLabelsFont = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sideMenu.AllLabelsFontStyle = System.Drawing.FontStyle.Bold;
            this.sideMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sideMenu.BackColor = System.Drawing.Color.White;
            this.sideMenu.CurrentClassBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sideMenu.DebugInt = 0;
            this.sideMenu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sideMenu.Location = new System.Drawing.Point(0, 0);
            this.sideMenu.Name = "sideMenu";
            this.sideMenu.Size = new System.Drawing.Size(260, 466);
            this.sideMenu.SpacingBetweenOptionsAndClasses = 25;
            this.sideMenu.TabIndex = 0;
            // 
            // MainDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 466);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sideMenu);
            this.Name = "MainDisplay";
            this.Text = "School Register";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private KonstantinControls.SideMenu sideMenu;
        private System.Windows.Forms.Button button1;
    }
}