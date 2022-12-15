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
            mutation = muter;
            if (!muter)
            {
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
            else
            {
                DB.stock stock = new DB.stock();
                dgvP.DataSource=stock.refresh(id);
                DB.client client = new DB.client();
                dgvC.DataSource=client.refresh(dgvP.Rows[0].Cells["idclient"].Value.ToString());
                DB.demarcheur demarcheur = new DB.demarcheur();
                dgvD.DataSource=demarcheur.refresh(dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString());

                DB.historique hist = new DB.historique();
                dgvV.DataSource = hist.getVersement(id);

                prix.Text=dgvP.Rows[0].Cells["mprix"].Value.ToString();
                montant.Text=dgvP.Rows[0].Cells["mreste"].Value.ToString();
                label1.Text= "Frais";
            }
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
                if (!mutation)
                {
                    DB.stock stock = new DB.stock();
                    DB.historique hist = new DB.historique();
                    DB.printer printer = new DB.printer();
                    string etat = "";
                    string action;
                    bool vendue=false;
                    if(dgvV.Rows.Count==1)
                        action = "1er Versement";
                    else
                        action = (dgvV.Rows.Count+1).ToString()+"eme Versement";

                    if (decimal.Parse(reste.Text)>0)
                    {
                        etat= "En cours de vente";
                    }
                    else
                    {
                        etat="Vendue";
                        vendue= true;
                    }

                    if (stock.setVersement(id, (decimal.Parse(prix.Text.Trim())-decimal.Parse(reste.Text)).ToString(), reste.Text, etat))
                    {
                        hist.add(action, id, dgvP.Rows[0].Cells["idclient"].Value.ToString(),
                            dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString(),
                            prix.Text.Trim(), versement.Text.Trim(), reste.Text,
                            dgvP.Rows[0].Cells["commission"].Value.ToString(),
                            dgvP.Rows[0].Cells["type_usage"].Value.ToString());

                        
                            Waiting wait = new Waiting(dgvP,dgvC,action,prix.Text,versement.Text,reste.Text);
                            wait.ShowDialog();
                        
                        this.Close();
                    }
                }
                else
                {
                    DB.stock stock = new DB.stock();
                    DB.historique hist = new DB.historique();
                    DB.printer printer = new DB.printer();
                    string etat;
                    string action;

                    if (dgvV.Rows.Count==1)
                        action = "1er Versement";
                    else
                        action = (dgvV.Rows.Count+1).ToString()+"eme Versement";
                    
                    if (decimal.Parse(reste.Text)>0)
                    {
                        etat= "En cours de Mutation";
                    }
                    else
                    {
                        etat="Mutée";
                    }

                    if (stock.setVersementPrive(id, (decimal.Parse(prix.Text.Trim())-decimal.Parse(reste.Text)).ToString(), reste.Text, etat))
                    {
                        hist.add(action, id, dgvP.Rows[0].Cells["idold"].Value.ToString(),
                            dgvP.Rows[0].Cells["idclient"].Value.ToString(),
                            dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString(),
                            prix.Text.Trim(), versement.Text.Trim(), reste.Text,
                            dgvP.Rows[0].Cells["type_usage"].Value.ToString());

                        Waiting wait = new Waiting(dgvP, dgvC, action, prix.Text, versement.Text, reste.Text);
                        wait.ShowDialog();
                        /*
                        printer.versement("Mutation de terrain",
                            dgvC.Rows[0].Cells["nom"].Value.ToString()+" "+dgvC.Rows[0].Cells["prenom"].Value.ToString(),
                            dgvC.Rows[0].Cells["piece"].Value.ToString()+" N° "+dgvC.Rows[0].Cells["numero"].Value.ToString()+" du "+dgvC.Rows[0].Cells["delivrance"].Value.ToString(),
                            dgvC.Rows[0].Cells["contact"].Value.ToString(), dgvP.Rows[0].Cells["lot"].Value.ToString(), dgvP.Rows[0].Cells["parcelle"].Value.ToString(),
                             dgvP.Rows[0].Cells["superficie"].Value.ToString(), action,
                             versement.Text, prix.Text,
                             (decimal.Parse(prix.Text)-decimal.Parse(reste.Text)).ToString(),
                             reste.Text, dgvP.Rows[0].Cells["id"].Value.ToString());
                        */
                        this.Close();
                    }
                }
            }
            else
                MessageBox.Show("Erreur! Réverifiez les données saisies");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
