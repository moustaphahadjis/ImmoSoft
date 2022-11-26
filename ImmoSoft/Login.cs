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
            if (dt!= null)
                if (dt.Rows.Count>0)
                {
                    Properties.Settings.Default.nom=dt.Rows[0]["nom"].ToString();
                    Properties.Settings.Default.prenom=dt.Rows[0]["prenom"].ToString();
                    Properties.Settings.Default.admin=dt.Rows[0]["admin"].ToString();
                    Properties.Settings.Default.id=dt.Rows[0]["id"].ToString();

                    Form1 form1 = new Form1();
                    form1.FormClosed+=(s, a) => { this.Close(); };
                    this.Hide();
                    form1.ShowDialog();
                }
                else
                    MessageBox.Show("Nom d'utilisateur ou mot de passe incorect");

            else
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorect");
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void annuler_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
