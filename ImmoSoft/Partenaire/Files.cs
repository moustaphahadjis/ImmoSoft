using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmoSoft
{
    public partial class Files : Form
    {
        DB.printer printer = new DB.printer();
        string nom = "";
        string idstock;
        string idchamps;
        public Files(string Nom, string id, string type)
        {
            InitializeComponent();
            nom = Nom;
            if (type=="stock")

            {
                idstock = id;
                refresh(printer.refresh(nom, id, null));
            }
            else
            {
                idchamps = id;
                refresh(printer.refresh(nom, null, id));
            }
            
        }
        void refresh(DataTable dt)
        {
            dgv1.DataSource=dt;
        }
        private void Files_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count > 0)
                if (dgv1.SelectedRows.Count>0)
                {
                    string path = printer.open(dgv1.SelectedRows[0].Cells["id"].Value.ToString());
                    if (path!=null)
                        Process.Start(path);
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DB.client cl = new DB.client();
                DB.site si = new DB.site();
                DB.stock st = new DB.stock();
                DB.champs ch = new DB.champs();
                DataRow parcelle;
                DataRow client;

                if (idstock!=null)
                {
                    parcelle = st.refresh(idstock).Rows[0];
                    client = cl.refresh(parcelle["idclient"].ToString()).Rows[0];
                }
                else
                {
                    parcelle = ch.refresh(idchamps).Rows[0]; 
                    client = cl.refresh(parcelle["idnew"].ToString()).Rows[0];
                }

                    DataRow site = si.refresh(parcelle["siteid"].ToString()).Rows[0];
                if (nom.ToLower().Contains("attestation"))
                {
                    printer.attestation(
                        client["nom"].ToString(), client["prenom"].ToString(), client["matrimonial"].ToString(),
                        client["piece"]+" n "+client["numero"]+" du "+client["delivrance"],
                        client["addresse"].ToString(), client["profession"].ToString(), client["contact"].ToString(),
                        site["ville"].ToString(), site["nom"].ToString(),
                        parcelle["section"].ToString(), parcelle["lot"].ToString(), parcelle["parcelle"].ToString(), parcelle["type_usage"].ToString(),
                        parcelle["superficie"].ToString(), parcelle["id"].ToString());
                    refresh(printer.refresh(nom,idstock,idchamps));
                }
                else if (nom.ToLower().Contains("fiche"))
                {
                    printer.fiche(
                        client["nom"].ToString(), client["prenom"].ToString(), client["matrimonial"].ToString(),
                        client["piece"]+" n "+client["numero"]+" du "+client["delivrance"],
                        client["addresse"].ToString(), client["profession"].ToString(), client["contact"].ToString(),
                        site["ville"].ToString(), site["nom"].ToString(),
                        parcelle["section"].ToString(), parcelle["lot"].ToString(), parcelle["parcelle"].ToString(), parcelle["type_usage"].ToString(),
                        parcelle["superficie"].ToString(), parcelle["id"].ToString());
                    refresh(printer.refresh(nom, idstock, idchamps));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count > 0)
                if (dgv1.SelectedRows.Count>0)
                {
                    if (MessageBox.Show("Voulez-vous vraiment supprimer?", "Supprimer",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                        printer.delete(dgv1.SelectedRows[0].Cells["id"].Value.ToString());
                }
        }
    }
}
