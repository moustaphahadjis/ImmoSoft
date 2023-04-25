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
        int count = 0;
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
            if (count>300)
            {
                timer1.Stop();
                this.Hide();
                Login log = new Login();
                log.Closed += (ev, args) => this.Close();
                log.ShowDialog();
            }
            else
            {
                count+=10;
            }
        }
    }
}
