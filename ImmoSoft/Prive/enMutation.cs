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
        public enMutation()
        {
            InitializeComponent();
        }
        void refresh()
        {
            DB.stock stock = new DB.stock();
            DataTable dt = new DataTable();
            dt=stock.refreshVendue("En cours de vente", selectedSite);
            dgv1.DataSource = dt;
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
        private void enMutation_Load(object sender, EventArgs e)
        {
            refresh();
            refreshSite();
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
                DB.printer printer = new DB.printer();
                printer.export(dgv1, search.Text);
            }
            else
            {
                MessageBox.Show("La liste est vide");
            }
        }
    }
}
