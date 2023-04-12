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
    public partial class historique : Form
    {
        string id;
        public historique()
        {
            InitializeComponent();
        }
        public historique(string ID)
        {
            InitializeComponent();
            id= ID;
        }
        void refresh(string From, string To)
        {
            DB.historique hist = new DB.historique();
            DataTable dt = new DataTable();
            dt=hist.refresh(From, To,id);
            dgv1.DataSource = dt;

            //search.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            //string[] sites = dt.AsEnumerable().Select<DataRow, string>(x => x.Field<string>("site")).ToArray();
            //string[] noms = dt.AsEnumerable().Select<DataRow, string>(x => x.Field<string>("site")).ToArray();
            // search.AutoCompleteCustomSource.AddRange(barcodes);
        }
        private void historique_Load(object sender, EventArgs e)
        {

        }

        private void confirm_Click(object sender, EventArgs e)
        {
            refresh(from.Value.Year.ToString() + "-" + from.Value.Month.ToString() + "-" + from.Value.Day.ToString() + " 00:00:01",
                to.Value.Year.ToString() + "-" + to.Value.Month.ToString() + "-" + to.Value.Day.ToString() + " 23:59:59");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count>0)
            {
                Waiting wait = new Waiting(dgv1);
                wait.ShowDialog();
            }
            else
            {
                MessageBox.Show("La liste est vide");
            }
        }
    }
}
