namespace SchoolRegisterRefactored.View
{
    partial class RemoveClassForm
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
            this.classNameLabel = new System.Windows.Forms.Label();
            this.classNameTextBox = new System.Windows.Forms.TextBox();
            this.removeClassButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // classNameLabel
            // 
            this.classNameLabel.AutoSize = true;
            this.classNameLabel.Location = new System.Drawing.Point(35, 35);
            this.classNameLabel.Name = "classNameLabel";
            this.classNameLabel.Size = new System.Drawing.Size(63, 13);
            this.classNameLabel.TabIndex = 11;
            this.classNameLabel.Text = "Class Name";
            this.classNameLabel.Click += new System.EventHandler(this.classNameLabel_Click);
            // 
            // classNameTextBox
            // 
            this.classNameTextBox.Location = new System.Drawing.Point(21, 51);
            this.classNameTextBox.Name = "classNameTextBox";
            this.classNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.classNameTextBox.TabIndex = 10;
            this.classNameTextBox.TextChanged += new System.EventHandler(this.classNameTextBox_TextChanged);
            // 
            // removeClassButton
            // 
            this.removeClassButton.Location = new System.Drawing.Point(21, 219);
            this.removeClassButton.Name = "removeClassButton";
            this.removeClassButton.Size = new System.Drawing.Size(100, 23);
            this.removeClassButton.TabIndex = 9;
            this.removeClassButton.Text = "Remove Class";
            this.removeClassButton.UseVisualStyleBackColor = true;
            this.removeClassButton.Click += new System.EventHandler(this.removeClassButton_Click);
            // 
            // RemoveClassForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 261);
            this.Controls.Add(this.classNameLabel);
            this.Controls.Add(this.classNameTextBox);
            this.Controls.Add(this.removeClassButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RemoveClassForm";
            this.ShowIcon = false;
            this.Text = "Remove Class";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label classNameLabel;
        private System.Windows.Forms.TextBox classNameTextBox;
        private System.Windows.Forms.Button removeClassButton;
    }
}