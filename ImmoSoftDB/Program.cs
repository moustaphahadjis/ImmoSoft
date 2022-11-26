using System;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace ImmoSoftDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\ImmoSoft\saabtenga.xlsx";
            string path = @"C:\Users\moust\Documents\ImmoSoft\saabtenga.xlsx";
            Excel.Application app = new Excel.Application();
            Excel.Workbook book = app.Workbooks.Open(path);
            Excel.Worksheet sheet =(Excel.Worksheet) book.ActiveSheet;
            for(int i =0; i<179;i++)
            {
                Console.WriteLine(sheet.Cells[1,"D"].ToString());
            }
        }
    }
}
