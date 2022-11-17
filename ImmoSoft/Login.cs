using MySql.Data;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            DB.user user = new DB.user();
            //user.insert("nom", "prenom", "admin", "admin", "admin");
            DataTable dt = user.checkUser(username.Text, password.Text);
            if(dt!= null)
            {
                Properties.Settings.Default. = 
            }
        }
    }
}
