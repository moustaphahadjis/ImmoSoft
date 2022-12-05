﻿using ImmoSoft.DB;
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
            /*
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\ImmoSoft\saabtenga.xlsx";
            string path = @"C:\Users\moust\Documents\ImmoSoft\sanou2.xlsx";
            Excel.Application app = new Excel.Application();
            Excel.Workbook book = app.Workbooks.Open(path);
            Excel.Worksheet sheet = (Excel.Worksheet)book.ActiveSheet;
            con.Open();
            for (int i = 2; i<225; i++)
            {
                string lot = sheet.Cells[i, "B"].Value2.ToString().Trim();
                string parcelle = sheet.Cells[i, "C"].Value2.ToString().Trim();
                string superficie;
                string tmp = sheet.Cells[i, "D"].Value2.ToString().Trim();
                if (tmp.Contains("m"))
                    superficie = tmp.Split('m')[0];
                else
                    superficie = tmp.Split('㎡')[0];
               // MessageBox.Show(superficie);
                try
                {
                    cmd = new MySqlCommand("insert into stock (siteid,lot, parcelle,superficie, etat) " +
                        "Values (@siteid,@lot,@prcle,@spf,@etat)", con);

                    cmd.Parameters.Add("@siteid", MySqlDbType.Int32).Value = 2;
                    cmd.Parameters.Add("@lot", MySqlDbType.Int32).Value = lot;
                    cmd.Parameters.Add("@prcle", MySqlDbType.VarChar).Value = parcelle;
                    cmd.Parameters.Add("@spf", MySqlDbType.Decimal).Value = superficie;
                    cmd.Parameters.Add("@etat", MySqlDbType.VarChar).Value = "Disponible";
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    con.Close();
                    app.Quit();
                }
            }
            con.Close();
            app.Quit();
        }
            */

            con.Open();
            cmd= new MySqlCommand("update stock set siteid=3 where lot=2 and siteid=1", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        
    }
}