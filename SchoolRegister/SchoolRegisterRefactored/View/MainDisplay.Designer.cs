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
            this.sideMenu = new KonstantinControls.SideMenu();
            this.dataGridDisplay1 = new SchoolRegisterRefactored.Controls.DataGridDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDisplay1)).BeginInit();
            this.SuspendLayout();
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
            // dataGridDisplay1
            // 
            this.dataGridDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridDisplay1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridDisplay1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDisplay1.CurrentClass = null;
            this.dataGridDisplay1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dataGridDisplay1.Location = new System.Drawing.Point(0, 0);
            this.dataGridDisplay1.MultiSelect = false;
            this.dataGridDisplay1.Name = "dataGridDisplay1";
            this.dataGridDisplay1.Size = new System.Drawing.Size(766, 466);
            this.dataGridDisplay1.TabIndex = 2;
            // 
            // MainDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 466);
            this.Controls.Add(this.sideMenu);
            this.Controls.Add(this.dataGridDisplay1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainDisplay";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "School Register";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDisplay1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KonstantinControls.SideMenu sideMenu;
        private Controls.DataGridDisplay dataGridDisplay1;
    }
}