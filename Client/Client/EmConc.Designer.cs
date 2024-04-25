namespace Client
{
    partial class EmConc
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
            AlertMasseg2 = new Label();
            BButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // AlertMasseg2
            // 
            AlertMasseg2.AutoSize = true;
            AlertMasseg2.BackColor = SystemColors.ButtonFace;
            AlertMasseg2.Location = new Point(74, 22);
            AlertMasseg2.Name = "AlertMasseg2";
            AlertMasseg2.Size = new Size(143, 15);
            AlertMasseg2.TabIndex = 0;
            AlertMasseg2.Text = "Email concide Try another";
            // 
            // BButton
            // 
            BButton.Location = new Point(126, 45);
            BButton.Name = "BButton";
            BButton.Size = new Size(75, 23);
            BButton.TabIndex = 0;
            BButton.Text = "Try Again!";
            BButton.UseVisualStyleBackColor = true;
            BButton.Click += BButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 18);
            label1.Name = "label1";
            label1.Size = new Size(293, 15);
            label1.TabIndex = 1;
            label1.Text = "This Email has already been registered, try another one";
            
            // 
            // EmConc
            // 
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(326, 80);
            Controls.Add(label1);
            Controls.Add(BButton);
            Name = "EmConc";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label AlertMasseg2;
        private Button BButton;
        private Label label1;
    }
}