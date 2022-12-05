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
        bool update=false;
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
            update=true;
        }

        private void addSite_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            common com = new common();
            if (!string.IsNullOrEmpty(nom.Text)
                && !string.IsNullOrEmpty(taille.Text))
                if (com.isNumber(taille.Text))
                {
                    if (!update)
                    {
                        DB.site site = new site();
                        site.add(nom.Text, ville.Text, taille.Text);
                    }
                    else
                    {
                        DB.site site = new site();
                        site.update(siteid, nom.Text, ville.Text, taille.Text);
                    }
                    this.Close();
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
