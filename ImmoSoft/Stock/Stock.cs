using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ImmoSoft
{
    public partial class Stock : Form
    {
        bool choix=false;
        public string id;
        System.Data.DataTable sitedt;
        string user = "";
        string selectedSite = "0";
        public Stock()
        {
            InitializeComponent();
            choix=false;
        }
        public Stock(bool Choix)
        {
            choix=true;
            InitializeComponent();

        }
        void refresh()
        {
            DB.stock stock = new DB.stock();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt=stock.refreshStock("Disponible", selectedSite);
            
            DataView view = dt.DefaultView;
            view.Sort = "lot ASC, parcelle ASC";
            dgv1.DataSource = view;
            /*
             dt.Columns.Add(new DataColumn("sort",));
             foreach (DataRow row in dt.Rows)
                 row["sort"]=row["lot"].ToString()+row["parcelle"].ToString();
             dgv1.DataSource=dt;
             dgv1.Sort(dgv1.Columns["sort"], ListSortDirection.Ascending);
              */
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
            user=Properties.Settings.Default.admin.ToLower();

            if (Properties.Settings.Default.admin.ToLower()=="caissiere")
            {
                groupBox2.Enabled=false;
                groupBox3.Enabled=false;

                groupBox2.Visible=false;
                groupBox3.Visible=false;
            }
            else if (Properties.Settings.Default.admin.ToLower()=="secretaire")
            {

                groupBox2.Enabled=false;
                groupBox3.Enabled=false;

                groupBox2.Visible=false;
                groupBox3.Visible=false;

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
            if(dgv1.Rows.Count>0)
            {
                List<string> lots= new List<string>();  
                foreach(DataGridViewRow row in dgv1.Rows)
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
        private void Stock_Load(object sender, EventArgs e)
        {
            refreshSite();
            if (search.Items.Count>0)
                if (!String.IsNullOrEmpty(Properties.Settings.Default.site))
                    search.Text = Properties.Settings.Default.site;
                else
                    search.SelectedItem=search.Items[0];
            refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addStock add = new addStock();
            add.FormClosed+=(s, a) => { this.refresh(); };
            add.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    addStock add = new addStock(dgv1.SelectedRows[0].Cells["id"].Value.ToString());
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
                    
                    Vente vente = new Vente(dgv1.SelectedRows[0].Cells["id"].Value.ToString(),'P', false);
                    vente.FormClosed+=(s, a) => { this.refresh(); };
                    vente.ShowDialog();
                }
                else
                    MessageBox.Show("Aucun element selectionné");
            else
                MessageBox.Show("Aucun element selectionné");
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

        private void search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sitedt.Rows.Count>0)
                foreach(DataRow row in sitedt.Rows)
                    if (row["nom"].ToString()==search.Text)
                    {
                        selectedSite=row["id"].ToString();
                        Properties.Settings.Default.site=search.Text;
                        Properties.Settings.Default.siteid=selectedSite;
                        refresh();
                        break;
                    }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                    if (MessageBox.Show("Voulez vous vraiment supprimer cette parcelle", "Supprimer",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bool go = false;
                        checkPassword check = new checkPassword(Properties.Settings.Default.id);
                        check.FormClosed+=(a, s) => { go=check.isPassword; };
                        check.ShowDialog();
                        if (go)
                        {
                            DB.stock stock = new DB.stock();
                            DB.historique hist = new DB.historique();
                            hist.add("Parcelle supprimée", dgv1.SelectedRows[0].Cells["id"].Value.ToString(), "0", "0", "0", "0", "0", "0", "");
                            stock.delete(dgv1.SelectedRows[0].Cells["id"].Value.ToString());
                            refresh();
                        }

                    }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {

                    Vente vente = new Vente(dgv1.SelectedRows[0].Cells["id"].Value.ToString(), 'P', true);
                    vente.FormClosed+=(s, a) => { this.refresh(); };
                    vente.ShowDialog();
                }
                else
                    MessageBox.Show("Aucun element selectionné");
            else
                MessageBox.Show("Aucun element selectionné");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Demarcheurs tmp = new Demarcheurs(true);
            tmp.FormClosing+=(s, args) =>
            {
                DB.stock stock = new DB.stock();
                string did = tmp.id;
                if(!String.IsNullOrEmpty(did))
                    stock.setDemarcheur(dgv1.SelectedRows[0].Cells["id"].Value.ToString(),did);
                this.refresh();
            };
            tmp.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                    if (dgv1.SelectedRows[0].Cells["iddemarcheur"].Value.ToString()!="0")
                {
                    Files file = new Files("fiche", dgv1.SelectedRows[0].Cells["id"].Value.ToString(), "stock dispo");
                    file.ShowDialog();
                }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                    if (dgv1.SelectedRows[0].Cells["id"].Value.ToString()!="0")
                    {
                        Files file = new Files("autre", dgv1.SelectedRows[0].Cells["id"].Value.ToString(), "stock autre");
                        file.ShowDialog();
                    }
        }
    }
}
