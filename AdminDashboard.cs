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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDatetime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
            SidePanel.Top = btnDashboard.Top;
            if (!MainPanel.Controls.Contains(DashboardForm.Instance))
            {
                MainPanel.Controls.Add(DashboardForm.Instance);
                DashboardForm.Instance.Dock = DockStyle.Fill;
                DashboardForm.Instance.BringToFront();
                DashboardForm.Instance.Visible = true;
            }
            else
                DashboardForm.Instance.BringToFront();
            DashboardForm.Instance.Visible = true;
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            SidePanel.Top = btnCategory.Top;
            MainPanel.Controls.Clear();
            if (!MainPanel.Controls.Contains(CategoryForm.Instance))
            {
                MainPanel.Controls.Add(CategoryForm.Instance);
                CategoryForm.Instance.Dock = DockStyle.Fill;
                CategoryForm.Instance.BringToFront();
                CategoryForm.Instance.Visible = true;
            }
            else
                CategoryForm.Instance.BringToFront();
            CategoryForm.Instance.Visible = true;
        }

        private void MAINP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
            SidePanel.Top = btnReport.Top;
            if (!MainPanel.Controls.Contains(ReportForm.Instance))
            {
                MainPanel.Controls.Add(ReportForm.Instance);
                ReportForm.Instance.Dock = DockStyle.Fill;
                ReportForm.Instance.BringToFront();
                ReportForm.Instance.Visible = true;
            }
            else
                ReportForm.Instance.BringToFront();
            ReportForm.Instance.Visible = true;
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
            SidePanel.Top = btnUser.Top;
            if (!MainPanel.Controls.Contains(UserForm.Instance))
            {
                MainPanel.Controls.Add(UserForm.Instance);
                UserForm.Instance.Dock = DockStyle.Fill;
                UserForm.Instance.BringToFront();
                UserForm.Instance.Visible = true;
            }
            else
                UserForm.Instance.BringToFront();
            UserForm.Instance.Visible = true;
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
            SidePanel.Top = btnFeedback.Top;
            if (!MainPanel.Controls.Contains(FeedBackForm.Instance))
            {
                MainPanel.Controls.Add(FeedBackForm.Instance);
                FeedBackForm.Instance.Dock = DockStyle.Fill;
                FeedBackForm.Instance.BringToFront();
                FeedBackForm.Instance.Visible = true;
            }
            else
                FeedBackForm.Instance.BringToFront();
            FeedBackForm.Instance.Visible = true;
        }

        private void btnItemMaster_Click(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
            SidePanel.Top = btnItemMaster.Top;
            if (!MainPanel.Controls.Contains(ItemMasterForm.Instance))
            {
                MainPanel.Controls.Add(ItemMasterForm.Instance);
                ItemMasterForm.Instance.Dock = DockStyle.Fill;
                ItemMasterForm.Instance.BringToFront();
                ItemMasterForm.Instance.Visible = true;
            }
            else
                ItemMasterForm.Instance.BringToFront();
            ItemMasterForm.Instance.Visible = true;
        }

        private void btnMember_Click(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
            SidePanel.Top = btnMember.Top;
            if (!MainPanel.Controls.Contains(MemberForm.Instance))
            {
                MainPanel.Controls.Add(MemberForm.Instance);
                MemberForm.Instance.Dock = DockStyle.Fill;
                MemberForm.Instance.BringToFront();
                MemberForm.Instance.Visible = true;
            }
            else
                MemberForm.Instance.BringToFront();
            MemberForm.Instance.Visible = true;
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
            SidePanel.Top = btnOrder.Top;
            if (!MainPanel.Controls.Contains(OrderForm.Instance))
            {
                MainPanel.Controls.Add(OrderForm.Instance);
                OrderForm.Instance.Dock = DockStyle.Fill;
                OrderForm.Instance.BringToFront();
                OrderForm.Instance.Visible = true;
            }
            else
                OrderForm.Instance.BringToFront();
            OrderForm.Instance.Visible = true;
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
            SidePanel.Top = btnBill.Top;
            if (!MainPanel.Controls.Contains(BillForm.Instance))
            {
                MainPanel.Controls.Add(BillForm.Instance);
                BillForm.Instance.Dock = DockStyle.Fill;
                BillForm.Instance.BringToFront();
                BillForm.Instance.Visible = true;
            }
            else
                BillForm.Instance.BringToFront();
            BillForm.Instance.Visible = true;
        }


        private void AdminDashboard_Load_1(object sender, EventArgs e)
        {
            lblUserName.Text = Global.userName;
            MainPanel.Controls.Clear();
            SidePanel.Top = btnDashboard.Top;
            if (!MainPanel.Controls.Contains(DashboardForm.Instance))
            {
                MainPanel.Controls.Add(DashboardForm.Instance);
                DashboardForm.Instance.Dock = DockStyle.Fill;
                DashboardForm.Instance.BringToFront();
                DashboardForm.Instance.Visible = true;
            }
            else
                DashboardForm.Instance.BringToFront();
            DashboardForm.Instance.Visible = true;

        }
    }
}
