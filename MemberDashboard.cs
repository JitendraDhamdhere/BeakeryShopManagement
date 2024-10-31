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
    public partial class MemberDashboard : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public MemberDashboard()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            MemberLogin memberLogin = new MemberLogin();
            memberLogin.Show();
        }

        public string Email
        {
            get { return lblEmail.Text; }
            set { lblEmail.Text = Email; }
        }

        public string UserId
        {
            get { return lblUserId.Text; }
            set { lblUserId.Text = UserId; }
        }


        public string UserName
        {
            get { return txtUserName.Text; }
            set { txtUserName.Text = UserName; }
        }

        private void AutoUser()
        {
            MemberLogin MLF = new MemberLogin();
            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Id,Name,MobileNumber,Mail FROM UserTable Where Mail=@d1";
            cmd.Parameters.AddWithValue("@d1", lblEmail.Text);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                lblUserId.Text = rdr.GetValue(0).ToString();
                lblUserName.Text = rdr.GetValue(1).ToString();
                //lblContactNo.Text = rdr.GetValue(2).ToString();
                lblEmail.Text = rdr.GetValue(3).ToString();
            }
            if ((rdr != null))
                rdr.Close();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            SidePanel.Top = btnDashboard.Top;
            MainPanel.Controls.Clear();
            if (!MainPanel.Controls.Contains(DashboardMemberForm.Instance))
            {
                MainPanel.Controls.Add(DashboardMemberForm.Instance);
                DashboardMemberForm.Instance.Dock = DockStyle.Fill;
                DashboardMemberForm.Instance.BringToFront();

                DashboardMemberForm.Instance.Visible = true;
            }
            else
                DashboardMemberForm.Instance.BringToFront();
            DashboardMemberForm.Instance.Visible = true;
        }
        
        private void btnOrder_Click(object sender, EventArgs e)
        {
            SidePanel.Top = btnOrder.Top;
            MainPanel.Controls.Clear();
            if (!MainPanel.Controls.Contains(OrderMemberForm.Instance))
            {
                MainPanel.Controls.Add(OrderMemberForm.Instance);
                OrderMemberForm.Instance.Dock = DockStyle.Fill;
                OrderMemberForm.Instance.BringToFront();
                OrderMemberForm.Instance.Visible = true;
            }
            else
                OrderMemberForm.Instance.BringToFront();
            OrderMemberForm.Instance.Visible = true;
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            SidePanel.Top = btnDashboard.Top;
            MainPanel.Controls.Clear();
            if (!MainPanel.Controls.Contains(FeedbackMemberForm.Instance))
            {
                MainPanel.Controls.Add(FeedbackMemberForm.Instance);
                FeedbackMemberForm.Instance.Dock = DockStyle.Fill;
                FeedbackMemberForm.Instance.BringToFront();
                FeedbackMemberForm.Instance.Visible = true;
            }
            else
                FeedbackMemberForm.Instance.BringToFront();
            FeedbackMemberForm.Instance.Visible = true;
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MemberDashboard_Load(object sender, EventArgs e)
        {
            lblUserName.Text = Global.userName;
            AutoUser();
            SidePanel.Top = btnDashboard.Top;
            MainPanel.Controls.Clear();
            if (!MainPanel.Controls.Contains(DashboardMemberForm.Instance))
            {
                MainPanel.Controls.Add(DashboardMemberForm.Instance);
                DashboardMemberForm.Instance.Dock = DockStyle.Fill;
                DashboardMemberForm.Instance.BringToFront();

                DashboardMemberForm.Instance.Visible = true;
            }
            else
                DashboardMemberForm.Instance.BringToFront();
            DashboardMemberForm.Instance.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDatetime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

        }

        private void lblDatetime_Click(object sender, EventArgs e)
        {

        }
    }
}
