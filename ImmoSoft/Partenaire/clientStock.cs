using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmoSoft.Partenaire
{
    public partial class clientStock : Form
    {
        string clientID, type;
        public clientStock(string id, string type)
        {
            InitializeComponent();
            clientID = id;
            this.type = type;
        }
        void refresh()
        {
            
            DB.stock stock = new DB.stock();

            if (type=="vendue")
                dgv1.DataSource=stock.refreshClientVendue( clientID);
            else
                dgv1.DataSource=stock.refreshClientEnVente("En cours de vente", clientID);
        }
        private void clientStock_Load(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
