using ImmoSoft.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

namespace ImmoSoft.DB
{
    internal class printer
    {
        MySqlConnection con;
        MySqlCommand cmd;
        string path;
        TelegramBotClient Bot;
        public printer()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\ImmoSoft\";


            //Bot Initialize
            Bot = new TelegramBotClient("6209952748:AAEgosxF3P43KRM4OESUTvbtHVb6Fp9SoBg");
            //IDList = new long[] { 1748952224  };
            //1412752592, 1317053901, 
        }
        public async void BotSend(string path, string filename)
        {
            try
            {
                string caption = "Utilisateur: "+Properties.Settings.Default.nom+" "+Properties.Settings.Default.prenom;
                DB.telegram tg = new DB.telegram();
                var dt = tg.refresh();
                foreach (DataRow row in dt.Rows)
                {
                    long id = long.Parse(row["address"].ToString());
                    Telegram.Bot.Types.Message message = await Bot.SendDocumentAsync(
                        chatId: id,
                        document: new InputOnlineFile(content: new FileStream(path, FileMode.Open, FileAccess.Read)
                        , fileName: filename),
                        caption: caption);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
                int last = 0, pos=2;
                for(int i=0; i < data.Rows.Count; i++)
                {
                    DataGridViewRow row = data.Rows[i];
                    doc.Tables[1].Rows.Add(doc.Tables[1].Rows[pos]);

                    if (lot!=row.Cells["lot"].Value.ToString())
                    {
                        doc.Tables[1].Rows.Add(doc.Tables[1].Rows[pos]);
                        pos++;
                        lot=row.Cells["lot"].Value.ToString();
                        doc.Tables[1].Rows[pos].Cells[2].Range.Text = lot;
                    }
                    doc.Tables[1].Rows[pos].Cells[1].Range.Text = (i+1).ToString();
                    doc.Tables[1].Rows[pos].Cells[3].Range.Text = row.Cells["Parcelle"].Value.ToString();
                    doc.Tables[1].Rows[pos].Cells[4].Range.Text = row.Cells["Superficie"].Value.ToString()+"㎡";
                    if (data.Columns.Contains("Client"))
                    doc.Tables[1].Rows[pos].Cells[5].Range.Text = row.Cells["Client"].Value.ToString();
                    if(data.Columns.Contains("Demarcheur"))
                    doc.Tables[1].Rows[pos].Cells[6].Range.Text = row.Cells["Demarcheur"].Value.ToString();
                    if (data.Columns.Contains("Montant"))
                        doc.Tables[1].Rows[pos].Cells[7].Range.Text = row.Cells["Montant"].Value.ToString();
                    if (data.Columns.Contains("Reste"))
                        doc.Tables[1].Rows[pos].Cells[8].Range.Text = row.Cells["Reste"].Value.ToString();
                    last=pos;
                    pos++;
                }
                doc.Tables[1].Rows[2].Delete();
                doc.SaveAs2(filepath);
                Object oMissing = System.Reflection.Missing.Value;
                doc.ExportAsFixedFormat(filepath.Replace("docx","pdf"), Word.WdExportFormat.wdExportFormatPDF, false,
                    Word.WdExportOptimizeFor.wdExportOptimizeForPrint,
                    Word.WdExportRange.wdExportAllDocument, 1, 1, Word.WdExportItem.wdExportDocumentContent, true, true,
                    Word.WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, ref oMissing);
                doc.Close();
                app.Quit();

                save("export",filenum,filename.Replace("docx", "pdf"),
                    filepath.Replace("docx", "pdf"),"0");

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
        public bool versement(string objet, string nom, string identity, string contact,
            string lot, string parcelle, string superficie,
            string motif, string montant,
            string prix, string total, string reste, string idstock)
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
                app.Selection.Find.Execute(FindText: "@objet", ReplaceWith: objet, Replace: Word.WdReplace.wdReplaceAll);
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
                    Word.WdExportOptimizeFor.wdExportOptimizeForPrint,
                    Word.WdExportRange.wdExportAllDocument, 1, 1, Word.WdExportItem.wdExportDocumentContent, true, true,
                    Word.WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, ref oMissing);
                doc.Close();
                app.Quit();

                save("recu",filenum, filename.Replace("docx", "pdf"), 
                    filepath.Replace("docx", "pdf"), idstock);

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


        public bool attestation(string nom,string prenom, string matrimonial,
            string identity,string adresse, string profession, string contact, 
            string ville, string site, string section,string lot, string parcelle,
            string usage, string superficie, string idstock)
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
                app.Selection.Find.Execute(FindText: "@prenom", ReplaceWith: prenom, Replace: Word.WdReplace.wdReplaceAll);
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
                app.Selection.Find.Execute(FindText: "@date", ReplaceWith: DateTime.Now.ToString("dd-MM-yyyy"), Replace: Word.WdReplace.wdReplaceAll);


                doc.SaveAs2(filepath);

                Object oMissing = System.Reflection.Missing.Value;
                doc.ExportAsFixedFormat(filepath.Replace("docx", "pdf"), Word.WdExportFormat.wdExportFormatPDF, false,
                    Word.WdExportOptimizeFor.wdExportOptimizeForPrint,
                    Word.WdExportRange.wdExportAllDocument, 1, 1, Word.WdExportItem.wdExportDocumentContent, true, true,
                    Word.WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, ref oMissing);
                doc.Close();
                app.Quit();

                save("attestation",filenum, filename.Replace("docx", "pdf"),
                    filepath.Replace("docx", "pdf"), idstock);
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

        public bool fiche(string nom, string prenom, string matrimonial,
            string identity, string adresse, string profession, string contact,
            string ville, string site, string section, string lot, string parcelle,
            string usage, string superficie, string idstock)
        {
            Word.Application app = new Word.Application();
            convertir con = new convertir();
            try
            {
                app.ShowAnimation = false;
                app.Visible=false;
                string filenum = (getLast("Fiche")+1).ToString();
                string filename = @"Fiche n"+filenum+".docx";
                string filepath = path + filename;
                Word.Document doc = app.Documents.Open(path+@"Fiche.docx");

                app.Selection.Find.Execute(FindText: "@num", ReplaceWith: filenum, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@nom", ReplaceWith: nom, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@prenom", ReplaceWith: prenom, Replace: Word.WdReplace.wdReplaceAll);
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
                app.Selection.Find.Execute(FindText: "@date", ReplaceWith: DateTime.Now.ToString("dd-MM-yyyy"), Replace: Word.WdReplace.wdReplaceAll);


                doc.SaveAs2(filepath);

                Object oMissing = System.Reflection.Missing.Value;
                doc.ExportAsFixedFormat(filepath.Replace("docx", "pdf"), Word.WdExportFormat.wdExportFormatPDF, false,
                    Word.WdExportOptimizeFor.wdExportOptimizeForPrint,
                    Word.WdExportRange.wdExportAllDocument, 1, 1, Word.WdExportItem.wdExportDocumentContent, true, true,
                    Word.WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, ref oMissing);
                doc.Close();
                app.Quit();

                save("fiche", filenum, filename.Replace("docx", "pdf"),
                    filepath.Replace("docx", "pdf"), idstock);

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
        public bool historique(DataGridView data)
        {
            Word.Application app = new Word.Application();
            try
            {
                app.ShowAnimation = false;
                app.Visible=false;

                string filenum = (getLast("Historique")+1).ToString();
                string filename = @"Historique n"+filenum+".docx";
                string filepath = path + filename;
                Word.Document doc = app.Documents.Open(path+@"Historique.docx");

                app.Selection.Find.Execute(FindText: "@date", ReplaceWith: DateTime.Now.ToString("dd-MM-yyyy"), Replace: Word.WdReplace.wdReplaceAll);

                
                int last = 0, pos = 2;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    DataGridViewRow row = data.Rows[i];
                    doc.Tables[1].Rows.Add(doc.Tables[1].Rows[pos]);

                    doc.Tables[1].Rows[pos].Cells[1].Range.Text = row.Cells["site"].Value.ToString();
                    doc.Tables[1].Rows[pos].Cells[2].Range.Text = row.Cells["lot"].Value.ToString()+
                        " - "+row.Cells["Parcelle"].Value.ToString()+
                        " - "+row.Cells["superficie"].Value.ToString();
                    doc.Tables[1].Rows[pos].Cells[3].Range.Text = row.Cells["Client"].Value.ToString();
                    doc.Tables[1].Rows[pos].Cells[4].Range.Text = row.Cells["contact"].Value.ToString();
                    doc.Tables[1].Rows[pos].Cells[5].Range.Text = row.Cells["Demarcheur"].Value.ToString();
                    doc.Tables[1].Rows[pos].Cells[6].Range.Text = row.Cells["action"].Value.ToString();
                    doc.Tables[1].Rows[pos].Cells[7].Range.Text = row.Cells["Montant"].Value.ToString();
                    doc.Tables[1].Rows[pos].Cells[8].Range.Text = row.Cells["Reste"].Value.ToString();
                    doc.Tables[1].Rows[pos].Cells[9].Range.Text = row.Cells["commission"].Value.ToString();
                    doc.Tables[1].Rows[pos].Cells[10].Range.Text = row.Cells["comreste"].Value.ToString();
                    doc.Tables[1].Rows[pos].Cells[11].Range.Text = row.Cells["date"].Value.ToString();
                    doc.Tables[1].Rows[pos].Cells[12].Range.Text = row.Cells["utilisateur"].Value.ToString();
                    last=pos;
                    pos++;
                }
                doc.Tables[1].Rows[pos].Delete();
                doc.SaveAs2(filepath);
                Object oMissing = System.Reflection.Missing.Value;
                doc.ExportAsFixedFormat(filepath.Replace("docx", "pdf"), Word.WdExportFormat.wdExportFormatPDF, false,
                    Word.WdExportOptimizeFor.wdExportOptimizeForPrint,
                    Word.WdExportRange.wdExportAllDocument, 1, 1, Word.WdExportItem.wdExportDocumentContent, true, true,
                    Word.WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, ref oMissing);
                doc.Close();
                app.Quit();

                save("historique", filenum, filename.Replace("docx", "pdf"),
                    filepath.Replace("docx", "pdf"), "0");

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
        public bool decharge(string nom, string prenom, 
            string identity, string contact,
            string site, string lot, string parcelle,
            string superficie, string idstock, string montant)
        {
            Word.Application app = new Word.Application();
            convertir con = new convertir();
            try
            {
                app.ShowAnimation = false;
                app.Visible=false;
                string filenum = (getLast("Decharge")+1).ToString();
                string filename = @"Decharge n"+filenum+".docx";
                string filepath = path + filename;
                Word.Document doc = app.Documents.Open(path+@"Decharge.docx");

                app.Selection.Find.Execute(FindText: "@num", ReplaceWith: filenum, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@nom", ReplaceWith: nom, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@prenom", ReplaceWith: prenom, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@motif", ReplaceWith: "vente", Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@identity", ReplaceWith: identity, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@contact", ReplaceWith: contact, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@site", ReplaceWith: site, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@lot", ReplaceWith: lot, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@parcelle", ReplaceWith: parcelle, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@superficie", ReplaceWith: superficie, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@montant", ReplaceWith: montant, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@lettre", ReplaceWith: con.enLettre(montant), Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@date", ReplaceWith: DateTime.Now.ToString("dd-MM-yyyy"), Replace: Word.WdReplace.wdReplaceAll);


                doc.SaveAs2(filepath);

                Object oMissing = System.Reflection.Missing.Value;
                doc.ExportAsFixedFormat(filepath.Replace("docx", "pdf"), Word.WdExportFormat.wdExportFormatPDF, false,
                    Word.WdExportOptimizeFor.wdExportOptimizeForPrint,
                    Word.WdExportRange.wdExportAllDocument, 1, 1, Word.WdExportItem.wdExportDocumentContent, true, true,
                    Word.WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, ref oMissing);
                doc.Close();
                app.Quit();

                save("decharge", filenum, filename.Replace("docx", "pdf"),
                    filepath.Replace("docx", "pdf"), idstock);

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

        public bool acte(string nomclient, string identity, string site,
            string lot, string parcelle, string superficie, string prix,
            string montant, string reste, string idstock)
        {
            Word.Application app = new Word.Application();
            convertir con = new convertir();
            try
            {
                app.ShowAnimation = false;
                app.Visible=false;
                string filenum = (getLast("Acte")+1).ToString();
                string filename = @"Acte n"+filenum+".docx";
                string filepath = path + filename;
                Word.Document doc = app.Documents.Open(path+@"acte.docx");

                app.Selection.Find.Execute(FindText: "@num", ReplaceWith: filenum, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@nomclient", ReplaceWith: nomclient, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@identity", ReplaceWith: identity, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@site", ReplaceWith: site, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@lot", ReplaceWith: lot, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@parcelle", ReplaceWith: parcelle, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@superficie", ReplaceWith: superficie, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@montant", ReplaceWith: montant, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@prix", ReplaceWith: prix, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@reste", ReplaceWith: reste, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "@date", ReplaceWith: DateTime.Now.ToString("dd-MM-yyyy"), Replace: Word.WdReplace.wdReplaceAll);


                doc.SaveAs2(filepath);

                Object oMissing = System.Reflection.Missing.Value;
                doc.ExportAsFixedFormat(filepath.Replace("docx", "pdf"), Word.WdExportFormat.wdExportFormatPDF, false,
                    Word.WdExportOptimizeFor.wdExportOptimizeForPrint,
                    Word.WdExportRange.wdExportAllDocument, 1, 1, Word.WdExportItem.wdExportDocumentContent, true, true,
                    Word.WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, ref oMissing);
                doc.Close();
                app.Quit();

                save("acte", filenum, filename.Replace("docx", "pdf"),
                    filepath.Replace("docx", "pdf"), idstock);
                
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

        public bool save(string type,string num, string nom, string path, string idstock)
        {
            try
            {
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                UInt32 size = (UInt32)file.Length;
                byte[] data = new byte[size];

                file.Read(data, 0, data.Length);
                file.Close();

                if (con.State==ConnectionState.Open)
                    con.Close();
                con.Open();
                cmd= new MySqlCommand("Insert into fichier (type,num,nom,file,size,idstock) values(@type,@num,@nom,@file,@size,@id)", con);
                cmd.Parameters.Add("@type", MySqlDbType.VarChar).Value=type;
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=idstock;
                cmd.Parameters.Add("@num", MySqlDbType.Int32).Value=num;
                cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value=nom;
                cmd.Parameters.Add("@file", MySqlDbType.LongBlob).Value=data;
                cmd.Parameters.Add("@size", MySqlDbType.UInt32).Value=size;
                cmd.ExecuteNonQuery();
                con.Close();


                BotSend(path, nom);
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
                    "Select nom,size,file from fichier where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                string nom;
                UInt32 FileSize;
                byte[] rawData;
                FileStream fs;
                MySqlDataReader myData = cmd.ExecuteReader();

                if (!myData.HasRows)
                    throw new Exception("There are no BLOBs to save");
                myData.Read();
                nom=myData.GetString(myData.GetOrdinal("nom"));
                FileSize = myData.GetUInt32(myData.GetOrdinal("size"));
                rawData = new byte[FileSize];
                myData.GetBytes(myData.GetOrdinal("file"), 0, rawData, 0, (int)FileSize);
                
                string filepath = System.IO.Path.GetTempPath()+ DateTime.Now.ToString("HH-mm")+" "+nom;
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
        public DataTable refresh()
        {
            con.Open();
            MySqlDataAdapter da;
            da= new MySqlDataAdapter(
               "select fichier.id, fichier.nom,fichier.date," +
               "site.nom as Site, stock.lot, stock.parcelle, stock.superficie" +
               "  from fichier " +
               " join stock on stock.id = fichier.idstock " +
               " join site on site.id = stock.siteid" +
               " where fichier.deleted=0", con);
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public DataTable refresh(string type, string idstock, string idchamps
            )
        {
            con.Open();
            MySqlDataAdapter da;
                da= new MySqlDataAdapter(
                   "select id,nom,date from fichier " +
               "where idstock=@idstock and type=@type and deleted=0", con);
                da.SelectCommand.Parameters.Add("@idstock", MySqlDbType.VarChar).Value=idstock;


                da.SelectCommand.Parameters.Add("@type", MySqlDbType.VarChar).Value=type;
                DataTable ds = new DataTable();
                ds.BeginLoadData();
                da.Fill(ds);
                ds.EndLoadData();
            con.Close();
            return ds;
        }
        public void delete(string id)
        {
            try
            {
                con.Open();
                cmd= new MySqlCommand("update fichier set deleted=1 where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Erreur lors de la suppression");
            }

        }
        public int getLast(string type)
        {
            try
            {
                con.Open();

                cmd=new MySqlCommand(
                        "select num from fichier where type=@type order by id Desc limit 1 ", con);
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
