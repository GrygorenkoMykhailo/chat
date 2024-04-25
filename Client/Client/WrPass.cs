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
    public partial class WrPass : Form
    {
        public WrPass()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {

            this.Hide();

            Login login = new Login();
            login.Show();
        }

        private void WrPass_Load(object sender, EventArgs e)
        {

        }
    }


}
