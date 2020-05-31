namespace SchoolRegisterRefactored.View
{
    partial class AddGradeForm
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
            this.gradeLabel = new System.Windows.Forms.Label();
            this.gradeTextBox = new System.Windows.Forms.TextBox();
            this.addClassButton = new System.Windows.Forms.Button();
            this.studentFistName = new System.Windows.Forms.Label();
            this.fistNameTextBox = new System.Windows.Forms.TextBox();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // gradeLabel
            // 
            this.gradeLabel.AutoSize = true;
            this.gradeLabel.Location = new System.Drawing.Point(51, 132);
            this.gradeLabel.Name = "gradeLabel";
            this.gradeLabel.Size = new System.Drawing.Size(36, 13);
            this.gradeLabel.TabIndex = 14;
            this.gradeLabel.Text = "Grade";
            // 
            // gradeTextBox
            // 
            this.gradeTextBox.Location = new System.Drawing.Point(22, 148);
            this.gradeTextBox.Name = "gradeTextBox";
            this.gradeTextBox.Size = new System.Drawing.Size(100, 20);
            this.gradeTextBox.TabIndex = 13;
            // 
            // addClassButton
            // 
            this.addClassButton.Location = new System.Drawing.Point(22, 211);
            this.addClassButton.Name = "addClassButton";
            this.addClassButton.Size = new System.Drawing.Size(100, 23);
            this.addClassButton.TabIndex = 12;
            this.addClassButton.Text = "Add Grade";
            this.addClassButton.UseVisualStyleBackColor = true;
            this.addClassButton.Click += new System.EventHandler(this.removeClassButton_Click);
            // 
            // studentFistName
            // 
            this.studentFistName.AutoSize = true;
            this.studentFistName.Location = new System.Drawing.Point(41, 9);
            this.studentFistName.Name = "studentFistName";
            this.studentFistName.Size = new System.Drawing.Size(57, 13);
            this.studentFistName.TabIndex = 16;
            this.studentFistName.Text = "First Name";
            // 
            // fistNameTextBox
            // 
            this.fistNameTextBox.Location = new System.Drawing.Point(22, 25);
            this.fistNameTextBox.Name = "fistNameTextBox";
            this.fistNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.fistNameTextBox.TabIndex = 15;
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Location = new System.Drawing.Point(41, 71);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Size = new System.Drawing.Size(58, 13);
            this.lastNameLabel.TabIndex = 18;
            this.lastNameLabel.Text = "Last Name";
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Location = new System.Drawing.Point(22, 87);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.lastNameTextBox.TabIndex = 17;
            // 
            // AddGradeForm
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
            this.Controls.Add(this.addClassButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddGradeForm";
            this.ShowIcon = false;
            this.Text = "Add Grade";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label gradeLabel;
        private System.Windows.Forms.TextBox gradeTextBox;
        private System.Windows.Forms.Button addClassButton;
        private System.Windows.Forms.Label studentFistName;
        private System.Windows.Forms.TextBox fistNameTextBox;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.TextBox lastNameTextBox;
    }
}