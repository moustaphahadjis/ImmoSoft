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
    public partial class Mutees : Form
    {
        DataTable sitedt;
        string selectedSite = "0";
        DB.common com = new DB.common();
        public Mutees()
        {
            InitializeComponent(); 
            dgv1.DataSourceChanged+=(s, a) =>
            {
                if (dgv1.Columns.Contains("cloture"))
                    dgv1.Columns["cloture"].Visible=true;
            };
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
        }
        void refresh()
        {
            DB.stock stock = new DB.stock();
            dgv1.DataSource = stock.refreshMutee("Mutée", selectedSite);

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
        private void Mutees_Load(object sender, EventArgs e)
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
                        addClient add = new addClient(
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

        private void button3_Click(object sender, EventArgs e)
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
                    Files file = new Files("attestation", dgv1.SelectedRows[0].Cells["id"].Value.ToString(), "champs");
                    file.ShowDialog();
                }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                    if (dgv1.SelectedRows[0].Cells["cloture"].Value.ToString()==false.ToString())
                    {
                        Cloture clo = new Cloture(dgv1.SelectedRows[0].Cells["id"].Value.ToString(), true);
                        clo.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Cette mutation a déjà été cloturée");
                    }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CurrencyManager man = (CurrencyManager)BindingContext[dgv1.DataSource];
            dgv1=com.searchClient(textBox1.Text, dgv1, man);
        }
    }
}
