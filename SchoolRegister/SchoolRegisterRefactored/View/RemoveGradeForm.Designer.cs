namespace SchoolRegisterRefactored.View
{
    partial class RemoveGradeForm
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
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.studentFistName = new System.Windows.Forms.Label();
            this.fistNameTextBox = new System.Windows.Forms.TextBox();
            this.gradeLabel = new System.Windows.Forms.Label();
            this.gradeTextBox = new System.Windows.Forms.TextBox();
            this.removeClassButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Location = new System.Drawing.Point(41, 80);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Size = new System.Drawing.Size(57, 13);
            this.lastNameLabel.TabIndex = 25;
            this.lastNameLabel.Text = "First Name";
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Location = new System.Drawing.Point(22, 96);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.lastNameTextBox.TabIndex = 24;
            // 
            // studentFistName
            // 
            this.studentFistName.AutoSize = true;
            this.studentFistName.Location = new System.Drawing.Point(41, 18);
            this.studentFistName.Name = "studentFistName";
            this.studentFistName.Size = new System.Drawing.Size(57, 13);
            this.studentFistName.TabIndex = 23;
            this.studentFistName.Text = "First Name";
            // 
            // fistNameTextBox
            // 
            this.fistNameTextBox.Location = new System.Drawing.Point(22, 34);
            this.fistNameTextBox.Name = "fistNameTextBox";
            this.fistNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.fistNameTextBox.TabIndex = 22;
            // 
            // gradeLabel
            // 
            this.gradeLabel.AutoSize = true;
            this.gradeLabel.Location = new System.Drawing.Point(51, 141);
            this.gradeLabel.Name = "gradeLabel";
            this.gradeLabel.Size = new System.Drawing.Size(36, 13);
            this.gradeLabel.TabIndex = 21;
            this.gradeLabel.Text = "Grade";
            // 
            // gradeTextBox
            // 
            this.gradeTextBox.Location = new System.Drawing.Point(22, 157);
            this.gradeTextBox.Name = "gradeTextBox";
            this.gradeTextBox.Size = new System.Drawing.Size(100, 20);
            this.gradeTextBox.TabIndex = 20;
            // 
            // removeClassButton
            // 
            this.removeClassButton.Location = new System.Drawing.Point(22, 220);
            this.removeClassButton.Name = "removeClassButton";
            this.removeClassButton.Size = new System.Drawing.Size(100, 23);
            this.removeClassButton.TabIndex = 19;
            this.removeClassButton.Text = "Remove Grade";
            this.removeClassButton.UseVisualStyleBackColor = true;
            this.removeClassButton.Click += new System.EventHandler(this.removeClassButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 261);
            this.Controls.Add(this.lastNameLabel);
            this.Controls.Add(this.lastNameTextBox);
            this.Controls.Add(this.studentFistName);
            this.Controls.Add(this.fistNameTextBox);
            this.Controls.Add(this.gradeLabel);
            this.Controls.Add(this.gradeTextBox);
            this.Controls.Add(this.removeClassButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Remove Grade";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.Label studentFistName;
        private System.Windows.Forms.TextBox fistNameTextBox;
        private System.Windows.Forms.Label gradeLabel;
        private System.Windows.Forms.TextBox gradeTextBox;
        private System.Windows.Forms.Button removeClassButton;
    }
}