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
    public partial class addDemarcheur : Form
    {
        bool modify;
        string id;
        public addDemarcheur()
        {
            InitializeComponent();
            modify = false;
        }
        public addDemarcheur(string ID)
        {
            InitializeComponent();
            modify = true;
            id=ID;
            DB.demarcheur client = new DB.demarcheur();
            DataRow row = client.refresh(id).Rows[0];
            nom.Text=row["nom"].ToString();
            prenom.Text=row["prenom"].ToString();
            piece.Text=row["piece"].ToString();
            numero.Text=row["numero"].ToString();
            contact.Text=row["contact"].ToString();
            addresse.Text=row["addresse"].ToString();
            nom.Text=row["nom"].ToString();
        }
        public addDemarcheur(string ID, bool check)
        {
            InitializeComponent();
            modify = true;
            id=ID;
            DB.demarcheur client = new DB.demarcheur();
            DataRow row = client.refresh(id).Rows[0];
            nom.Text=row["nom"].ToString();
            prenom.Text=row["prenom"].ToString();
            piece.Text=row["piece"].ToString();
            numero.Text=row["numero"].ToString();
            contact.Text=row["contact"].ToString();
            addresse.Text=row["addresse"].ToString();
            nom.Text=row["nom"].ToString();


            nom.Enabled=false;
            prenom.Enabled=false;
            piece.Enabled=false;
            numero.Enabled=false;
            contact.Enabled=false;
            addresse.Enabled=false;
            button1.Enabled=false;
            button3.Enabled=false;

            button1.Visible=false;
            button3.Visible=false;
        }
        void add()
        {
            DB.demarcheur demarcheur = new DB.demarcheur();
            DB.common com = new DB.common();

            if (!string.IsNullOrEmpty(nom.Text)&&
                !string.IsNullOrEmpty(prenom.Text)&&
                !string.IsNullOrEmpty(contact.Text))
                if (com.isPhone(contact.Text.Trim()))
                {
                    if (demarcheur.add(nom.Text.TrimStart().TrimEnd().ToUpper(),
                        prenom.Text.TrimStart().TrimEnd().ToUpper(),
                        piece.Text,
                        numero.Text.TrimStart().TrimEnd().ToUpper(),
                        delivrance.Text, profession.Text,
                        contact.Text.Trim().ToUpper(),
                        addresse.Text.TrimStart().TrimEnd().ToUpper()))
                    {
                        MessageBox.Show("Nouveau client ajouté avec succès!");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez bien verifier le numero de telephone!");
                }
        }
        void update()
        {
            DB.demarcheur demarcheur = new DB.demarcheur();
            DB.common com = new DB.common();

            if (!string.IsNullOrEmpty(nom.Text)&&
                !string.IsNullOrEmpty(prenom.Text)&&
                !string.IsNullOrEmpty(contact.Text))
            {
                if (com.isPhone(contact.Text.Trim()))
                {
                    if (demarcheur.update(id, nom.Text.TrimStart().TrimEnd().ToUpper(),
                        prenom.Text.TrimStart().TrimEnd().ToUpper(),
                        piece.Text,
                        numero.Text.TrimStart().TrimEnd().ToUpper(),
                        delivrance.Text, profession.Text,
                        contact.Text.Trim().ToUpper(),
                        addresse.Text.TrimStart().TrimEnd().ToUpper()))
                    {
                        MessageBox.Show("Client mis à jour avec succès!");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez bien verifier le numero de telephone!");
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (modify)
                update();
            else
                add();
        }

        private void addDemarcheur_Load(object sender, EventArgs e)
        {

        }
    }
}
