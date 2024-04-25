namespace Client
{
    partial class WrPass
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
            AlertMasseg1 = new Label();
            BackButton = new Button();
            SuspendLayout();
            // 
            // AlertMasseg1
            // 
            AlertMasseg1.AutoSize = true;
            AlertMasseg1.BackColor = SystemColors.Control;
            AlertMasseg1.ForeColor = Color.Black;
            AlertMasseg1.Location = new Point(49, 18);
            AlertMasseg1.Name = "AlertMasseg1";
            AlertMasseg1.Size = new Size(146, 15);
            AlertMasseg1.TabIndex = 0;
            AlertMasseg1.Text = "Wrong Password Try again";
            // 
            // BackButton
            // 
            BackButton.Location = new Point(80, 62);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(75, 23);
            BackButton.TabIndex = 1;
            BackButton.Text = "Try Again";
            BackButton.UseVisualStyleBackColor = true;
            BackButton.Click += BackButton_Click;
            // 
            // WrPass
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(245, 116);
            Controls.Add(BackButton);
            Controls.Add(AlertMasseg1);
            Name = "WrPass";
            Text = "WrongPassword";
            Load += WrPass_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label AlertMasseg1;
        private Button BackButton;
        
    }
}