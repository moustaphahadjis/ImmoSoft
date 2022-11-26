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
    public partial class checkPassword : Form
    {
        string id;
        public bool isPassword=false;
        public checkPassword(string id)
        {
            InitializeComponent();
            this.id=id;
        }

        private void checkPassword_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB.user user = new DB.user();
            if (user.checkPassword(id, password.Text))
            {
                isPassword=true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Mot de passe incorrect");
            }
        }
    }
}
