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
    public partial class Champs : Form
    {
        bool choix = false;
        public string id;
        DataTable sitedt;
        string selectedSite = "0";
        public Champs()
        {
            InitializeComponent();
        }
        public Champs(bool Choix)
        {
            choix=true;
            InitializeComponent();

        }
        void refresh()
        {
            DB.champs champs = new DB.champs();
            DataTable dt = new DataTable();
            dt=champs.refreshStock("Disponible", selectedSite);
            DataView view = dt.DefaultView;
            view.Sort = "lot ASC, parcelle ASC";
            dgv1.DataSource = view;

            if (choix)
            {
                choisir.Visible=true;
                choisir.Enabled=true;
                groupBox2.Enabled=false;
                button7.Enabled=false;
                groupBox2.Visible=false;
                button7.Visible=false;
            }
            else
            {
                choisir.Visible=false;
                choisir.Enabled=false;
            }

            calculate();
        }
        void refreshSite()
        {
            DB.site site = new DB.site();
            sitedt = site.refresh();
            search.Items.Clear();
            search.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            string[] sites = sitedt.AsEnumerable().Select<DataRow, string>(x => x.Field<string>("nom")).ToArray();
            //string[] noms = dt.AsEnumerable().Select<DataRow, string>(x => x.Field<string>("site")).ToArray();
            search.Items.AddRange(sites);
        }
        void calculate()
        {
            if (dgv1.Rows.Count>0)
            {
                List<string> lots = new List<string>();
                foreach (DataGridViewRow row in dgv1.Rows)
                    if (!lots.Contains(row.Cells["lot"].Value.ToString()))
                        lots.Add(row.Cells["lot"].Value.ToString());
                nblot.Text=lots.Count.ToString();
                nbpar.Text=dgv1.Rows.Count.ToString();
            }
            else
            {
                nblot.Text="0";
                nbpar.Text="0";
            }

        }
        private void Champs_Load(object sender, EventArgs e)
        {
            refreshSite();
            if (search.Items.Count>0)
                search.SelectedItem=search.Items[0];
            refresh();
        }

        private void search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sitedt.Rows.Count>0)
                foreach (DataRow row in sitedt.Rows)
                    if (row["nom"].ToString()==search.Text)
                    {
                        selectedSite=row["id"].ToString();
                        refresh();
                        break;
                    }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addChamps add = new addChamps();
            add.FormClosed+=(s, a) => { this.refresh(); };
            add.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    addChamps add = new addChamps(dgv1.SelectedRows[0].Cells["id"].Value.ToString());
                    add.FormClosed+=(a, s) => { refresh(); };
                    add.ShowDialog();
                }
                else
                    MessageBox.Show("Aucun element selectionné");
            else
                MessageBox.Show("Aucun element selectionné");

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {

                    Mutation vente = new Mutation(dgv1.SelectedRows[0].Cells["id"].Value.ToString(), 'P');
                    vente.FormClosed+=(s, a) => { this.refresh(); };
                    vente.ShowDialog();
                }
                else
                    MessageBox.Show("Aucun element selectionné");
            else
                MessageBox.Show("Aucun element selectionné");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    Demarcheurs tmp = new Demarcheurs(true);
                    tmp.FormClosing+=(s, args) =>
                    {
                        DB.champs stock = new DB.champs();
                        if (tmp.id!=null)
                            if (stock.setDemarcheur(dgv1.SelectedRows[0].Cells["id"].Value.ToString(), tmp.id))
                            {
                                MessageBox.Show("Parcelle assignée avec succès");
                                this.refresh();
                            }
                    };
                    tmp.ShowDialog();
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
                    Clients tmp = new Clients(true);
                    tmp.FormClosing+=(s, args) =>
                    {
                        DB.champs stock = new DB.champs();
                        if (stock.setClient(dgv1.SelectedRows[0].Cells["id"].Value.ToString(), tmp.id))
                        {
                            MessageBox.Show("Parcelle assignée avec succès");
                            this.refresh();
                        }
                    };
                    tmp.ShowDialog();
                }
                else
                    MessageBox.Show("Aucun element selectionné");
            else
                MessageBox.Show("Aucun element selectionné");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
            {
                Waiting wait = new Waiting(dgv1, search.Text);
                wait.ShowDialog();
            }
            else
            {
                MessageBox.Show("La liste est vide");
            }
        }
    }
}
