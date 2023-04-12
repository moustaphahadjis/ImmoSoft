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
        DataGridView dgv1, dgvP, dgvC, dgvD;
        string name;
        int time;
        bool finish=false;

        //versement
        string action, prix, versement, reste;
        bool payment = false, fiche = false, attribution = false, decharge=false, acte=false, historique = false;
        
        //attribution
        DataRow client, site, parcelle;

        //decharge
        string montant, sitenom;

        //historique
        public Waiting(DataGridView dgv)
        {
            InitializeComponent();
            dgv1=dgv;
            this.historique = true;
        }
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
        public Waiting(DataGridView dgvp, DataGridView dgvd, string montant,string site)
        {
            InitializeComponent();
            dgvP=dgvp; dgvD=dgvd;
            this.montant = montant;
            this.sitenom=site;
            decharge=true;
        }
        public Waiting(DataRow parcelle, DataRow client, string site)
        {
            InitializeComponent();
            this.client = client;
            this.parcelle= parcelle;
            this.sitenom= site;
            acte=true;
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
                    client["piece"]+" n° "+client["numero"]+" du "+client["delivrance"],
                    client["addresse"].ToString(), client["profession"].ToString(), client["contact"].ToString(),
                    site["ville"].ToString(), site["nom"].ToString(),
                    parcelle["section"].ToString(), parcelle["lot"].ToString(), parcelle["parcelle"].ToString(), parcelle["type_usage"].ToString(),
                    parcelle["superficie"].ToString(), parcelle["id"].ToString());
                finish= true;
            }
            else if(fiche)
            {
                printer.fiche(
                    client["nom"].ToString(), client["prenom"].ToString(), client["matrimonial"].ToString(),
                    client["piece"]+" n° "+client["numero"]+" du "+client["delivrance"],
                    client["addresse"].ToString(), client["profession"].ToString(), client["contact"].ToString(),
                    site["ville"].ToString(), site["nom"].ToString(),
                    parcelle["section"].ToString(), parcelle["lot"].ToString(), parcelle["parcelle"].ToString(), parcelle["type_usage"].ToString(),
                    parcelle["superficie"].ToString(), parcelle["id"].ToString());
                finish= true;
            }
            else if(decharge)
            {
                printer.decharge(dgvD.Rows[0].Cells["nom"].Value.ToString(),
                    dgvD.Rows[0].Cells["prenom"].Value.ToString(),
                    dgvD.Rows[0].Cells["piece"].Value.ToString()+" n° "+
                    dgvD.Rows[0].Cells["numero"].Value.ToString()+" du "+
                    dgvD.Rows[0].Cells["delivrance"].Value.ToString(),
                    dgvD.Rows[0].Cells["contact"].Value.ToString(),
                    sitenom,
                    dgvP.Rows[0].Cells["lot"].Value.ToString(),
                    dgvP.Rows[0].Cells["parcelle"].Value.ToString(),
                    dgvP.Rows[0].Cells["superficie"].Value.ToString(),
                    dgvP.Rows[0].Cells["id"].Value.ToString(),
                    montant);
                finish= true;
            }
            else if (acte)
            {
                printer.acte(client["nom"].ToString()+" "+
                    client["prenom"].ToString(),
                    client["piece"].ToString()+" n° "+
                    client["numero"].ToString()+" du "+
                    client["delivrance"].ToString(), 
                    sitenom,
                    parcelle["lot"].ToString(),
                    parcelle["parcelle"].ToString(),
                    parcelle["superficie"].ToString(),
                    parcelle["prix"].ToString(),
                    parcelle["montant"].ToString(),
                    parcelle["reste"].ToString(),
                    parcelle["id"].ToString());
                finish= true;
            }
            else if (historique)
            {
                printer.historique(dgv1);
                finish= true;
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
