using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.Show();
        }

        //private void LoginButton_Click(Object sender, EventArgs e) 
        //{
            
        //    if (PassField== Hash and UserField == Username)
        //    {



        //        this.Hide();
        //        Form1 main = new Form1();
        //        main.Show();

        //    }   
        //    else
        //    {
        //        this.Hide();
        //        WrPass wapass = new WrPass();
        //        wapass.Show();
        //    }


        //}
    }
}
