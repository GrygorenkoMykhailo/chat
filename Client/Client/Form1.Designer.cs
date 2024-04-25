using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Client
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

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

        private Button MassageSend;
        private TextBox MassegeBox;
        private TextBox ChatBox;
    }
}
