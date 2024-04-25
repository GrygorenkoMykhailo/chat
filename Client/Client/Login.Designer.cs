namespace Client
{
    partial class Login
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
            UserField = new TextBox();
            PassField = new TextBox();
            LoginButton = new Button();
            RegisterButton = new Button();
            SuspendLayout();
            // 
            // UserField
            // 
            UserField.Location = new Point(166, 97);
            UserField.Name = "UserField";
            UserField.Size = new Size(201, 23);
            UserField.TabIndex = 0;
            // 
            // PassField
            // 
            PassField.Location = new Point(166, 138);
            PassField.Name = "PassField";
            PassField.Size = new Size(201, 23);
            PassField.TabIndex = 2;
            PassField.UseSystemPasswordChar = true;
            // 
            // LoginButton
            // 
            LoginButton.Cursor = Cursors.Hand;
            LoginButton.Location = new Point(230, 195);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(75, 23);
            LoginButton.TabIndex = 3;
            LoginButton.Text = "Log In";
            LoginButton.UseVisualStyleBackColor = true;
            // 
            // RegisterButton
            // 
            RegisterButton.Cursor = Cursors.Hand;
            RegisterButton.Location = new Point(230, 234);
            RegisterButton.Name = "RegisterButton";
            RegisterButton.Size = new Size(75, 23);
            RegisterButton.TabIndex = 4;
            RegisterButton.Text = "Sing In";
            RegisterButton.UseVisualStyleBackColor = true;
            RegisterButton.Click += RegisterButton_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(530, 280);
            Controls.Add(RegisterButton);
            Controls.Add(LoginButton);
            Controls.Add(PassField);
            Controls.Add(UserField);
            Name = "Login";
            Text = "TCP Chat";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox UserField;
        private TextBox PassField;
        private Button LoginButton;
        private Button RegisterButton;
    }
}