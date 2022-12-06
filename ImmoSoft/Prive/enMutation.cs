using ImmoSoft.DB;
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
    public partial class enMutation : Form
    {
        DataTable sitedt;
        string selectedSite = "0";
        DB.common com = new DB.common();
        public enMutation()
        {
            InitializeComponent();
        }
        void refresh()
        {
            DB.champs champs = new DB.champs();
            DataTable dt = new DataTable();
            dt=champs.refreshVendue("En cours de Mutation", selectedSite);
            dgv1.DataSource = dt;
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
        private void enMutation_Load(object sender, EventArgs e)
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
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    if (dgv1.SelectedRows[0].Cells["idclient"].Value.ToString()!="0")
                    {
                        addClient add = new addClient(
                            dgv1.SelectedRows[0].Cells["idclient"].Value.ToString(), true);
                        add.ShowDialog();
                    }
                    else
                        MessageBox.Show("Erreur");
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    if (dgv1.SelectedRows[0].Cells["iddemarcheur"].Value.ToString()!="0")
                    {
                        addDemarcheur add = new addDemarcheur(
                            dgv1.SelectedRows[0].Cells["iddemarcheur"].Value.ToString(), true);
                        add.ShowDialog();
                    }
                    else
                        MessageBox.Show("Pas de demarcheur assigné à cette vente!");
                }
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    Files file = new Files("fiche", dgv1.SelectedRows[0].Cells["id"].Value.ToString(), "champs");
                    file.ShowDialog();
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    addVersement add = new addVersement(dgv1.SelectedRows[0].Cells["id"].Value.ToString(), true);
                    add.FormClosed+=(s, a) => { this.refresh(); };
                    add.ShowDialog();
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                    if (MessageBox.Show("Voulez vous vraiment annuler cette mutation", "Annuler",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bool go = false;
                        checkPassword check = new checkPassword(Properties.Settings.Default.id);
                        check.FormClosed+=(a, s) => { go=check.isPassword; };
                        check.ShowDialog();
                        if (go)
                        {
                            DB.champs champs = new DB.champs();
                            DB.historiqueChamps hist = new DB.historiqueChamps();
                            string idclient = dgv1.SelectedRows[0].Cells["idclient"].Value.ToString();
                            champs.update(dgv1.SelectedRows[0].Cells["id"].Value.ToString(),
                                "0", "0", "0", "0", "0", "Disponible", "");
                            hist.annulerVente("Vente annulée", dgv1.SelectedRows[0].Cells["id"].Value.ToString(),
                                idclient, "0", "0", "0", "0", "");
                            refresh();
                        }

                    }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CurrencyManager man = (CurrencyManager)BindingContext[dgv1.DataSource];
            dgv1=com.searchClient(textBox1.Text, dgv1, man);

        }
    }
}
