﻿using System;
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
    public partial class Site : Form
    {
        public Site()
        {
            InitializeComponent();
            //parcellesDisponiblesToolStripMenuItem.Click
            parcellesDisponiblesToolStripMenuItem.Click+=(a,s) => 
            {
                Properties.Settings.Default.site=dgv1.SelectedRows[0].Cells["nom"].Value.ToString();
                Stock stock = new Stock();
                stock.ShowDialog();
            };
            venteEnCoursToolStripMenuItem.Click+=(a, s) =>
            {
                Properties.Settings.Default.site=dgv1.SelectedRows[0].Cells["nom"].Value.ToString();
                enVente stock = new enVente();
                stock.ShowDialog();
            };
            parcellesVenduesToolStripMenuItem.Click+=(a, s) =>
            {
                Properties.Settings.Default.site=dgv1.SelectedRows[0].Cells["nom"].Value.ToString();
                Vendues stock = new Vendues();
                stock.ShowDialog();
            };
            mutationEnCoursToolStripMenuItem.Click+=(a, s) =>
            {
                Properties.Settings.Default.site=dgv1.SelectedRows[0].Cells["nom"].Value.ToString();
                Vendues stock = new Vendues();
                stock.ShowDialog();
            };
            parcellesMutéesToolStripMenuItem.Click+=(a, s) =>
            {
                Properties.Settings.Default.site=dgv1.SelectedRows[0].Cells["nom"].Value.ToString();
                Vendues stock = new Vendues();
                stock.ShowDialog();
            };
        }
        void refresh()
        {
            DB.site site = new DB.site();
            dgv1.DataSource=site.refresh();
        }
        private void Site_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addSite add = new addSite();
            add.FormClosed+=(s, a) => { this.refresh(); };
            add.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
                if (dgv1.SelectedRows.Count>0)
                {
                    addSite add = new addSite(dgv1.SelectedRows[0].Cells["id"].Value.ToString());
                    add.FormClosed+=(a, s) => { refresh(); };
                    add.ShowDialog();
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
                DB.printer printer = new DB.printer();
                printer.export(dgv1, "Sites");
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
                    if (MessageBox.Show("Voulez vous vraiment supprimer ce site", "Supprimer",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bool go = false;
                        checkPassword check = new checkPassword(Properties.Settings.Default.id);
                        check.FormClosed+=(a, s) => { go=check.isPassword; };
                        check.ShowDialog();
                        if (go)
                        {
                            DB.site site = new DB.site();
                            site.delete(dgv1.SelectedRows[0].Cells["id"].Value.ToString());
                            refresh();
                        }

                    }
        }

        private void dgv1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Right)
                if (dgv1.Rows.Count>0)
                    if (dgv1.SelectedRows.Count>0)
                    {
                        menuStrip.Show(dgv1, new Point(e.X, e.Y));
                    }
        }
    }
}
