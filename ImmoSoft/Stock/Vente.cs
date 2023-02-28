using System;
using System.CodeDom;
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
    public partial class Vente : Form
    {
        string pid = "0", cid= "0", did= "0";
        DB.common com = new DB.common();
        bool don= false;
        bool update= false;
        public Vente(string id, char option, bool Don)
        {
            InitializeComponent();
            don= Don;
            if (don)
            {
                prix.Enabled=false;
                confirm.Text="Confirmer don";
            }


            dgvP.DataSourceChanged+=(s, a) =>
            {
                if (dgvP.Rows.Count>0)
                {
                    cid = dgvP.Rows[0].Cells["idclient"].Value.ToString();
                    prix.Text=dgvP.Rows[0].Cells["prix"].Value.ToString();
                    usage.Text=dgvP.Rows[0].Cells["type_usage"].Value.ToString();
                    vsmt.Text = dgvP.Rows[0].Cells["montant"].Value.ToString();
                    rest.Text = dgvP.Rows[0].Cells["reste"].Value.ToString();

                    if (dgvP.Rows[0].Cells["idclient"].Value.ToString()!="0")
                    {
                        DB.client client = new DB.client();
                        dgvC.DataSource=client.refresh(cid);
                    }
                    if (dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString()!="0")
                    {
                        did = dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString();
                        DB.demarcheur demarcheur = new DB.demarcheur();
                        dgvD.DataSource=demarcheur.refresh(did);
                    }
                }
            };
            if (option=='P')
            {
                pid = id;
                DB.stock stock = new DB.stock();
                dgvP.DataSource=stock.refresh(pid);
            }
            else if (option=='C')
            {
                cid = id;
                DB.client client = new DB.client();
                dgvC.DataSource=client.refresh(cid);
            }
            else if (option=='D')
            {
                did = id;
                DB.demarcheur demarcheur = new DB.demarcheur();
                dgvD.DataSource=demarcheur.refresh(did);
            }
        }

        public Vente(string id)
        {
            InitializeComponent();
            update=true;

            dgvP.DataSourceChanged+=(s, a) =>
            {
                if (dgvP.Rows.Count>0)
                {
                    pid=dgvP.Rows[0].Cells["id"].Value.ToString();
                    prix.Text=dgvP.Rows[0].Cells["prix"].Value.ToString();
                    usage.Text=dgvP.Rows[0].Cells["type_usage"].Value.ToString();
                    vsmt.Text = dgvP.Rows[0].Cells["montant"].Value.ToString();
                    rest.Text = dgvP.Rows[0].Cells["reste"].Value.ToString();

                    if (dgvP.Rows[0].Cells["idclient"].Value.ToString()!="0")
                    {
                        cid = dgvP.Rows[0].Cells["idclient"].Value.ToString();
                        DB.client client = new DB.client();
                        dgvC.DataSource=client.refresh(cid);
                    }
                    if (dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString()!="0")
                    {
                        did = dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString();
                        DB.demarcheur demarcheur = new DB.demarcheur();
                        dgvD.DataSource=demarcheur.refresh(did);
                    }
                }
            };
            pid=id;
                DB.stock stock = new DB.stock();
                dgvP.DataSource=stock.refresh(pid);
        }

        private void selectC_Click(object sender, EventArgs e)
        {
            Clients tmp = new Clients(true);
            tmp.FormClosing+=(s, args) =>
            {
                cid=tmp.id;
                if (!String.IsNullOrEmpty(cid))
                {
                    DB.client stock = new DB.client();
                    dgvC.DataSource=stock.refresh(cid);
                }
                else
                    cid="0";
            };
            tmp.ShowDialog();
        }
        bool check()
        {
            bool r = false;
            if (dgvP.Rows.Count>0 &&
                dgvC.Rows.Count>0)
            {
                decimal tmp = 0;
                if (decimal.TryParse(rest.Text.Trim(), out tmp))
                    if (tmp>=0)
                        r=true;
            }
            return r;
        }
        void calculate()
        {
            if (prix.Text.Trim().Length>0 && vsmt.Text.Trim().Length>0 && commission.Text.Trim().Length>0)
                if (com.isNumber(prix.Text.Trim()) && com.isNumber(vsmt.Text.Trim()) && com.isNumber(commission.Text))
                    rest.Text = (decimal.Parse(prix.Text.Trim())-decimal.Parse(vsmt.Text.Trim())).ToString();
                else
                    rest.Text = "";
            else
                rest.Text="";
        }
        private void selectD_Click(object sender, EventArgs e)
        {
            Demarcheurs tmp = new Demarcheurs(true);
            tmp.FormClosing+=(s, args) =>
            {
                did=tmp.id;
                if (!String.IsNullOrEmpty(did))
                {
                    DB.demarcheur demarcheur = new DB.demarcheur();
                    dgvD.DataSource=demarcheur.refresh(did);
                    commission.ReadOnly=false;
                }
                else
                {
                    did="0";
                    commission.Text="0";
                    commission.ReadOnly=true;
                }
            };
            tmp.ShowDialog();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            if (!don)
            {
                if (check())
                {
                    DB.stock stock = new DB.stock();
                    DB.historique hist = new DB.historique();
                    string action, etat;
                    if (update)
                        action = "Vente Modifiée";
                    else
                        action = "Vente assignée";

                    if (decimal.Parse(rest.Text)>0)
                        etat= "En cours de vente";
                    else
                        etat="Vendue";

                    if (stock.vente(pid, cid, did, prix.Text.Trim(), vsmt.Text,rest.Text,commission.Text, usage.Text, etat))
                    {
                        hist.add(action, pid, cid, did, prix.Text.Trim(), vsmt.Text, rest.Text, commission.Text, usage.Text);
                        
                        /*
                        DB.printer printer = new DB.printer();
                        printer.versement("Achat de terrain",
                            dgvC.Rows[0].Cells["nom"].Value.ToString()+" "+dgvC.Rows[0].Cells["prenom"].Value.ToString(),
                            dgvC.Rows[0].Cells["piece"].Value.ToString()+" N° "+dgvC.Rows[0].Cells["numero"].Value.ToString()+" du "+dgvC.Rows[0].Cells["delivrance"].Value.ToString(),
                            dgvC.Rows[0].Cells["contact"].Value.ToString(), dgvP.Rows[0].Cells["lot"].Value.ToString(), dgvP.Rows[0].Cells["parcelle"].Value.ToString(),
                             dgvP.Rows[0].Cells["superficie"].Value.ToString(), action,
                             vsmt.Text, prix.Text,
                             (decimal.Parse(prix.Text)-decimal.Parse(rest.Text)).ToString(),
                             rest.Text, dgvP.Rows[0].Cells["id"].Value.ToString());
                        */
                        this.Close();
                    }
                }
            }
            else
            {
                if (dgvP.Rows.Count>0 &&
                    dgvC.Rows.Count>0)
                {
                    DB.stock stock = new DB.stock();
                    DB.historique hist = new DB.historique();
                    string etat = "Don";
                    string action = "Don";

                    if (stock.vente(pid, cid, did, "0", "0", "0", commission.Text, usage.Text, etat))
                    {
                        hist.add(action, pid, cid, did, "0", "0", "0", commission.Text, usage.Text);
                        
                        /*
                        DB.printer printer = new DB.printer();
                        printer.versement("Achat de terrain",
                            dgvC.Rows[0].Cells["nom"].Value.ToString()+" "
                            +dgvC.Rows[0].Cells["prenom"].Value.ToString(),
                            dgvC.Rows[0].Cells["piece"].Value.ToString()+" N° "
                            +dgvC.Rows[0].Cells["numero"].Value.ToString()+" du "
                            +dgvC.Rows[0].Cells["delivrance"].Value.ToString(),
                            dgvC.Rows[0].Cells["contact"].Value.ToString(), 
                            dgvP.Rows[0].Cells["lot"].Value.ToString(), 
                            dgvP.Rows[0].Cells["parcelle"].Value.ToString(),
                             dgvP.Rows[0].Cells["superficie"].Value.ToString(), action,
                             "0","0","0","0", dgvP.Rows[0].Cells["id"].Value.ToString());
                        */
                        this.Close();
                    }
                }
            }
        }

        private void prix_TextChanged_1(object sender, EventArgs e)
        {
            calculate();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void reinit_Click(object sender, EventArgs e)
        {

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteC_Click(object sender, EventArgs e)
        {
            cid="0";
            if (dgvC.Rows.Count>0)
                dgvC.Rows.Remove(dgvC.Rows[0]);
        }

        private void deleteD_Click(object sender, EventArgs e)
        {
            did="0";
            if (dgvD.Rows.Count>0)
                dgvD.Rows.Remove(dgvD.Rows[0]);
        }

        private void Vente_Load(object sender, EventArgs e)
        {
            
        }

        private void selectP_Click(object sender, EventArgs e)
        {
            Stock st = new Stock(true);
            st.FormClosing+=(s, args) =>
            {
                pid=st.id;
                DB.stock stock = new DB.stock();
                dgvP.DataSource=stock.refresh(pid);
            };
            st.ShowDialog();
        }
    }
}
