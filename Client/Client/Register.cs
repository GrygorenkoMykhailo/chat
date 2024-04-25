using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();

            UserRegisterField.Text = "Enter UserName";
            UserRegisterField.ForeColor = Color.Gray;

            EmailRegisterField.ForeColor = Color.Gray;
            EmailRegisterField.Text = "Enter Email";

            UserRegisterField.Enter += UserRegisterField_Enter;
            UserRegisterField.Leave += UserRegisterField_Leave;

            EmailRegisterField.Enter += EmailRegisterField_Enter;
            EmailRegisterField.Leave += EmailRegisterField_Leave;

        }


        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void UserRegisterField_Enter(object sender, EventArgs e)
        {
            if (UserRegisterField.Text == "Enter UserName")
            {
                UserRegisterField.Text = "";
                UserRegisterField.ForeColor = Color.Black;
            }
        }
        private void UserRegisterField_Leave(object sender, EventArgs e)
        {
            if (UserRegisterField.Text == "")
            {
                UserRegisterField.Text = "Enter UserName";
                UserRegisterField.ForeColor = Color.Gray;
            }
        }

        private void EmailRegisterField_Enter(object sender, EventArgs e)
        {
            if (EmailRegisterField.Text == "Enter Email")
            {
                EmailRegisterField.Text = "";
                EmailRegisterField.ForeColor = Color.Black;
            }
        }

        private void EmailRegisterField_Leave(object sender, EventArgs e)
        {
            if (EmailRegisterField.Text == "")
            {
                EmailRegisterField.Text = "Enter Email";
                EmailRegisterField.ForeColor = Color.Gray;
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
  


