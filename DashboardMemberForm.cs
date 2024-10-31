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
    public partial class DashboardMemberForm : UserControl
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();

        public DashboardMemberForm()
        {
            InitializeComponent();
        }

        private static DashboardMemberForm _instance;

        public static DashboardMemberForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DashboardMemberForm();
                return _instance;
            }
        }

        public void Order()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "Select Count(Id) from BillTable";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                    lblOrder.Text = (rdr.GetValue(0).ToString());
                else
                    lblOrder.Text = "0";
                if ((rdr != null))
                    rdr.Close();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Feedback()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "Select Count(Id) from FeedbackTable";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                    lblFeedback.Text = (rdr.GetValue(0).ToString());
                else
                    lblFeedback.Text = "0";
                if ((rdr != null))
                    rdr.Close();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DashboardMemberForm_Load(object sender, EventArgs e)
        {
            Order();
            Feedback();
            dataGridView1.DataSource = GetData();
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
            var SelectQry = "select * from BillTable";

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            Order();
            Feedback();
            dataGridView1.DataSource = GetData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
