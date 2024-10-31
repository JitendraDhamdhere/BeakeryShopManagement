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
    public partial class FeedBackForm : UserControl
    {
      //  SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
       // SqlConnection con = null;
       // SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        private static FeedBackForm _instance;

        public static FeedBackForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FeedBackForm();
                return _instance;
            }
        }
        public FeedBackForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
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
            var SelectQry = "select * from FeedbackTable";

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

        private void FeedBackForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetData();

        }
    }
}
