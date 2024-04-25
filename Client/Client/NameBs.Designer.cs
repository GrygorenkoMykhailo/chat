namespace Client
{
    partial class NameBs
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
            AlertMassege4 = new Label();
            BackButton = new Button();
            SuspendLayout();
            // 
            // AlertMassege4
            // 
            AlertMassege4.AutoSize = true;
            AlertMassege4.Location = new Point(12, 23);
            AlertMassege4.Name = "AlertMassege4";
            AlertMassege4.Size = new Size(285, 15);
            AlertMassege4.TabIndex = 0;
            AlertMassege4.Text = "This UserName is already in use, enter a different one";
            // 
            // BackButton
            // 
            BackButton.Location = new Point(122, 48);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(75, 23);
            BackButton.TabIndex = 1;
            BackButton.Text = "Try Again";
            BackButton.UseVisualStyleBackColor = true;
            BackButton.Click += this.BackButton_Click;
            // 
            // NameBs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(311, 83);
            Controls.Add(BackButton);
            Controls.Add(AlertMassege4);
            Name = "NameBs";
            Text = "UserName Alert";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label AlertMassege4;
        private Button BackButton;
    }
}