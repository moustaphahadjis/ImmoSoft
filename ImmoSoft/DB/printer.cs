using ImmoSoft.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace ImmoSoft.DB
{
    internal class printer
    {
        MySqlConnection con;
        MySqlCommand cmd;
        string path;
        public printer()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\ImmoSoft\";
        }
        public bool export(DataGridView data,string site)
        {
            Word.Application app = new Word.Application();
            try
            {
                app.ShowAnimation = false;
                app.Visible=false;

                string filenum = (getLast("Export")+1).ToString();
                string filename = @"Export n"+filenum+".docx";
                string filepath = path + filename;
                Word.Document doc = app.Documents.Open(path+@"Export.docx");

                app.Selection.Find.Execute(FindText: "@site", ReplaceWith: site, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@date", ReplaceWith: DateTime.Now.ToString("dd-MM-yyyy"), Replace: Word.WdReplace.wdReplaceAll);

                string lot = "";
                int last = 0;
                for(int i=0; i < data.Rows.Count; i++)
                {
                    DataGridViewRow row = data.Rows[i];
                    doc.Tables[1].Rows.Add(doc.Tables[1].Rows[i+2]);

                    doc.Tables[1].Rows[i+2].Cells[1].Range.Text = (i+1).ToString();
                    if (lot!=row.Cells["lot"].Value.ToString())
                    {
                        lot=row.Cells["lot"].Value.ToString();
                        doc.Tables[1].Rows[i+2].Cells[2].Range.Text = lot;
                    }
                    doc.Tables[1].Rows[i+2].Cells[3].Range.Text = row.Cells["Parcelle"].Value.ToString();
                    doc.Tables[1].Rows[i+2].Cells[4].Range.Text = row.Cells["Superficie"].Value.ToString();
                    if (data.Columns.Contains("Montant"))
                    doc.Tables[1].Rows[i+2].Cells[5].Range.Text = row.Cells["Montant"].Value.ToString();
                    if(data.Columns.Contains("Reste"))
                    doc.Tables[1].Rows[i+2].Cells[6].Range.Text = row.Cells["Reste"].Value.ToString();
                    last=i;
                }
                doc.Tables[1].Rows[last+3].Delete();
                doc.SaveAs2(filepath);
                Object oMissing = System.Reflection.Missing.Value;
                doc.ExportAsFixedFormat(filepath.Replace("docx","pdf"), Word.WdExportFormat.wdExportFormatPDF, false,
                    Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen,
                    Word.WdExportRange.wdExportAllDocument, 1, 1, Word.WdExportItem.wdExportDocumentContent, true, true,
                    Word.WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, ref oMissing);
                doc.Close();
                app.Quit();

                save(filenum,filename.Replace("docx", "pdf"),
                    new FileStream(filepath, FileMode.Open, FileAccess.Read),"0");

                Process.Start(filepath.Replace("docx", "pdf"));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erreur lors de l'exportation");
                return false;
            }
        }
        public bool versement(string nom, string identity, string contact,
            string lot, string parcelle, string superficie,
            string motif, string montant,
            string prix, string total, string reste, string idHistorique)
        {
            Word.Application app = new Word.Application();
            convertir con = new convertir();
            try
            {
                app.ShowAnimation = false;
                app.Visible=false;
                string filenum = (getLast("Recu")+1).ToString();
                string filename = @"Recu n"+filenum+".docx";
                string filepath = path + filename;
                Word.Document doc = app.Documents.Open(path+@"Versement.docx");

                app.Selection.Find.Execute(FindText: "@num", ReplaceWith: filenum, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@nom", ReplaceWith: nom, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@identity", ReplaceWith: identity, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@contact", ReplaceWith: contact, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@lot", ReplaceWith: lot, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@parcelle", ReplaceWith: parcelle, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@superficie", ReplaceWith: superficie, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@motif", ReplaceWith: motif, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@montant", ReplaceWith: montant, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@lettre", ReplaceWith: con.enLettre(montant), Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@prix", ReplaceWith: prix+" FCFA", Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@total", ReplaceWith: total+" FCFA", Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@reste", ReplaceWith: reste+" FCFA", Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@date", ReplaceWith: DateTime.Now.ToString("dd-MM-yyyy"), Replace: Word.WdReplace.wdReplaceAll);

                
                doc.SaveAs2(filepath);

                Object oMissing = System.Reflection.Missing.Value;
                doc.ExportAsFixedFormat(filepath.Replace("docx", "pdf"), Word.WdExportFormat.wdExportFormatPDF, false,
                    Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen,
                    Word.WdExportRange.wdExportAllDocument, 1, 1, Word.WdExportItem.wdExportDocumentContent, true, true,
                    Word.WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, ref oMissing);
                doc.Close();
                app.Quit();

                save(filenum, filename.Replace("docx", "pdf"), 
                    new FileStream(filepath, FileMode.Open, FileAccess.Read), idHistorique);

                Process.Start(filepath.Replace("docx", "pdf"));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erreur lors de l'exportation");
                if (app!=null)
                    app.Quit();
                return false;
            }
        }


        public bool attestation(string nom, string identity, string contact, 
            string matrimonial, string adresse, string profession,
            string ville, string site, string section, string usage,
           string lot, string parcelle, string superficie,
           string motif, string montant,
           string prix, string total, string reste, string idHistorique)
        {
            Word.Application app = new Word.Application();
            convertir con = new convertir();
            try
            {
                app.ShowAnimation = false;
                app.Visible=false;
                string filenum = (getLast("Attestation")+1).ToString();
                string filename = @"Attestation n"+filenum+".docx";
                string filepath = path + filename;
                Word.Document doc = app.Documents.Open(path+@"Attestation.docx");

                app.Selection.Find.Execute(FindText: "@num", ReplaceWith: filenum, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@nom", ReplaceWith: nom, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@matrimonial", ReplaceWith: matrimonial, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@adresse", ReplaceWith: adresse, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@profession", ReplaceWith: profession, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@ville", ReplaceWith: ville, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@site", ReplaceWith: site, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@section", ReplaceWith: section, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@usage", ReplaceWith: usage, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@identity", ReplaceWith: identity, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@contact", ReplaceWith: contact, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@lot", ReplaceWith: lot, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@parcelle", ReplaceWith: parcelle, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@superficie", ReplaceWith: superficie, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@motif", ReplaceWith: motif, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@montant", ReplaceWith: montant, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@lettre", ReplaceWith: con.enLettre(montant), Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@prix", ReplaceWith: prix+" FCFA", Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@total", ReplaceWith: total+" FCFA", Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@reste", ReplaceWith: reste+" FCFA", Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@date", ReplaceWith: DateTime.Now.ToString("dd-MM-yyyy"), Replace: Word.WdReplace.wdReplaceAll);


                doc.SaveAs2(filepath);

                Object oMissing = System.Reflection.Missing.Value;
                doc.ExportAsFixedFormat(filepath.Replace("docx", "pdf"), Word.WdExportFormat.wdExportFormatPDF, false,
                    Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen,
                    Word.WdExportRange.wdExportAllDocument, 1, 1, Word.WdExportItem.wdExportDocumentContent, true, true,
                    Word.WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, ref oMissing);
                doc.Close();
                app.Quit();

                save(filenum, filename.Replace("docx", "pdf"),
                    new FileStream(filepath, FileMode.Open, FileAccess.Read), idHistorique);

                Process.Start(filepath.Replace("docx", "pdf"));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erreur lors de l'exportation");
                return false;
            }
        }

        public bool save(string num, string nom, FileStream file, string idhistorique)
        {
            try
            {
                UInt32 size = (UInt32)file.Length;
                byte[] data = new byte[size];
                file.Read(data, 0, data.Length);
                file.Close();

                if (con.State==ConnectionState.Open)
                    con.Close();
                con.Open();
                cmd= new MySqlCommand("Insert into fichier (num,nom,file,size,idlink) values(@num,@nom,@file,@size,@id)", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=idhistorique;
                cmd.Parameters.Add("@num", MySqlDbType.Int32).Value=num;
                cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value=nom;
                cmd.Parameters.Add("@file", MySqlDbType.LongBlob).Value=data;
                cmd.Parameters.Add("@size", MySqlDbType.UInt32).Value=size;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erreur lors de l'enregistrement");
                return false;
            }
        }
        public string open(string id)
        {
            try
            {
                con.Open();
                cmd= new MySqlCommand(
                    "Select size,file from files where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;

                UInt32 FileSize;
                byte[] rawData;
                FileStream fs;
                MySqlDataReader myData = cmd.ExecuteReader();

                if (!myData.HasRows)
                    throw new Exception("There are no BLOBs to save");
                myData.Read();
                FileSize = myData.GetUInt32(myData.GetOrdinal("size"));
                rawData = new byte[FileSize];
                myData.GetBytes(myData.GetOrdinal("file"), 0, rawData, 0, (int)FileSize);

                string filepath = path + @"DB-"+DateTime.Now.ToString("dd-MM-yyyy HH-mm")+".docx";
                fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(rawData, 0, (int)FileSize);
                fs.Close();
                con.Close();
                return filepath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'enregistrement");
                return null;
            }
        }
        public DataTable refreshExport()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter(
                "select id,type,date from files where deleted=0", con);
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public int getLast(string nom)
        {
            try
            {
                con.Open();
                string type = "";

                if (nom.Contains("Export"))
                    type="Export";
                else if (nom.Contains("Recu"))
                    type="Recu";
                else if (nom.Contains("Attestation"))
                    type="attestation";
                else if (nom.Contains("Fiche"))
                    type="Fiche";

                cmd=new MySqlCommand(
                        "select num from fichier order by id Desc limit 1 " +
                        "where nom Like %@type%", con);
                cmd.Parameters.Add("@type", MySqlDbType.VarChar).Value=type;
                var reader = cmd.ExecuteReader();
                int r = 0;

                if (reader.Read())
                {
                    r=reader.GetInt32(0);
                }
                con.Close();
                return r;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
