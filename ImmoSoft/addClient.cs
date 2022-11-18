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
            delivrance.Format = DateTimePickerFormat.Custom;
            delivrance.CustomFormat = "dd/MM/yyyy";
            modify = false;
        }
        public addClient(string ID)
        {
            InitializeComponent();
            delivrance.Format = DateTimePickerFormat.Custom;
            delivrance.CustomFormat = "dd/MM/yyyy";
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
            profession.Text=row["profession"].ToString();
            string[] date = row["delivrance"].ToString().Split('/');
            delivrance.Value=new DateTime(Int32.Parse(
                date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));
        }
        public addClient(string ID, bool check)
        {
            InitializeComponent();
            delivrance.Format = DateTimePickerFormat.Custom;
            delivrance.CustomFormat = "dd/MM/yyyy";
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
            profession.Text=row["profession"].ToString();
            matrimonial.Text=row["matrimonial"].ToString();
            string[] date = row["delivrance"].ToString().Split('/');
            delivrance.Value=new DateTime(Int32.Parse(
                date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));

            nom.Enabled=false;
            prenom.Enabled=false;
            piece.Enabled=false;
            numero.Enabled=false;
            contact.Enabled=false;
            addresse.Enabled=false;
            profession.Enabled=false;
            delivrance.Enabled=false;
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
                        delivrance.Value.ToString("dd/MM/yyyy"),
                        profession.Text.TrimStart().TrimEnd().ToUpper(),
                        matrimonial.Text.TrimStart().TrimEnd().ToUpper(),
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
                        delivrance.Value.ToString("dd/MM/yyyy"),
                        profession.Text.TrimStart().TrimEnd().ToUpper(),
                        matrimonial.Text.TrimStart().TrimEnd().ToUpper(),
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

        private void delivrance_ValueChanged(object sender, EventArgs e)
        {

        }

        private void piece_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void profession_TextChanged(object sender, EventArgs e)
        {

        }

        private void numero_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
