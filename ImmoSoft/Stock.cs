using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ImmoSoft
{
    public partial class Stock : Form
    {
        bool choix=false;
        public string id;
        public Stock()
        {
            InitializeComponent();
        }
        public Stock(bool Choix)
        {
            choix=true;
            InitializeComponent();

        }
        void refresh()
        {
            DB.stock stock = new DB.stock();
            DataTable dt = new DataTable();
            dt=stock.refresh("Disponible","etat");
            DataView view = dt.DefaultView;
            view.Sort = "lot ASC, parcelle ASC";
            dgv1.DataSource = view;

            choisir.Visible=choix;
            choisir.Enabled=choix;

            //search.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            //string[] sites = dt.AsEnumerable().Select<DataRow, string>(x => x.Field<string>("site")).ToArray();
            //string[] noms = dt.AsEnumerable().Select<DataRow, string>(x => x.Field<string>("site")).ToArray();
            // search.AutoCompleteCustomSource.AddRange(barcodes);
        }
        private void Stock_Load(object sender, EventArgs e)
        {
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
                    
                    Vente vente = new Vente(dgv1.SelectedRows[0].Cells["id"].Value.ToString(),'P');
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
                        DB.stock stock = new DB.stock();
                        if(tmp.id!=null)
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
                        DB.stock stock = new DB.stock();
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
                DB.printer printer = new DB.printer();
                printer.export(dgv1, "tous");
            }
            else
            {
                MessageBox.Show("La liste est vide");
            }
        }
    }
}
