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
    public partial class commission : Form
    {
        string iddemarcheur = "";
        public commission(string id, string option)
        {
            InitializeComponent();
            iddemarcheur = id;

            DB.demarcheur dem = new DB.demarcheur();
            dgvD.DataSource = dem.refresh(id);

            if(option=="stock")
            {
                DB.stock stock= new DB.stock();
                dgv1.DataSource = stock.refreshCommission(id);
            }
            else
            {
                /*
                DB.champs champs = new DB.champs();
                dgv1.DataSource = champs.refreshCommission(id);
                */
            }

        }

        private void commission_Load(object sender, EventArgs e)
        {

        }
    }
}
