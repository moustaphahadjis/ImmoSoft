using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmoSoft
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //Application.Run(new Form1());
            //Application.Run(new historique());
            //Application.Run(new addVersement("1"));
            if (AnotherInstanceExists())
            {
                MessageBox.Show("Application en cours d'execution.",
                    "L'application est déjà lancée, Veuillez la fermer puis relancez",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Form objMyForm = new Loading();
            objMyForm.ShowDialog();
        }

        public static bool AnotherInstanceExists()

        {
            Process currentRunningProcess = Process.GetCurrentProcess();
            Process[] listOfProcs = Process.GetProcessesByName(currentRunningProcess.ProcessName);
            foreach (Process proc in listOfProcs)
            {
                if ((proc.MainModule.FileName == currentRunningProcess.MainModule.FileName) && (proc.Id != currentRunningProcess.Id))
                    return true;
            }
            return false;
        }
    }
}