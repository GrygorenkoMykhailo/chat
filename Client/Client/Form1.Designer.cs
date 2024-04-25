namespace Client
{
    partial class Form1
    {
        /// <summary>
<<<<<<< HEAD
        ///  Required designer variable.
=======
        /// Required designer variable.
>>>>>>> ArDev
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
<<<<<<< HEAD
        ///  Clean up any resources being used.
=======
        /// Clean up any resources being used.
>>>>>>> ArDev
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
<<<<<<< HEAD
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        #endregion
    }
}
=======
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MassageSend = new Button();
            MassegeBox = new TextBox();
            ChatBox = new TextBox();
            SuspendLayout();
            // 
            // MassageSend
            // 
            MassageSend.Location = new Point(713, 415);
            MassageSend.Name = "MassageSend";
            MassageSend.Size = new Size(75, 23);
            MassageSend.TabIndex = 0;
            MassageSend.Text = "Send";
            MassageSend.UseVisualStyleBackColor = true;
            // 
            // MassegeBox
            // 
            MassegeBox.Location = new Point(160, 416);
            MassegeBox.Name = "MassegeBox";
            MassegeBox.Size = new Size(547, 23);
            MassegeBox.TabIndex = 1;
            // 
            // ChatBox
            // 
            ChatBox.Location = new Point(160, 12);
            ChatBox.Multiline = true;
            ChatBox.Name = "ChatBox";
            ChatBox.Size = new Size(628, 387);
            ChatBox.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(ChatBox);
            Controls.Add(MassegeBox);
            Controls.Add(MassageSend);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button MassageSend;
        private TextBox MassegeBox;
        private TextBox ChatBox;
    }
}
>>>>>>> ArDev
