using ImmoSoft.Partenaire;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmoSoft
{
    public partial class Clients : Form
    {
        DB.common com = new DB.common();
        bool choix = false;
        public string id;
        public Clients()
        {
            InitializeComponent();
            if (Properties.Settings.Default.admin.ToLower()=="caissiere")
            {

                option2.Enabled=false;

                option2.Visible=false;

            }
            else if (Properties.Settings.Default.admin.ToLower()=="secretaire")
            {

                option2.Enabled=false;

                option2.Visible=false;

            }
        }
        public Clients(bool Choix)
        {
            InitializeComponent();
            choix=true;
            dgv1.DataSourceChanged+=(e, s) =>
            {
                dgv1.CellDoubleClick+=(a, sv) =>
                {
                    if (dgv1.Rows.Count>0)
                        if (dgv1.SelectedRows.Count>0)
                        {
                            id=dgv1.SelectedRows[0].Cells["id"].Value.ToString();
                            this.Close();
                        }
                        else
                            MessageBox.Show("Aucun element selectionné");
                    else
                        MessageBox.Show("Aucun element selectionné");
                };
            };
        }

        void refresh()
        {
            DB.client client = new DB.client();
            DataTable dt = new DataTable();
            dt=client.refresh();
            dgv1.DataSource = dt;
            choisir.Visible=choix;
            choisir.Enabled=choix;
            option2.Enabled=!choix;
            option2.Visible=!choix;

            search.AutoCompleteSource= AutoCompleteSource.CustomSource;
            search.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            search.AutoCompleteMode= AutoCompleteMode.Suggest;

            string[] nom = dt.AsEnumerable().Select<DataRow, string>(x => x.Field<string>("nom")).ToArray();
            string[] prenom = dt.AsEnumerable().Select<DataRow, string>(x => x.Field<string>("prenom")).ToArray();
            //prenom.CopyTo(nom, nom.Length);
            string[] contact = dt.AsEnumerable().Select<DataRow, string>(x => x.Field<string>("contact")).ToArray();
            search.AutoCompleteCustomSource.AddRange(nom);
            //search.AutoCompleteCustomSource.AddRange(prenom);
            search.AutoCompleteCustomSource.AddRange(contact);
        }
        private void Clients_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            CurrencyManager man = (CurrencyManager)BindingContext[dgv1.DataSource];
            dgv1=com.searchPerson(search.Text, dgv1, man);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addClient add = new addClient();
            add.FormClosed+=(s, args) =>
            {
                refresh();
            };
            add.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dgv1.Rows.Count>0)
                if(dgv1.SelectedRows.Count>0)
                {
                    addClient add = new addClient(dgv1.SelectedRows[0].Cells["id"].Value.ToString());
                    add.FormClosed+=(s, args) =>
                    {
                        refresh();
                    };
                    add.ShowDialog();
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    if (MessageBox.Show("Voulez vous vraiment supprimer ce client?", "Supprimer",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                    {
                        DB.client client= new DB.client();
                        if (client.delete(dgv1.SelectedRows[0].Cells["id"].Value.ToString()))
                        {
                            MessageBox.Show("Client supprimé avec succès");
                            refresh();
                        }
                    }
                }
        }

        private void choisir_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    id=dgv1.SelectedRows[0].Cells["id"].Value.ToString();
                    this.Close();
                }
                else
                    MessageBox.Show("Aucun element selectionné");
            else
                MessageBox.Show("Aucun element selectionné");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {

                    Vente vente = new Vente(dgv1.SelectedRows[0].Cells["id"].Value.ToString(), 'C',false);
                    vente.FormClosed+=(s, a) => { this.refresh(); };
                    vente.ShowDialog();
                }
                else
                    MessageBox.Show("Aucun element selectionné");
            else
                MessageBox.Show("Aucun element selectionné");

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    clientStock cs = new clientStock(dgv1.SelectedRows[0].Cells["id"].Value.ToString(),"en cours de vente");
                    cs.ShowDialog();
                        }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    clientStock cs = new clientStock(dgv1.SelectedRows[0].Cells["id"].Value.ToString(), "vendue");
                    cs.ShowDialog();
                }
        }
    }
}
