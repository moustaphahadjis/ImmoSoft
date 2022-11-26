using ImmoSoft.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmoSoft
{
    public partial class Attribution : Form
    {
        DataTable sitedt;
        string selectedSite = "0";
        public Attribution()
        {
            InitializeComponent();
        }
        void refresh(string From, string To)
        {
            DB.attribution att = new DB.attribution();
            dgv1.DataSource=att.refresh(From, To, "");
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
        private void Attribution_Load(object sender, EventArgs e)
        {
            //refresh();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            refresh(from.Value.Year.ToString() + "-" + from.Value.Month.ToString() + "-" + from.Value.Day.ToString() + " 00:00:01",
                to.Value.Year.ToString() + "-" + to.Value.Month.ToString() + "-" + to.Value.Day.ToString() + " 23:59:59");
        }

        private void search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sitedt.Rows.Count>0)
                foreach (DataRow row in sitedt.Rows)
                    if (row["nom"].ToString()==search.Text)
                    {
                        selectedSite=row["id"].ToString();
                        //refresh();
                        break;
                    }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    Files file;
                    if (dgv1.SelectedRows[0].Cells["idstock"].Value.ToString()=="0")
                        file = new Files("fiche", dgv1.SelectedRows[0].Cells["idchamps"].Value.ToString(), "champs");
                    else
                        file = new Files("fiche", dgv1.SelectedRows[0].Cells["idstock"].Value.ToString(), "stock");

                    file.ShowDialog();
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    Files file;
                    if (dgv1.SelectedRows[0].Cells["idstock"].Value.ToString()=="0")
                        file = new Files("attestation", dgv1.SelectedRows[0].Cells["idchamps"].Value.ToString(), "champs");
                    else
                        file = new Files("attestation", dgv1.SelectedRows[0].Cells["idstock"].Value.ToString(), "stock");

                    file.ShowDialog();
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    if (dgv1.SelectedRows[0].Cells["idnew"].Value.ToString()!="0")
                    {
                        addClient add = new addClient(
                            dgv1.SelectedRows[0].Cells["idnew"].Value.ToString(), true);
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
                    if (dgv1.SelectedRows[0].Cells["idold"].Value.ToString()!="0")
                    {
                        addClient add = new addClient(
                            dgv1.SelectedRows[0].Cells["idold"].Value.ToString(), true);
                        add.ShowDialog();
                    }
                    else
                        MessageBox.Show("Erreur");
                }
        }
    }
}
