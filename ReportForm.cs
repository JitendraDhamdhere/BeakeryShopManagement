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
    public partial class ReportForm : UserControl
    {
      //  SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public ReportForm()
        {
            InitializeComponent();
        }
        private static ReportForm _instance;

        public static ReportForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ReportForm();
                return _instance;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT * From BillTable WHERE BillDate between @date1 and @date2 order by BillDate", con);
                cmd.Parameters.Add("@date1", SqlDbType.VarChar, 30, "BillDate").Value = dtpInvoiceDateFrom.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.VarChar, 30, "BillDate").Value = dtpInvoiceDateTo.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "BillTable");
                DataGridView1.DataSource = myDataSet.Tables["BillTable"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
