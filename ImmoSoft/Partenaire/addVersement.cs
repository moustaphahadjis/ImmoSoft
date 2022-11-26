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

namespace ImmoSoft
{
    public partial class addVersement : Form
    {
        string id;
        bool mutation;
        DB.common com = new DB.common();
        public addVersement(string ID, bool muter)
        {
            InitializeComponent();
            id = ID;
            DB.stock stock = new DB.stock();
            dgvP.DataSource=stock.refresh(id);
            DB.client client = new DB.client();
            dgvC.DataSource=client.refresh(dgvP.Rows[0].Cells["idclient"].Value.ToString());
            DB.demarcheur demarcheur = new DB.demarcheur();
            dgvD.DataSource=demarcheur.refresh(dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString());

            DB.historique hist = new DB.historique();
            dgvV.DataSource = hist.getVersement(id);

            prix.Text=dgvP.Rows[0].Cells["prix"].Value.ToString();
            montant.Text=dgvP.Rows[0].Cells["reste"].Value.ToString();
        }
        bool check()
        {
            bool r = false;
            if (dgvP.Rows.Count>0 &&
                dgvC.Rows.Count>0)
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
            if (versement.Text.Trim().Length>0)
                if (com.isNumber(versement.Text.Trim()))
                    reste.Text = (decimal.Parse(montant.Text.Trim())-decimal.Parse(versement.Text.Trim())).ToString();
                else
                    reste.Text = "";
            else
                reste.Text="";
        }
        private void addVersement_Load(object sender, EventArgs e)
        {
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void versement_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                DB.stock stock = new DB.stock();
                DB.historique hist = new DB.historique();
                DB.printer printer = new DB.printer();
                DB.attribution att = new DB.attribution();
                bool attribue = false;
                string etat = "";
                string action = (dgvV.Rows.Count+1).ToString()+"eme Versement";
                if (decimal.Parse(reste.Text)>0)
                {
                    etat= "En cours de vente";
                }
                else
                {
                    etat="Vendue";
                    attribue = true;
                }

                if (stock.setVersement(id, (decimal.Parse(prix.Text.Trim())-decimal.Parse(reste.Text)).ToString(), reste.Text, etat))
                {
                    if (attribue)
                        att.insert("0", id, "0", dgvP.Rows[0].Cells["idclient"].Value.ToString(), attribue);
                    hist.add(action, id, dgvP.Rows[0].Cells["idclient"].Value.ToString(),
                        dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString(),
                        prix.Text.Trim(), versement.Text.Trim(), reste.Text,
                        dgvP.Rows[0].Cells["type_usage"].Value.ToString());

                    printer.versement("Achat de terrain",
                        dgvC.Rows[0].Cells["nom"].Value.ToString()+" "+dgvC.Rows[0].Cells["prenom"].Value.ToString(),
                        dgvC.Rows[0].Cells["piece"].Value.ToString()+" N° "+dgvC.Rows[0].Cells["numero"].Value.ToString()+" du "+dgvC.Rows[0].Cells["delivrance"].Value.ToString(),
                        dgvC.Rows[0].Cells["contact"].Value.ToString(), dgvP.Rows[0].Cells["lot"].Value.ToString(), dgvP.Rows[0].Cells["parcelle"].Value.ToString(),
                         dgvP.Rows[0].Cells["superficie"].Value.ToString(), action,
                         versement.Text, prix.Text,
                         (decimal.Parse(prix.Text)-decimal.Parse(reste.Text)).ToString(),
                         reste.Text, dgvP.Rows[0].Cells["id"].Value.ToString());

                   /* printer.versement(dgvC.Rows[0].Cells["nom"].Value.ToString() +
                        " " + dgvC.Rows[0].Cells["prenom"].Value.ToString(),
                        dgvC.Rows[0].Cells["Piece"].Value.ToString() +
                        " N° "+dgvC.Rows[0].Cells["Numero"].Value.ToString(),
                        dgvC.Rows[0].Cells["contact"].Value.ToString(),
                        dgvP.Rows[0].Cells["lot"].Value.ToString(),
                        dgvP.Rows[0].Cells["parcelle"].Value.ToString(),
                        dgvP.Rows[0].Cells["superficie"].Value.ToString(),
                        action, versement.Text, prix.Text,
                        (decimal.Parse(prix.Text.Trim())-decimal.Parse(reste.Text)).ToString(),
                        reste.Text, hist.getLast().ToString());
                   */
                }
            }
            else
                MessageBox.Show("Erreur! Réverifiez les données saisies");
        }
    }
}
