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
    public partial class CategoryForm : UserControl
    {
     //   SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public CategoryForm()
        {
            InitializeComponent();
        }

        private static CategoryForm _instance;

        public static CategoryForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CategoryForm();
                return _instance;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            rest();
            dataGridView1.DataSource = GetData();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetData();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            rest();
        }

        private void rest()
        {
            txtCategory.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCategory.Text == "")
            {
                MessageBox.Show("Please enter Category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCategory.Focus();
                return;
            }
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "Update CategoryTable set Category='" + txtCategory.Text + "' Where Id='" + lblId.Text + "'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully updated", "Category Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = GetData();
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                rest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCategory.Text == "")
            {
                MessageBox.Show("Please enter Category name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCategory.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "insert into CategoryTable(Category) VALUES ('" + txtCategory.Text + "')";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully Saved", "Category Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = GetData();
                rest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            var SelectQry = "select * from CategoryTable ORDER BY Id asc";

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }
        private void delete_records()
        {
            try
            {
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from CategoryTable where Id='" + lblId.Text + "'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Category Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = GetData();
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    rest();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = GetData();
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    rest();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                lblId.Text = dr.Cells[0].Value.ToString();
                txtCategory.Text = dr.Cells[1].Value.ToString();
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
                btnSave.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
