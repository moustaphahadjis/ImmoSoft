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
    public partial class addUser : Form
    {
        bool update = false;
        string id;
        public addUser()
        {
            InitializeComponent();
        }
        public addUser(string Id)
        {
            InitializeComponent();
            update = true;
            id=Id;
            DB.user user = new DB.user();
            DataRow row = user.refresh(Id).Rows[0];
            nom.Text=row["nom"].ToString();
            prenom.Text=row["prenom"].ToString();
            addresse.Text=row["adresse"].ToString();
            username.Text=row["username"].ToString();
            contact.Text=row["contact"].ToString();
            admin.Text=row["admin"].ToString();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool done=false;
            DB.user user = new DB.user();
            if (!string.IsNullOrEmpty(username.Text.Trim())&&
                !string.IsNullOrEmpty(password.Text.Trim())&&
                !string.IsNullOrEmpty(password2.Text.Trim())&&
                !string.IsNullOrEmpty(admin.Text.Trim()))
                if (!update)
                {
                    if (!user.usernameExist(username.Text))
                        if (password.Text.Length>8)
                            if (password.Text==password2.Text)
                            {


                                user.insert(nom.Text.TrimEnd().TrimStart(),
                                    prenom.Text.TrimEnd().TrimStart(),
                                    addresse.Text.TrimEnd().TrimStart(),
                                    contact.Text.TrimEnd().TrimStart(),
                                    username.Text.TrimEnd().TrimStart(),
                                    password.Text.TrimEnd().TrimStart(),
                                    admin.Text);
                                done=true;
                                MessageBox.Show("Utilisateur inséré avec succès");

                            }
                }
            else
                {
                    if (!user.usernameExist(username.Text,id))
                        if (password.Text.Length>8)
                            if (password.Text==password2.Text)
                            {


                                user.insert(nom.Text.TrimEnd().TrimStart(),
                                    prenom.Text.TrimEnd().TrimStart(),
                                    addresse.Text.TrimEnd().TrimStart(),
                                    contact.Text.TrimEnd().TrimStart(),
                                    username.Text.TrimEnd().TrimStart(),
                                    password.Text.TrimEnd().TrimStart(),
                                    admin.Text);
                                done=true;
                                MessageBox.Show("Utilisateur inséré avec succès");

                            }
                }
            if (!done)
                MessageBox.Show("Erreur lors de l'insertion");
        }

        private void addUser_Load(object sender, EventArgs e)
        {

        }
    }
}
