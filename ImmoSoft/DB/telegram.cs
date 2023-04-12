using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmoSoft.DB
{
    internal class telegram
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public telegram()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
        }
        public DataTable refresh()
        {
            try
            {
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(
                    "select * from telegram ", con);

                DataTable ds = new DataTable();
                ds.BeginLoadData();
                da.Fill(ds);
                ds.EndLoadData();
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur server");
                return null;
            }
        }
    }
}
