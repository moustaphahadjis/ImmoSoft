using ImmoSoft.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmoSoft
{
    public partial class addStock : Form
    {
        DataTable dtsite;
        string siteid, stockid;
        public addStock()
        {
            InitializeComponent();
        }
        public addStock(string stockID)
        {
            InitializeComponent();
            stockid = stockID;
            DB.stock st = new stock();
            DataRow row = st.refresh(stockid).Rows[0];

            // site.Text = row["site"].ToString();
            lot.Text=row["lot"].ToString();
            section.Text=row["section"].ToString();
            prcle.Text=row["parcelle"].ToString();
            spf.Text=row["superficie"].ToString();
        }
        private void addStock_Load(object sender, EventArgs e)
        {
            refresh();
        }
        void refresh()
        {
            DB.site site = new DB.site();
            dtsite=site.refresh();
            foreach(DataRow row in dtsite.Rows)
            {
                this.combobox.Items.Add(row["nom"].ToString());
                if (siteid!=null)
                    if (row["id"].ToString()==siteid)
                        combobox.Text = row["nom"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                common com = new common();
                if (!String.IsNullOrEmpty(lot.Text) &&
                    !String.IsNullOrEmpty(combobox.Text) &&
                    !String.IsNullOrEmpty(prcle.Text) &&
                    !String.IsNullOrEmpty(spf.Text))
                    if (com.isNumber(lot.Text.Trim())
                            && com.isNumber(spf.Text.Trim()))
                    {
                        DB.site ste = new site();
                        DB.stock stock = new stock();

                        if (stockid==null)
                            stock.add(siteid, section.Text, lot.Text, prcle.Text, spf.Text, "Disponible");
                        else
                            stock.update(stockid, siteid, section.Text, lot.Text, prcle.Text, spf.Text);
                        this.Close();
                    }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void site_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtsite.Rows.Count>0)
                foreach (DataRow row in dtsite.Rows)
                {
                    if (row["nom"].ToString()==combobox.Text)
                    {
                        siteid=row["id"].ToString();
                        break;
                    }
                }
        }
    }
}
