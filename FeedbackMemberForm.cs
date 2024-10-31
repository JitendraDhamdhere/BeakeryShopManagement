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
    public partial class FeedbackMemberForm : UserControl
    {
      //  SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public FeedbackMemberForm()
        {
            InitializeComponent();
        }

        private static FeedbackMemberForm _instance;

        public static FeedbackMemberForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FeedbackMemberForm();
                return _instance;
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                char ch = ' ';
                if (txtSubject.Text != "")
                {
                    ch = txtSubject.Text[0];
                }
                if ((txtSubject.Text == "") || (txtFeedback.Text == "") || (txtName.Text == ""))
                {
                    MessageBox.Show("Fields Marked * are Mandatory");
                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();

                    string cb = "insert into FeedbackTable([Subject],Feedback,UserName,FeedbackDate) VALUES ('" + txtSubject.Text + "','" + txtFeedback.Text + "','" + txtName.Text + "','" + System.DateTime.Now + "')";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                    MessageBox.Show("Feedback Send Successfully", "Feedback Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

            private void Reset()
            {
                txtSubject.Text = "";
                txtFeedback.Text = "";
                txtName.Text = "";
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
    }
