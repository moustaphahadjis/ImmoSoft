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
    public partial class Mutation : Form
    {
        string pid = "0", cid = "0", did = "0", oid="0";
        DB.common com = new DB.common();
        public Mutation(string id, char option)
        {
            InitializeComponent();
            dgvP.DataSourceChanged+=(s, a) =>
            {
                if (dgvP.Rows.Count>0)
                {

                    pid=dgvP.Rows[0].Cells["id"].Value.ToString();
                    prix.Text=dgvP.Rows[0].Cells["prix"].Value.ToString();
                    usage.Text=dgvP.Rows[0].Cells["type_usage"].Value.ToString();
                    vsmt.Text = dgvP.Rows[0].Cells["montant"].Value.ToString();
                    rest.Text = dgvP.Rows[0].Cells["reste"].Value.ToString();

                    if (dgvP.Rows[0].Cells["idold"].Value.ToString()!="0")
                    {
                        cid = dgvP.Rows[0].Cells["idold"].Value.ToString();
                        DB.client client = new DB.client();
                        dgvO.DataSource=client.refresh(cid);
                    }
                    if (dgvP.Rows[0].Cells["idnew"].Value.ToString()!="0")
                    {
                        cid = dgvP.Rows[0].Cells["idnew"].Value.ToString();
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
                DB.champs stock = new DB.champs();
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

            oid=dgvO.Rows[0].Cells["id"].Value.ToString();
        }


        private void selectP_Click(object sender, EventArgs e)
        {
            Stock st = new Stock(true);
            st.FormClosing+=(s, args) =>
            {
                pid=st.id;
                DB.champs champs = new DB.champs();
                dgvP.DataSource=champs.refresh(pid);
            };
            st.ShowDialog();
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
            if (prix.Text.Trim().Length>0 && vsmt.Text.Trim().Length>0)
                if (com.isNumber(prix.Text.Trim()) && com.isNumber(vsmt.Text.Trim()))
                    rest.Text = (decimal.Parse(prix.Text.Trim())-decimal.Parse(vsmt.Text.Trim())).ToString();
                else
                    rest.Text = "0";
            else
                rest.Text="0";
        }
        private void confirm_Click(object sender, EventArgs e)
        {
            if (check())
            {
                DB.champs champs = new DB.champs();
                DB.historiqueChamps hist = new DB.historiqueChamps();
                string etat = "";
                string action = "Mutation assignée";
                //gerer le rang des versement
                if (decimal.Parse(rest.Text)>0)
                {
                    etat= "En cours de Mutation";
                }
                else
                {
                    etat="Mutée";
                }

                if (champs.mutation(pid,oid, cid, did, prix.Text.Trim(), vsmt.Text.Trim(), rest.Text, usage.Text, etat))
                {
                    hist.add(action, pid, oid, cid, did, prix.Text.Trim(), vsmt.Text.Trim(), rest.Text, usage.Text);
                    /*
                    DB.printer printer = new DB.printer();
                    printer.versement("Mutation",
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

        private void vsmt_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void prix_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void selectD_Click(object sender, EventArgs e)
        {
            Clients tmp = new Clients(true);
            tmp.FormClosing+=(s, args) =>
            {
                cid=tmp.id;
                DB.client stock = new DB.client();
                dgvC.DataSource=stock.refresh(cid);
            };
            tmp.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Demarcheurs tmp = new Demarcheurs(true);
            tmp.FormClosing+=(s, args) =>
            {
                did=tmp.id;
                DB.demarcheur demarcheur = new DB.demarcheur();
                dgvD.DataSource=demarcheur.refresh(did);
            };
            tmp.ShowDialog();
        }

        private void Mutation_Load(object sender, EventArgs e)
        {

        }
    }
}
