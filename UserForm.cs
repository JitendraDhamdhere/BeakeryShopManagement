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
    public partial class UserForm : UserControl
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public UserForm()
        {
            InitializeComponent();
        }
        private static UserForm _instance;

        public static UserForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserForm();
                return _instance;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reset();
            dataGridView1.DataSource = GetData();
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                char ch = ' ';
                if (txtMail.Text != "")
                {
                    ch = txtMail.Text[0];
                }
                if ((txtMail.Text == "") || (txtPassword.Text == "") || (txtName.Text == "") || (txtMobileNumber.Text == ""))
                {
                    MessageBox.Show("Fields Marked * are Mandatory");
                }
                else if (txtMail.Text.Contains(" "))
                {
                    MessageBox.Show("Username Cannot Contain Space");
                }
                else if (!(((Convert.ToInt16(ch) >= 65) && (Convert.ToInt16(ch) <= 90)) || ((Convert.ToInt16(ch) >= 97) && (Convert.ToInt16(ch) <= 122))))
                {
                    MessageBox.Show("Username Must starts with an Alphabet");
                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct = "select Mail from UserTable where Mail=@find";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@find", SqlDbType.NVarChar, 30, "Mail"));
                    cmd.Parameters["@find"].Value = txtMail.Text;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        MessageBox.Show("Mail Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMail.Text = "";
                        txtMail.Focus();

                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                        return;
                    }
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct1 = "select MobileNumber from UserTable where MobileNumber='" + txtMobileNumber.Text + "'";

                    cmd = new SqlCommand(ct1);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        MessageBox.Show("Mobile Number Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMobileNumber.Text = "";
                        txtMobileNumber.Focus();

                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                        return;
                    }
                    con = new SqlConnection(cs.DBConn);
                    con.Open();

                    string cb = "insert into UserTable(Mail,Password,Name,MobileNumber,UType) VALUES ('" + txtMail.Text + "','" + txtPassword.Text + "','" + txtName.Text + "','" + txtMobileNumber.Text + "','M')";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                    MessageBox.Show("Successfully Registered", "User Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    dataGridView1.DataSource = GetData();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Reset()
        {
            txtMail.Text = "";
            txtPassword.Text = "";
            txtName.Text = "";
            txtMobileNumber.Text = "";
            txtMail.Focus();
        }

        private SqlConnection Connection
        {
            get
            {
                SqlConnection ConnectionToFetch = new SqlConnection(cs.DBConn);
                ConnectionToFetch.Open();
                return ConnectionToFetch;
            }
        }

        public DataView GetData()
        {
            var SelectQry = "select * from UserTable where Utype='M' ";//ORDER BY ID desc

            DataSet SampleSource = new DataSet();
            DataView TableView;
            try
            {
                SqlCommand SampleCommand = new SqlCommand();
                var SampleDataAdapter = new SqlDataAdapter();
                SampleCommand.CommandText = SelectQry;
                SampleCommand.Connection = Connection;
                SampleDataAdapter.SelectCommand = SampleCommand;
                SampleDataAdapter.Fill(SampleSource);
                TableView = SampleSource.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TableView;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            Reset();
            dataGridView1.DataSource = GetData();
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                lblId.Text = dr.Cells[0].Value.ToString();
                txtMail.Text = dr.Cells[1].Value.ToString();
                txtPassword.Text = dr.Cells[2].Value.ToString();
                txtName.Text = dr.Cells[3].Value.ToString();
                txtMobileNumber.Text = dr.Cells[4].Value.ToString();
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
                btnSave.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            char ch = ' ';
            if (txtMail.Text != "")
            {
                ch = txtMail.Text[0];
            }
            if ((txtMail.Text == "") || (txtPassword.Text == "") || (txtName.Text == "") || (txtMobileNumber.Text == ""))
            {
                MessageBox.Show("Fields Marked * are Mandatory");
            }
            else if (txtMail.Text.Contains(" "))
            {
                MessageBox.Show("Username Cannot Contain Space");
            }
            else if (!(((Convert.ToInt16(ch) >= 65) && (Convert.ToInt16(ch) <= 90)) || ((Convert.ToInt16(ch) >= 97) && (Convert.ToInt16(ch) <= 122))))
            {
                MessageBox.Show("Username Must starts with an Alphabet");
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "UPDATE UserTable SET Mail=@d2,Password=@d3,Name=@d4,MobileNumber=@d5 Where Id=@d1";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d1", SqlDbType.NVarChar, 50, "Id"));
                cmd.Parameters.Add(new SqlParameter("@d2", SqlDbType.NVarChar, 50, "Mail"));
                cmd.Parameters.Add(new SqlParameter("@d3", SqlDbType.NVarChar, 50, "Password"));
                cmd.Parameters.Add(new SqlParameter("@d4", SqlDbType.NVarChar, 50, "Name"));
                cmd.Parameters.Add(new SqlParameter("@d5", SqlDbType.NVarChar, 50, "MobileNumber"));

                cmd.Parameters["@d1"].Value = lblId.Text;
                cmd.Parameters["@d2"].Value = txtMail.Text;
                cmd.Parameters["@d3"].Value = txtPassword.Text;
                cmd.Parameters["@d4"].Value = txtName.Text;
                cmd.Parameters["@d5"].Value = txtMobileNumber.Text;

                cmd.ExecuteReader();
                MessageBox.Show("Successfully Updated.", "User Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Close();
                Reset();
                btnDelete.Visible = false;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                dataGridView1.DataSource = GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblId.Text == "")
                {
                    MessageBox.Show("Please select User", "User Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    deleterecords();
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    btnSave.Enabled = true;
                    Reset();
                    dataGridView1.DataSource = GetData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void deleterecords()
        {

            try
            {

                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq1 = "delete from UserTable where Id =" + lblId.Text + "";
                cmd = new SqlCommand(cq1);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                btnSave.Enabled = true;
                Reset();
                dataGridView1.DataSource = GetData();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "User Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                else
                {
                    MessageBox.Show("No Record found", "User Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
