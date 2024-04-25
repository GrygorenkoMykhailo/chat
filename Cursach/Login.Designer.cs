namespace Cursach
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                ///fsafsafasf
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LoginButton = new Button();
            RegisterButton = new Button();
            PasswordField = new TextBox();
            UserField = new TextBox();
            SuspendLayout();
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(109, 186);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(114, 23);
            LoginButton.TabIndex = 0;
            LoginButton.Text = "Log In";
            LoginButton.UseVisualStyleBackColor = true;
            // 
            // RegisterButton
            // 
            RegisterButton.Location = new Point(109, 228);
            RegisterButton.Name = "RegisterButton";
            RegisterButton.Size = new Size(114, 23);
            RegisterButton.TabIndex = 1;
            RegisterButton.Text = "Sing In";
            RegisterButton.UseVisualStyleBackColor = true;
            RegisterButton.Click += button2_Click;
            // 
            // PasswordField
            // 
            PasswordField.Location = new Point(85, 119);
            PasswordField.Name = "PasswordField";
            PasswordField.Size = new Size(173, 23);
            PasswordField.TabIndex = 2;
            PasswordField.UseSystemPasswordChar = true;
            PasswordField.TextChanged += textBox1_TextChanged;
            // 
            // UserField
            // 
            UserField.Location = new Point(85, 75);
            UserField.Name = "UserField";
            UserField.Size = new Size(173, 23);
            UserField.TabIndex = 3;
            UserField.TextChanged += UserLogin_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(347, 272);
            Controls.Add(UserField);
            Controls.Add(PasswordField);
            Controls.Add(RegisterButton);
            Controls.Add(LoginButton);
            Name = "Form1";
            Text = "TCP Chat";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button LoginButton;
        private Button RegisterButton;
        private TextBox PasswordField;
        private TextBox UserField;
    }
}
