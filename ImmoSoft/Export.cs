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
    public partial class Export : Form
    {
        public Export()
        {
            InitializeComponent();
        }
        void refresh()
        {
            DB.stock stock = new DB.stock();
            dgv1.DataSource = stock.refreshStock("Disponible", "0");
            dgv1.Columns[4].HeaderText="Client nom";
            dgv1.Columns[5].HeaderText="Client prenom";
            dgv1.Columns[6].HeaderText="Demarcheur nom";
            dgv1.Columns[7].HeaderText="Demarcheur prenom";
        }
        private void Export_Load(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
