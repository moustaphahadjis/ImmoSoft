using ImmoSoft.DB;
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
    public partial class addSite : Form
    {
        DataTable dtsite;
        string siteid;
        public addSite()
        {
            InitializeComponent();
        }
        public addSite(string siteID)
        {
            InitializeComponent();
            siteid = siteID;
            DB.site st = new site();
            DataRow row = st.refresh(siteid).Rows[0];

            // site.Text = row["site"].ToString();
            nom.Text=row["nom"].ToString();
            ville.Text=row["ville"].ToString();
            taille.Text=row["taille"].ToString();
        }

        private void addSite_Load(object sender, EventArgs e)
        {

        }
    }
}
