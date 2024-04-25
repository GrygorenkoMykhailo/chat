namespace Client
{
    partial class Register
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
            RegisterNewButton = new Button();
            BackButton = new Button();
            Pass1RegisterField = new TextBox();
            EmailRegisterField = new TextBox();
            Pass2RegisterField = new TextBox();
            UserRegisterField = new TextBox();
            Pass2 = new Label();
            Pass1 = new Label();
            SuspendLayout();
            // 
            // RegisterNewButton
            // 
            RegisterNewButton.Cursor = Cursors.Hand;
            RegisterNewButton.Location = new Point(196, 240);
            RegisterNewButton.Name = "RegisterNewButton";
            RegisterNewButton.Size = new Size(75, 23);
            RegisterNewButton.TabIndex = 0;
            RegisterNewButton.Text = "Sing In";
            RegisterNewButton.UseVisualStyleBackColor = true;
            // 
            // BackButton
            // 
            BackButton.Cursor = Cursors.Hand;
            BackButton.Location = new Point(196, 269);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(75, 23);
            BackButton.TabIndex = 1;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = true;
            BackButton.Click += BackButton_Click;
            // 
            // Pass1RegisterField
            // 
            Pass1RegisterField.Location = new Point(144, 147);
            Pass1RegisterField.Name = "Pass1RegisterField";
            Pass1RegisterField.Size = new Size(174, 23);
            Pass1RegisterField.TabIndex = 2;
            Pass1RegisterField.UseSystemPasswordChar = true;
            // 
            // EmailRegisterField
            // 
            EmailRegisterField.Location = new Point(144, 83);
            EmailRegisterField.Name = "EmailRegisterField";
            EmailRegisterField.Size = new Size(174, 23);
            EmailRegisterField.TabIndex = 3;
            // 
            // Pass2RegisterField
            // 
            Pass2RegisterField.Location = new Point(144, 194);
            Pass2RegisterField.Name = "Pass2RegisterField";
            Pass2RegisterField.Size = new Size(174, 23);
            Pass2RegisterField.TabIndex = 6;
            Pass2RegisterField.UseSystemPasswordChar = true;
            // 
            // UserRegisterField
            // 
            UserRegisterField.Location = new Point(144, 32);
            UserRegisterField.Name = "UserRegisterField";
            UserRegisterField.Size = new Size(174, 23);
            UserRegisterField.TabIndex = 5;
            // 
            // Pass2
            // 
            Pass2.AutoSize = true;
            Pass2.Location = new Point(184, 173);
            Pass2.Name = "Pass2";
            Pass2.Size = new Size(87, 15);
            Pass2.TabIndex = 7;
            Pass2.Text = "Repit Password";
            // 
            // Pass1
            // 
            Pass1.AutoSize = true;
            Pass1.Location = new Point(184, 129);
            Pass1.Name = "Pass1";
            Pass1.Size = new Size(87, 15);
            Pass1.TabIndex = 8;
            Pass1.Text = "Enter Password";
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(477, 304);
            Controls.Add(Pass1);
            Controls.Add(Pass2);
            Controls.Add(UserRegisterField);
            Controls.Add(Pass2RegisterField);
            Controls.Add(EmailRegisterField);
            Controls.Add(Pass1RegisterField);
            Controls.Add(BackButton);
            Controls.Add(RegisterNewButton);
            Name = "Register";
            Text = "Form1";
            Load += Register_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button RegisterNewButton;
        private Button BackButton;
        private TextBox Pass1RegisterField;
        private TextBox EmailRegisterField;
        private TextBox Pass2RegisterField;
        private TextBox UserRegisterField;
        private Label Pass2;
        private Label Pass1;


    }
}