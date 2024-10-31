using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryShopManagement
{
    public partial class MemberLogin : Form
    {
        public MemberLogin()
        {
            InitializeComponent();
        }
        //SqlDataAdapter rdr = null;
        DataTable dtable = new DataTable();
      //  SqlConnection con = null;
      //  SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Please enter email id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            try
            {
                SqlConnection myConnection = default(SqlConnection);
                myConnection = new SqlConnection(cs.DBConn);
                SqlCommand myCommand = default(SqlCommand);
                myCommand = new SqlCommand("SELECT Name,Mail,Password FROM UserTable WHERE Mail = @Mail AND Password = @Password And UType='M'", myConnection);
                SqlParameter uName = new SqlParameter("@Mail", SqlDbType.VarChar);
                SqlParameter uPassword = new SqlParameter("@Password", SqlDbType.VarChar);
                uName.Value = txtEmail.Text;
                uPassword.Value = txtPassword.Text;
                myCommand.Parameters.Add(uName);
                myCommand.Parameters.Add(uPassword);
                myCommand.Connection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (myReader.Read() == true)
                {
                    this.Hide();
                    MemberDashboard frm = new MemberDashboard();
                    Global.userName = myReader["Name"].ToString();
                    frm.Show();

                }
                else
                {
                    MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Clear();
                    txtPassword.Clear();
                    txtEmail.Focus();
                }
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MemberForgetPassword forgetPassword = new MemberForgetPassword();
            forgetPassword.Show();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm frm = new LoginForm();
            frm.Show();
        }
    }
}
