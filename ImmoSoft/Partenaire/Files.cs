using Microsoft.Office.Interop.Excel;
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
        bool demarcheur=false;
        string idatt;
        public Files(string Nom, string id, string type)
        {
            InitializeComponent();
            nom = Nom;
            if (type=="stock")

            {
                idstock = id;
                refresh(printer.refresh(nom, id, null));
            }
            else if(type=="stock dispo")
            {
                idstock = id;
                demarcheur= true;
                refresh(printer.refresh(nom, id, null));
            }
            else if (type=="champs dispo")
            {
                idchamps = id;
                demarcheur= true;
                refresh(printer.refresh(nom, null, id));
            }
            else
            {
                idchamps = id;
                refresh(printer.refresh(nom, null, id));
            }
            
        }
        public Files(string Nom, string id, string idatt, string type)
        {
            InitializeComponent();
            nom = Nom;
            idstock = id;
            this.idatt=idatt;
            refresh(printer.refresh(nom, id, null));
        }
            void refresh(System.Data.DataTable dt)
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
                DB.demarcheur dem = new DB.demarcheur();
                DB.site si = new DB.site();
                DB.stock st = new DB.stock();
                DB.stock ch = new DB.stock();
                DB.attribution att = new DB.attribution();
                DataRow attribution = att.refresh(idatt).Rows[0];
                DataRow parcelle;
                DataRow person;

                if (idstock!=null)
                {
                    parcelle = st.refresh(idstock).Rows[0];
                    if (!demarcheur)
                        person = cl.refresh(parcelle["idclient"].ToString()).Rows[0];
                    else
                        person=dem.refresh(parcelle["iddemarcheur"].ToString()).Rows[0];
                }
                else
                {
                    parcelle = ch.refresh(idchamps).Rows[0];
                    if (!demarcheur)
                        person = cl.refresh(parcelle["idnew"].ToString()).Rows[0];
                    else
                        person=dem.refresh(parcelle["iddemarcheur"].ToString()).Rows[0];
                }


                DataRow site = si.refresh(parcelle["siteid"].ToString()).Rows[0];
                if (nom.ToLower().Contains("attestation"))
                {
                    Waiting wait = new Waiting(person, site, parcelle, true);
                    wait.ShowDialog();
                    refresh(printer.refresh(nom, idstock, idchamps));
                }
                else if (nom.ToLower().Contains("attribution"))
                {
                    person=cl.refresh(attribution["idnew"].ToString()).Rows[0];
                    Waiting wait = new Waiting(person, site, parcelle, true);
                    wait.ShowDialog();
                    refresh(printer.refresh(nom, idstock, idchamps));
                }
                else if (nom.ToLower().Contains("fiche"))
                {
                    Waiting wait = new Waiting(person, site, parcelle, false);
                    wait.ShowDialog();
                    refresh(printer.refresh(nom, idstock, idchamps));
                }
                else if (nom.ToLower().Contains("acte"))
                {
                    Waiting wait = new Waiting(parcelle, person, site["nom"].ToString());
                    wait.ShowDialog();
                    refresh(printer.refresh(nom, idstock, idchamps));
                }
            }
            catch (Exception ex)
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
