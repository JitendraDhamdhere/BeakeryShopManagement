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
    public partial class OrderMemberForm : UserControl
    {
        public OrderMemberForm()
        {
            InitializeComponent();
        }

        private static OrderMemberForm _instance;

        public static OrderMemberForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OrderMemberForm();
                return _instance;
            }
        }


        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        List<Button> UserButtons = new List<Button>();

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            AutoIdGeneration();
            btnSave.Visible = true;
            rest();
        }

        private void AutoIdGeneration()
        {
            int Num = 0;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string OleDb = "SELECT Max(ID+1) FROM BillTable";
            cmd = new SqlCommand(OleDb);
            cmd.Connection = con;
            if (Convert.IsDBNull(cmd.ExecuteScalar()))
            {
                Num = 1;
                lblId.Text = Convert.ToString(Num);
                txtBillId.Text = Convert.ToString("INV-" + Num);
            }
            else
            {
                Num = System.Convert.ToInt32((cmd.ExecuteScalar()));
                lblId.Text = Convert.ToString(Num);
                txtBillId.Text = Convert.ToString("INV-" + Num);
            }
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        private void rest()
        {
            txtItemName.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
            txtTotal.Text = "";
            txtDiscount.Text = "";
            txtDiscountPrice.Text = "0";
            txtGTotal.Text = "";
            lblSubtotal.Text = "0.0";
            txtCustomerName.Text = "";
            txtContactNo.Text = "";
            ListView1.Items.Clear();
        }

        private void OrderMemberForm_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string cmdText1 = "SELECT Distinct RTRIM(Category) from CategoryTable order by 1";
            cmd = new SqlCommand(cmdText1);
            cmd.Connection = con;
            rdr = cmd.ExecuteReader();
            flpCategory.Controls.Clear();
            while ((rdr.Read()))
            {
                Button btn = new Button();
                btn.Text = rdr.GetValue(0).ToString();
                btn.FlatStyle = FlatStyle.Popup;
                btn.BackColor = Color.FromArgb(254, 191, 15);
                btn.Width = 130;
                btn.Height = 60;
                UserButtons.Add(btn);
                flpCategory.Controls.Add(btn);
                this.Controls.Add(flpCategory);
                btn.Click += btn_Click;
            }
            con.Close();
            AutoIdGeneration();
            btnSave.Visible = true;
            rest();
        }



        private void Calc()
        {
            try
            {
                double val1 = 0;
                double val2 = 0;
                double.TryParse(txtPrice.Text, out val1);
                double.TryParse(txtQuantity.Text, out val2);
                val1 = Math.Round(val1, 2);
                val2 = Math.Round(val2, 2);
                double I = (val1 * val2);
                I = Math.Round(I, 2);
                txtTotal.Text = I.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public double subtot()
        {
            int i = 0;
            int j = 0;
            double k = 0;
            i = 0;
            j = 0;
            k = 0;
            try
            {
                j = ListView1.Items.Count;
                for (i = 0; i <= j - 1; i++)
                {
                    k = k + Convert.ToDouble(ListView1.Items[i].SubItems[4].Text);
                    k = Math.Round(k, 2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return k;
        }

        public void Calculate()
        {

            try
            {
                double val1 = 0;
                double val2 = 0;
                double val3 = 0;
                double val4 = 0;


                if (txtDiscount.Text != "")
                {
                    val3 = Convert.ToDouble(((Convert.ToDouble(lblSubtotal.Text)) * Convert.ToDouble(txtDiscount.Text) / 100));
                    val3 = Math.Round(val3, 2);
                    txtDiscountPrice.Text = val3.ToString();

                }

                double.TryParse(lblSubtotal.Text, out val2);
                double.TryParse(txtDiscountPrice.Text, out val3);
                double.TryParse(txtTotal.Text, out val4);
                val2 = Math.Round(val2, 2);
                val3 = Math.Round(val3, 2);
                val4 = val1 + val2 - val3;
                val4 = Math.Round(val4, 2);
                txtGTotal.Text = val4.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            Calc();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemove.Visible = true;
            btnAdd.Visible = true;
            btnMin.Visible = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListView1.Items.Count == 0)
                {
                    MessageBox.Show("No items to remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int itmCnt = 0;
                    int i = 0;
                    int t = 0;

                    ListView1.FocusedItem.Remove();
                    itmCnt = ListView1.Items.Count;
                    t = 1;

                    for (i = 1; i <= itmCnt + 1; i++)
                    {
                        t = t + 1;
                    }
                    lblSubtotal.Text = subtot().ToString();
                }

                btnRemove.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                double val1 = 1;
                foreach (ListViewItem lvi in ListView1.SelectedItems)
                {
                    val1 += double.Parse(lvi.SubItems[3].Text);
                    lvi.SubItems[3].Text = Convert.ToString(val1);
                    lvi.SubItems[4].Text = Convert.ToString(double.Parse(lvi.SubItems[3].Text) * double.Parse(lvi.SubItems[2].Text));
                    lblSubtotal.Text = subtot().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            try
            {
                double val1 = 1;
                foreach (ListViewItem lvi in ListView1.SelectedItems)
                {
                    lvi.SubItems[3].Text = Convert.ToString(double.Parse(lvi.SubItems[3].Text) - val1);
                    lvi.SubItems[4].Text = Convert.ToString(double.Parse(lvi.SubItems[3].Text) * double.Parse(lvi.SubItems[2].Text));
                    lblSubtotal.Text = subtot().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerName.Text == "")
                {
                    MessageBox.Show("Please Enter Customer Name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCustomerName.Focus();
                    return;
                }
                if (ListView1.Items.Count == 0)
                {
                    MessageBox.Show("sorry no item added", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ListView1.Items.Count == 0)
                {
                    MessageBox.Show("sorry no item added", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AutoIdGeneration();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                MemberDashboard MMF = new MemberDashboard();
                string cb = "insert Into BillTable(BillId,CustomerName,ContactNo,Discount,DiscountPrice,Total,BillDate) VALUES ('" + txtBillId.Text + "','" + txtCustomerName.Text + "','" + txtContactNo.Text + "','" + txtDiscount.Text + "','" + txtDiscountPrice.Text + "','" + txtGTotal.Text + "','" + System.DateTime.Now.ToString() + "')";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Close();


                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);

                    string cd = "insert Into BillInfoTable(BillId,ProductName,Price,Quantity,SubTotal) VALUES (@d1,@d2,@d3,@d4,@d5)";
                    cmd = new SqlCommand(cd);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", txtBillId.Text);
                    cmd.Parameters.AddWithValue("d2", ListView1.Items[i].SubItems[1].Text);
                    cmd.Parameters.AddWithValue("d3", ListView1.Items[i].SubItems[2].Text);
                    cmd.Parameters.AddWithValue("d4", ListView1.Items[i].SubItems[3].Text);
                    cmd.Parameters.AddWithValue("d5", ListView1.Items[i].SubItems[4].Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                MessageBox.Show("Successfully Saved", "Bill Treatment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rest();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn1 = (Button)sender;
                string str = btn1.Text;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cmdText1 = "SELECT ItemName from ItemMasterTable where Category=@d1 order by 1";
                cmd = new SqlCommand(cmdText1);
                cmd.Parameters.AddWithValue("@d1", str);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                flpItemsKOT.Controls.Clear();
                flpItemsKOT.AutoScroll = true;
                while ((rdr.Read()))
                {
                    Button btn = new Button();
                    btn.Text = rdr.GetValue(0).ToString();
                    btn.Size = new System.Drawing.Size(100, 100);
                    btn.ForeColor = Color.Black;
                    btn.BackColor = Color.FromArgb(229, 57, 53);
                    flpItemsKOT.Controls.Add(btn);
                    this.Controls.Add(flpItemsKOT);
                    btn.Click += btn_Click2;
                    btn.Tag = rdr.GetValue(0).ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Click2(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string str = btn.Text;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT ItemId,ItemName,Price FROM ItemMasterTable Where ItemName=@d1";
                cmd.Parameters.AddWithValue("@d1", str);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtItemId.Text = rdr.GetValue(0).ToString();
                    txtItemName.Text = rdr.GetValue(1).ToString();
                    txtPrice.Text = rdr.GetValue(2).ToString();
                    txtQuantity.Text = "1";
                    Calc();
                }
                if ((rdr != null))
                    rdr.Close();
                if (txtItemName.Text == "")
                {
                    MessageBox.Show("Please select Product Name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtItemName.Focus();
                    return;
                }
                if (txtPrice.Text == "")
                {
                    MessageBox.Show("Please enter price", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }
                if (txtQuantity.Text == "")
                {
                    MessageBox.Show("Please enter quantity", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantity.Focus();
                    return;
                }

                if (txtTotal.Text == "")
                {
                    MessageBox.Show("Please enter total", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotal.Focus();
                    return;
                }

                if (ListView1.Items.Count == 0)
                {

                    ListViewItem lst = new ListViewItem();
                    lst.SubItems.Add(txtItemName.Text);
                    lst.SubItems.Add(txtPrice.Text);
                    lst.SubItems.Add(txtQuantity.Text);
                    lst.SubItems.Add(txtTotal.Text);
                    ListView1.Items.Add(lst);
                    lblSubtotal.Text = subtot().ToString();
                    txtItemName.Text = "";
                    txtPrice.Text = "";
                    txtTotal.Text = "";
                    txtQuantity.Text = "";
                    return;
                }

                for (int j = 0; j <= ListView1.Items.Count - 1; j++)
                {
                    if (ListView1.Items[j].SubItems[1].Text == txtItemName.Text)
                    {
                        ListView1.Items[j].SubItems[1].Text = txtItemName.Text;
                        ListView1.Items[j].SubItems[2].Text = txtPrice.Text;
                        ListView1.Items[j].SubItems[3].Text = txtQuantity.Text;
                        ListView1.Items[j].SubItems[4].Text = txtTotal.Text;
                        lblSubtotal.Text = subtot().ToString();
                        txtItemName.Text = "";
                        txtPrice.Text = "";
                        txtTotal.Text = "";
                        txtQuantity.Text = "";
                        return;

                    }
                }

                ListViewItem lst1 = new ListViewItem();

                lst1.SubItems.Add(txtItemName.Text);
                lst1.SubItems.Add(txtPrice.Text);
                lst1.SubItems.Add(txtQuantity.Text);
                lst1.SubItems.Add(txtTotal.Text);
                ListView1.Items.Add(lst1);
                lblSubtotal.Text = subtot().ToString();
                txtItemName.Text = "";
                txtPrice.Text = "";
                txtTotal.Text = "";
                txtQuantity.Text = "";
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
