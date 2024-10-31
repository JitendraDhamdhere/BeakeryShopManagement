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
    public partial class ItemMasterForm : UserControl
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public ItemMasterForm()
        {
            InitializeComponent();
        }
        private static ItemMasterForm _instance;

        public static ItemMasterForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ItemMasterForm();
                return _instance;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            rest();
            AutoIdGeneration();
            dataGridView1.DataSource = GetData();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
        }

        private void AutoIdGeneration()
        {
            int Num = 0;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string OleDb = "SELECT Max(ID+1) FROM ItemMasterTable";
            cmd = new SqlCommand(OleDb);
            cmd.Connection = con;
            if (Convert.IsDBNull(cmd.ExecuteScalar()))
            {
                Num = 1;
                lblId.Text = Convert.ToString(Num);
                txtItemId.Text = Convert.ToString("ITM-" + Num);
            }
            else
            {
                Num = System.Convert.ToInt32((cmd.ExecuteScalar()));
                lblId.Text = Convert.ToString(Num);
                txtItemId.Text = Convert.ToString("ITM-" + Num);
            }
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        private void rest()
        {
            txtItemName.Text = "";
            cmbCategory.Text = "";
            txtPrice.Text = "";
            Autocomplete();
        }

        private void Autocomplete()
        {
            try
            {
                cmbCategory.Items.Clear();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Category) from CategoryTable order by Category";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbCategory.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ItemMasterForm_Load(object sender, EventArgs e)
        {

            AutoIdGeneration();
            dataGridView1.DataSource = GetData();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            rest();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtItemName.Text == "")
            {
                MessageBox.Show("Please enter item name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtItemName.Focus();
                return;
            }
            if (cmbCategory.Text == "")
            {
                MessageBox.Show("Please select category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCategory.Focus();
                return;
            }
            if (txtPrice.Text == "")
            {
                MessageBox.Show("Please enter Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrice.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "insert into ItemMasterTable(ItemId,ItemName,Category,Price) VALUES ('" + txtItemId.Text + "','" + txtItemName.Text + "','" + cmbCategory.Text + "','" + txtPrice.Text + "')";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully saved", "Item Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AutoIdGeneration();
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
            var SelectQry = "select * from ItemMasterTable ORDER BY Id asc";

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                txtItemId.Text = dr.Cells[1].Value.ToString();
                txtItemName.Text = dr.Cells[2].Value.ToString();
                cmbCategory.Text = dr.Cells[3].Value.ToString();
                txtPrice.Text = dr.Cells[4].Value.ToString();
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
            if (txtItemName.Text == "")
            {
                MessageBox.Show("Please enter item name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtItemName.Focus();
                return;
            }
            if (cmbCategory.Text == "")
            {
                MessageBox.Show("Please select category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCategory.Focus();
                return;
            }
            if (txtPrice.Text == "")
            {
                MessageBox.Show("Please enter Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrice.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "Update ItemMasterTable set ItemName='" + txtItemName.Text + "',Category='" + cmbCategory.Text + "',Price='" + txtPrice.Text + "' Where ItemId='" + txtItemId.Text + "'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully updated", "Product Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AutoIdGeneration();
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
                string cq = "delete from ItemMasterTable where PackageCode='" + txtItemId.Text + "'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Item Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AutoIdGeneration();
                    dataGridView1.DataSource = GetData();
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    rest();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AutoIdGeneration();
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
    }
}
