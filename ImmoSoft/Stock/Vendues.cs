﻿
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
    public partial class Vendues : Form
    {
        DataTable sitedt;
        string selectedSite = "0";
        public Vendues()
        {
            InitializeComponent();
            dgv1.DataSourceChanged+=(s, a) =>
            {
                if (dgv1.Columns.Contains("cloture"))
                    dgv1.Columns["cloture"].Visible=true;
            };
        }
        void refresh()
        {
            DB.stock stock = new DB.stock();
            dgv1.DataSource = stock.refreshVendue("Vendue", selectedSite);

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
        private void Vendues_Load(object sender, EventArgs e)
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
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    if (dgv1.SelectedRows[0].Cells["idclient"].Value.ToString()!="0")
                    {
                        addClient add = new addClient(
                            dgv1.SelectedRows[0].Cells["idclient"].Value.ToString(),true);
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
                    Files file = new Files("fiche", dgv1.SelectedRows[0].Cells["id"].Value.ToString(),"stock");
                    file.ShowDialog();
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(dgv1.Rows.Count>0)
                if(dgv1.SelectedRows.Count>0)
                {
                    Files file = new Files("attestation", dgv1.SelectedRows[0].Cells["id"].Value.ToString(), "stock");
                    file.ShowDialog();
                }
        }

        private void search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sitedt.Rows.Count>0)
                foreach (DataRow row in sitedt.Rows)
                    if (row["nom"].ToString()==search.Text)
                    {
                        selectedSite=row["id"].ToString();
                        Properties.Settings.Default.site=search.Text;
                        Properties.Settings.Default.siteid=selectedSite;
                        refresh();
                        break;
                    }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                    if (dgv1.SelectedRows[0].Cells["cloture"].Value.ToString()=="0")
                    {
                        Cloture clo = new Cloture(dgv1.SelectedRows[0].Cells["id"].Value.ToString(), false);
                        clo.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Cette vente a déjà été cloturée");
                    }
        }
    }
}