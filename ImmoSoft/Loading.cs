using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmoSoft
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar.Size.Width>700)
            {
                timer1.Stop();
                this.Hide();
                Login log = new Login();
                log.Closed += (ev, args) => this.Close();
                log.ShowDialog();
            }
            else
            {
                progressBar.Width+=10;
            }
        }
    }
}
