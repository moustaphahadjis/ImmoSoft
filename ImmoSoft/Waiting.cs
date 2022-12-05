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
    public partial class Waiting : Form
    {
        DataGridView dgv1, dgvP, dgvC;
        string name;
        int time;
        bool finish=false;

        //versement
        string action, prix, versement, reste;
        bool payment = false, fiche = false, attribution = false;

        //attribution
        DataRow client, site, parcelle;
        public Waiting(DataGridView dgv, string name)
        {
            InitializeComponent();
            dgv1=dgv;
            this.name = name;
        }
        public Waiting(DataGridView dgvp, DataGridView dgvc, string action,
            string prix, string versement, string reste)
        {
            InitializeComponent();
            dgvP=dgvp; dgvC=dgvc;
            this.action = action;
            this.prix = prix;
            this.versement = versement;
            this.reste = reste;
            payment=true;
        }
        public Waiting(DataRow client, DataRow site, DataRow parcelle, bool attribution)
        {
            InitializeComponent();
            this.client = client;
            this.site = site;
            this.parcelle= parcelle;
            if (attribution)
                this.attribution=true;
            else
                this.fiche=true;
        }

        private void Waiting_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        void work(object s, DoWorkEventArgs e)
        {
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time>3)
            {
                timer1.Stop();
                backgroundWorker1.RunWorkerAsync();
            }
            else
                time++;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DB.printer printer = new DB.printer();

            if (payment)
            {
                printer.versement("Achat de terrain",
                            dgvC.Rows[0].Cells["nom"].Value.ToString()+" "+dgvC.Rows[0].Cells["prenom"].Value.ToString(),
                            dgvC.Rows[0].Cells["piece"].Value.ToString()+" N° "+dgvC.Rows[0].Cells["numero"].Value.ToString()+" du "+dgvC.Rows[0].Cells["delivrance"].Value.ToString(),
                            dgvC.Rows[0].Cells["contact"].Value.ToString(), dgvP.Rows[0].Cells["lot"].Value.ToString(), dgvP.Rows[0].Cells["parcelle"].Value.ToString(),
                             dgvP.Rows[0].Cells["superficie"].Value.ToString(), action,
                             versement, prix,
                             (decimal.Parse(prix)-decimal.Parse(reste)).ToString(),
                             reste, dgvP.Rows[0].Cells["id"].Value.ToString());
                finish= true;
            }
            else if (attribution)
            {

                printer.attestation(
                    client["nom"].ToString(), client["prenom"].ToString(), client["matrimonial"].ToString(),
                    client["piece"]+" n "+client["numero"]+" du "+client["delivrance"],
                    client["addresse"].ToString(), client["profession"].ToString(), client["contact"].ToString(),
                    site["ville"].ToString(), site["nom"].ToString(),
                    parcelle["section"].ToString(), parcelle["lot"].ToString(), parcelle["parcelle"].ToString(), parcelle["type_usage"].ToString(),
                    parcelle["superficie"].ToString(), parcelle["id"].ToString());
            }
            else if(fiche)
            {
                printer.fiche(
                    client["nom"].ToString(), client["prenom"].ToString(), client["matrimonial"].ToString(),
                    client["piece"]+" n "+client["numero"]+" du "+client["delivrance"],
                    client["addresse"].ToString(), client["profession"].ToString(), client["contact"].ToString(),
                    site["ville"].ToString(), site["nom"].ToString(),
                    parcelle["section"].ToString(), parcelle["lot"].ToString(), parcelle["parcelle"].ToString(), parcelle["type_usage"].ToString(),
                    parcelle["superficie"].ToString(), parcelle["id"].ToString());
            }
            else
            {
                printer.export(dgv1, name);
                finish= true;

            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
