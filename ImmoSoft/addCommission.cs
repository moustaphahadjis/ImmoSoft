using Microsoft.Office.Interop.Excel;
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
    public partial class addCommission : Form
    {
        string iddemarcheur = "";
        string site = "";

        DB.common com = new DB.common();
        public addCommission(string idstock,string site, string option)
        {
            InitializeComponent();
            if (option=="stock")
            {
                DB.stock stock = new DB.stock();
                dgvP.DataSource = stock.refresh(idstock);
                iddemarcheur=dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString();
                comm.Text = dgvP.Rows[0].Cells["commission"].Value.ToString();
                payable.Text =(decimal.Parse(dgvP.Rows[0].Cells["commission"].Value.ToString())-
                    decimal.Parse(dgvP.Rows[0].Cells["comReste"].Value.ToString())).ToString();
            }
            else
            {
                /*
                DB.champs champs = new DB.champs();
                dgv1.DataSource = champs.refreshCommission(id);
                */
            }
            this.site=site;
            DB.demarcheur dem = new DB.demarcheur();
            dgvD.DataSource = dem.refresh(iddemarcheur);

        }
        bool check()
        {
            bool r = false;
            if (dgvP.Rows.Count>0 &&
                dgvD.Rows.Count>0)
            {
                decimal tmp = 0;
                if (decimal.TryParse(reste.Text.Trim(), out tmp))
                    if (tmp>=0)
                        r=true;
            }
            return r;
        }
        void calculate()
        {
            if (comm.Text.Trim().Length>0 && payable.Text.Trim().Length>0 && montant.Text.Trim().Length>0)
                if (com.isNumber(comm.Text.Trim()) && com.isNumber(montant.Text.Trim()))
                    reste.Text = (decimal.Parse(payable.Text.Trim())-decimal.Parse(montant.Text.Trim())).ToString();
                else
                    reste.Text = "";
            else
                reste.Text="";
        }
        private void addCommission_Load(object sender, EventArgs e)
        {

        }

        private void confirm_Click(object sender, EventArgs e)
        {
            if(check())
            {
                DB.stock stock = new DB.stock();
                DB.historique hist = new DB.historique();

                if (stock.updateCommission(dgvP.Rows[0].Cells["id"].Value.ToString(),
                    reste.Text))
                    hist.add("Commission payée", dgvP.Rows[0].Cells["id"].Value.ToString(),
                        dgvP.Rows[0].Cells["idClient"].Value.ToString(),
                        dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString(),
                        dgvP.Rows[0].Cells["prix"].Value.ToString(), "0",
                        dgvP.Rows[0].Cells["reste"].Value.ToString(),
                        dgvP.Rows[0].Cells["commission"].Value.ToString(),
                        reste.Text,
                        dgvP.Rows[0].Cells["type_usage"].Value.ToString());
                Waiting wait = new Waiting(dgvP,dgvD,montant.Text,site);
                wait.ShowDialog();



                this.Close();
            }
        }

        private void montant_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
