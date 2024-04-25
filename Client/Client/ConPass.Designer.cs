namespace Client
{
    partial class ConPass
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
            AlertMassege3 = new Label();
            BackButton = new Button();
            SuspendLayout();
            // 
            // AlertMassege3
            // 
            AlertMassege3.AutoSize = true;
            AlertMassege3.Location = new Point(55, 18);
            AlertMassege3.Name = "AlertMassege3";
            AlertMassege3.Size = new Size(113, 15);
            AlertMassege3.TabIndex = 0;
            AlertMassege3.Text = "Password mismatch";
           
            // 
            // BackButton
            // 
            BackButton.Location = new Point(74, 42);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(75, 23);
            BackButton.TabIndex = 1;
            BackButton.Text = "Try Again";
            BackButton.UseVisualStyleBackColor = true;
            BackButton.Click += this.BackButton_Click;
            // 
            // ConPass
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(239, 77);
            Controls.Add(BackButton);
            Controls.Add(AlertMassege3);
            Name = "ConPass";
            Text = "Registration Allert";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label AlertMassege3;
        private Button BackButton;
    }
}