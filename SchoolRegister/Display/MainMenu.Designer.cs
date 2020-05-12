namespace Register
{
    partial class MainMenu
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
            this.sideMenu1 = new KonstantinControls.SideMenu();
            this.SuspendLayout();
            // 
            // sideMenu1
            // 
            this.sideMenu1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sideMenu1.BackColor = System.Drawing.Color.White;
            this.sideMenu1.CurrentClassColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sideMenu1.DebugInt = 8;
            this.sideMenu1.EnableLinesBetweenOptions = false;
            this.sideMenu1.Location = new System.Drawing.Point(0, 0);
            this.sideMenu1.Name = "sideMenu1";
            this.sideMenu1.Size = new System.Drawing.Size(260, 465);
            this.sideMenu1.SpacingBetweenOptions = 25;
            this.sideMenu1.TabIndex = 2;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 465);
            this.Controls.Add(this.sideMenu1);
            this.Name = "MainMenu";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private KonstantinControls.SideMenu sideMenu1;
    }
}

