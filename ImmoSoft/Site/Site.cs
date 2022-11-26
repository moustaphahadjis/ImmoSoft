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
    public partial class Site : Form
    {
        public Site()
        {
            InitializeComponent();
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
    }
}
