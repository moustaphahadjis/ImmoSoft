using Microsoft.Office.Interop.Excel;
using OpenCvSharp;
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
    public partial class Cloture : Form
    {
        string id;
        string pid = "0", cid = "0", did = "0", oid = "0";
        string prix, mnt, rest, comm, usage;
        bool mutation;
        public Cloture(string ID, bool muter)
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
                
                DB.historique hist = new DB.historique();
                dgvV.DataSource = hist.getVersement(id);
                pid=dgvP.Rows[0].Cells["id"].Value.ToString();
                did=dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString();
                cid=dgvC.Rows[0].Cells["id"].Value.ToString();

                prix=dgvP.Rows[0].Cells["prix"].Value.ToString();
                mnt=dgvP.Rows[0].Cells["montant"].Value.ToString();
                rest=dgvP.Rows[0].Cells["reste"].Value.ToString();
                usage=dgvP.Rows[0].Cells["type_usage"].Value.ToString();
                comm=dgvP.Rows[0].Cells["commission"].Value.ToString();
            }
            else
            {
                DB.stock stock = new DB.stock();
                dgvP.DataSource=stock.refresh(id);
                DB.client client = new DB.client();
                dgvC.DataSource=client.refresh(dgvP.Rows[0].Cells["idclient"].Value.ToString());
                
                DB.historique hist = new DB.historique();
                dgvV.DataSource = hist.getVersement(id);

                pid=dgvP.Rows[0].Cells["id"].Value.ToString();
                did=dgvP.Rows[0].Cells["iddemarcheur"].Value.ToString();
                oid=dgvP.Rows[0].Cells["idold"].Value.ToString();
                cid=dgvC.Rows[0].Cells["id"].Value.ToString();

                prix=dgvP.Rows[0].Cells["prix"].Value.ToString();
                mnt=dgvP.Rows[0].Cells["montant"].Value.ToString();
                rest=dgvP.Rows[0].Cells["reste"].Value.ToString();
                comm=dgvP.Rows[0].Cells["commission"].Value.ToString();
                usage=dgvP.Rows[0].Cells["type_usage"].Value.ToString();
            }
        }

        private void Cloture_Load(object sender, EventArgs e)
        {

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            DB.attribution att = new DB.attribution();
            DB.historique hist= new DB.historique();
            DB.historique histc=new DB.historique();
            DB.stock stock= new DB.stock();
            if (!mutation)
            {
                att.insert("0", pid, "0", cid, true);
                stock.cloturer(pid,cid);
                hist.add("Cloture", pid, cid, did, prix, mnt, rest,comm, usage);
            }
            else
            {

                att.insert(pid, "0", oid, cid, true);
                stock.cloturer(pid,cid);
                histc.add("Cloture", pid,oid, cid, did, prix, mnt, rest, usage);
            }
            this.Close();
        }
    }
}
