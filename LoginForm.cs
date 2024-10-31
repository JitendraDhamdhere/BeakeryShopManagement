using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryShopManagement
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnMemberLogin_Click(object sender, EventArgs e)
        {
            MemberLogin LF = new MemberLogin();
            LF.Show();
            this.Hide();
        }


        private void btnAdmin_Click(object sender, EventArgs e)
        {
            AdminLogin LF = new AdminLogin();
            LF.Show();
            this.Hide();
        }

        private void btnClose_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
