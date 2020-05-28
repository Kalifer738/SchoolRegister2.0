namespace Display
{
    partial class SettingsForm
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
            this.OpenSideMenuCheckBox = new System.Windows.Forms.CheckBox();
            this.ClassToLoadComboBox = new System.Windows.Forms.ComboBox();
            this.ClassToLoadLabel = new System.Windows.Forms.Label();
            this.SaveChangesButton = new System.Windows.Forms.Button();
            this.LoadLastClassCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowMessagesLabel = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // OpenSideMenuCheckBox
            // 
            this.OpenSideMenuCheckBox.AutoSize = true;
            this.OpenSideMenuCheckBox.Location = new System.Drawing.Point(15, 75);
            this.OpenSideMenuCheckBox.Name = "OpenSideMenuCheckBox";
            this.OpenSideMenuCheckBox.Size = new System.Drawing.Size(147, 17);
            this.OpenSideMenuCheckBox.TabIndex = 0;
            this.OpenSideMenuCheckBox.Text = "Open side menu on lunch";
            this.OpenSideMenuCheckBox.UseVisualStyleBackColor = true;
            // 
            // ClassToLoadComboBox
            // 
            this.ClassToLoadComboBox.FormattingEnabled = true;
            this.ClassToLoadComboBox.Location = new System.Drawing.Point(11, 25);
            this.ClassToLoadComboBox.Name = "ClassToLoadComboBox";
            this.ClassToLoadComboBox.Size = new System.Drawing.Size(210, 21);
            this.ClassToLoadComboBox.TabIndex = 1;
            // 
            // ClassToLoadLabel
            // 
            this.ClassToLoadLabel.Location = new System.Drawing.Point(12, 9);
            this.ClassToLoadLabel.Name = "ClassToLoadLabel";
            this.ClassToLoadLabel.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.ClassToLoadLabel.Size = new System.Drawing.Size(210, 13);
            this.ClassToLoadLabel.TabIndex = 2;
            this.ClassToLoadLabel.Text = "Class to load on lunch";
            // 
            // SaveChangesButton
            // 
            this.SaveChangesButton.Location = new System.Drawing.Point(83, 426);
            this.SaveChangesButton.Name = "SaveChangesButton";
            this.SaveChangesButton.Size = new System.Drawing.Size(75, 23);
            this.SaveChangesButton.TabIndex = 3;
            this.SaveChangesButton.Text = "Save All";
            this.SaveChangesButton.UseVisualStyleBackColor = true;
            this.SaveChangesButton.Click += new System.EventHandler(this.SaveChangesButton_Click);
            // 
            // LoadLastClassCheckBox
            // 
            this.LoadLastClassCheckBox.AutoSize = true;
            this.LoadLastClassCheckBox.Location = new System.Drawing.Point(15, 52);
            this.LoadLastClassCheckBox.Name = "LoadLastClassCheckBox";
            this.LoadLastClassCheckBox.Size = new System.Drawing.Size(179, 17);
            this.LoadLastClassCheckBox.TabIndex = 4;
            this.LoadLastClassCheckBox.Text = "Load last opened class on lunch";
            this.LoadLastClassCheckBox.UseVisualStyleBackColor = true;
            this.LoadLastClassCheckBox.CheckedChanged += new System.EventHandler(this.LoadLastClassCheckBox_CheckedChanged);
            // 
            // ShowMessagesLabel
            // 
            this.ShowMessagesLabel.AutoSize = true;
            this.ShowMessagesLabel.Location = new System.Drawing.Point(15, 98);
            this.ShowMessagesLabel.Name = "ShowMessagesLabel";
            this.ShowMessagesLabel.Size = new System.Drawing.Size(104, 17);
            this.ShowMessagesLabel.TabIndex = 5;
            this.ShowMessagesLabel.Text = "Show Messages";
            this.ShowMessagesLabel.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 461);
            this.Controls.Add(this.ShowMessagesLabel);
            this.Controls.Add(this.LoadLastClassCheckBox);
            this.Controls.Add(this.SaveChangesButton);
            this.Controls.Add(this.ClassToLoadLabel);
            this.Controls.Add(this.ClassToLoadComboBox);
            this.Controls.Add(this.OpenSideMenuCheckBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "SettingsPanel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox OpenSideMenuCheckBox;
        private System.Windows.Forms.ComboBox ClassToLoadComboBox;
        private System.Windows.Forms.Label ClassToLoadLabel;
        private System.Windows.Forms.Button SaveChangesButton;
        private System.Windows.Forms.CheckBox LoadLastClassCheckBox;
        private System.Windows.Forms.CheckBox ShowMessagesLabel;
    }
}