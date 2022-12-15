using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ImmoSoft
{
    public partial class Demarcheurs : Form
    {
        DB.common com = new DB.common();
        bool choix = false;
        public string id;
        public Demarcheurs()
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
        public Demarcheurs(bool Choix)
        {
            choix=true;
            InitializeComponent();
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
            DB.demarcheur demarcheur = new DB.demarcheur();
            DataTable dt = new DataTable();
            dt=demarcheur.refresh();
            dgv1.DataSource = dt;
            choisir.Visible=choix;
            choisir.Enabled=choix;
            option2.Enabled=!choix;
            option2.Visible=!choix;
            /*
            search.AutoCompleteSource= AutoCompleteSource.CustomSource;
            search.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            search.AutoCompleteMode= AutoCompleteMode.Suggest;

            string[] nom = dt.AsEnumerable().Select<DataRow, string>(x => x.Field<string>("nom")).ToArray();
            string[] prenom = dt.AsEnumerable().Select<DataRow, string>(x => x.Field<string>("prenom")).ToArray();
            string[] contact = dt.AsEnumerable().Select<DataRow, string>(x => x.Field<string>("contact")).ToArray();
            
            search.AutoCompleteCustomSource.AddRange(nom);
            search.AutoCompleteCustomSource.AddRange(prenom);
            search.AutoCompleteCustomSource.AddRange(contact);
            */
        }
        private void Demarcheurs_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addDemarcheur add = new addDemarcheur();
            add.FormClosed+=(s, args) =>
            {
                refresh();
            };
            add.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    addDemarcheur add = new addDemarcheur(dgv1.SelectedRows[0].Cells["id"].Value.ToString());
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
                    if (MessageBox.Show("Voulez vous vraiment supprimer ce demarcheur?", "Supprimer",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                    {
                        DB.demarcheur demarcheur = new DB.demarcheur();
                        if (demarcheur.delete(dgv1.SelectedRows[0].Cells["id"].Value.ToString()))
                        {
                            MessageBox.Show("Demarcheur supprimé avec succès");
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

                    Vente vente = new Vente(dgv1.SelectedRows[0].Cells["id"].Value.ToString(), 'D',false);
                    vente.FormClosed+=(s, a) => { this.refresh(); };
                    vente.ShowDialog();
                }
                else
                    MessageBox.Show("Aucun element selectionné");
            else
                MessageBox.Show("Aucun element selectionné");

        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            CurrencyManager man = (CurrencyManager)BindingContext[dgv1.DataSource];
            dgv1=com.searchPerson(search.Text, dgv1, man);
        }
    }
}
