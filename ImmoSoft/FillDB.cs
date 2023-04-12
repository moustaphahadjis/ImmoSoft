using ImmoSoft.DB;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;

namespace ImmoSoft
{
    public partial class FillDB : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        public FillDB()
        {
            InitializeComponent();
            con = new MySqlConnection(
                "datasource=localhost;port=3306;username=root; password=;database=immosoft");
            con.Close();
        }

        private void FillDB_Load(object sender, EventArgs e)
        {
            
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\ImmoSoft\saabtenga.xlsx";
            
            string path = @"C:\Users\moust\Downloads\RESTE Kiri 26HA.xlsx";
            Excel.Application app = new Excel.Application();
            Excel.Workbook book = app.Workbooks.Open(path);
            Excel.Worksheet sheet = (Excel.Worksheet)book.ActiveSheet;
            con.Open();
            string lot=""; string parcelle = ""; string superficie="";
            for (int i = 1; i<200; i++)
            {
                if (sheet.Cells[i, "C"].Value2!=null)
                {
                    try
                    {
                        if (sheet.Cells[i, "B"].Value2!=null)
                            lot = sheet.Cells[i, "B"].Value2.ToString().Trim();
                        parcelle = sheet.Cells[i, "C"].Value2.ToString().Trim();
                        
                        string tmp = sheet.Cells[i, "D"].Value2.ToString().Trim();
                        if (tmp.Contains("m"))
                            superficie = tmp.Split('m')[0];
                        else
                            superficie = tmp.Split('㎡')[0];
                        // MessageBox.Show(superficie);

                        cmd = new MySqlCommand("insert into stock (siteid,lot, parcelle,superficie, etat) " +
                            "Values (@siteid,@lot,@prcle,@spf,@etat)", con);

                        cmd.Parameters.Add("@siteid", MySqlDbType.Int32).Value = 1;
                        cmd.Parameters.Add("@lot", MySqlDbType.Int32).Value = lot;
                        cmd.Parameters.Add("@prcle", MySqlDbType.VarChar).Value = parcelle;
                        cmd.Parameters.Add("@spf", MySqlDbType.Decimal).Value = superficie;
                        cmd.Parameters.Add("@etat", MySqlDbType.VarChar).Value = "Disponible";
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(lot +" "+parcelle+" "+superficie);
                        con.Close();
                        app.Quit();
                    }
                }
            }
            con.Close();
            app.Quit();
        }
            
        }
            
        
    }

