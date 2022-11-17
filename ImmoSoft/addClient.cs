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
    public partial class addClient : Form
    {
        bool modify;
        string id; 
        public addClient()
        {
            InitializeComponent();
            modify = false;
        }
        public addClient(string ID)
        {
            InitializeComponent();
            modify = true;
            id=ID;
            DB.client client = new DB.client();
            DataRow row = client.refresh(id).Rows[0];
            nom.Text=row["nom"].ToString();
            prenom.Text=row["prenom"].ToString();
            piece.Text=row["piece"].ToString();
            numero.Text=row["numero"].ToString();
            contact.Text=row["contact"].ToString();
            addresse.Text=row["addresse"].ToString();
        }
        public addClient(string ID, bool check)
        {
            InitializeComponent();
            modify = true;
            id=ID;
            DB.client client = new DB.client();
            DataRow row = client.refresh(id).Rows[0];
            nom.Text=row["nom"].ToString();
            prenom.Text=row["prenom"].ToString();
            piece.Text=row["piece"].ToString();
            numero.Text=row["numero"].ToString();
            contact.Text=row["contact"].ToString();
            addresse.Text=row["addresse"].ToString();

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
            DB.client client = new DB.client();
            DB.common com = new DB.common();

            if (!string.IsNullOrEmpty(nom.Text)&&
                !string.IsNullOrEmpty(prenom.Text)&&
                !string.IsNullOrEmpty(contact.Text))
                if (com.isPhone(contact.Text.Trim()))
                {
                    if (client.add(nom.Text.TrimStart().TrimEnd().ToUpper(),
                        prenom.Text.TrimStart().TrimEnd().ToUpper(),
                        piece.Text,
                        numero.Text.TrimStart().TrimEnd().ToUpper(),
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
            DB.client client = new DB.client();
            DB.common com = new DB.common();

            if (!string.IsNullOrEmpty(nom.Text)&&
                !string.IsNullOrEmpty(prenom.Text)&&
                !string.IsNullOrEmpty(contact.Text))
            {
                if (com.isPhone(contact.Text.Trim()))
                {
                    if (client.update(id, nom.Text.TrimStart().TrimEnd().ToUpper(),
                        prenom.Text.TrimStart().TrimEnd().ToUpper(),
                        piece.Text,
                        numero.Text.TrimStart().TrimEnd().ToUpper(),
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
            if(modify)
                update();
            else
                add();
        }

        private void addClient_Load(object sender, EventArgs e)
        {

        }
    }
}
