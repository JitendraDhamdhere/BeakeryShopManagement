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
    public partial class SplashScreenBSM : Form
    {
        public SplashScreenBSM()
        {
            InitializeComponent();
        }

        private void SplashScreenBSM_Load(object sender, EventArgs e)
        {
            // Start the loading process
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            // Set the timer interval (in milliseconds)
            timer1.Interval = 100;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value += 5; // Adjust the step as needed
            }
            else
            {
                // Loading is complete
                timer1.Stop();
                this.Hide();
                LoginForm LF = new LoginForm();
                LF.Show();
            }
        }
    }
}
