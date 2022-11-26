using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace ImmoSoft.DB
{
    internal class ayantdroit
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public ayantdroit()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
        }
        public DataTable refresh(string id)
        {
            try
            {
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(
                    "select * from ayantdroit where id=@id and deleted=0", con);
                da.SelectCommand.Parameters.Add("@id", MySqlDbType.Int32).Value=id;

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
        public bool insert(string idclient,string idattribution,string idchamps, string idstock)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("insert into ayantdroit (idclient,idattribution,idchamps,idstock)" +
                    " Values(@idclient, @idattribution, @idchamps, @idstock)", con);
                cmd.Parameters.Add("@idclient", MySqlDbType.Int32).Value=idclient;
                cmd.Parameters.Add("@idattribution", MySqlDbType.Int32).Value=idattribution;
                cmd.Parameters.Add("@idchamps", MySqlDbType.Int32).Value=idchamps;
                cmd.Parameters.Add("@idstock", MySqlDbType.Int32).Value=idstock;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur server");
                return false;
            }
        }
        public bool update(string id, string idclient, string idattribution, string idchamps, string idstock)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("update attribution set idclient=@idclient," +
                    "idattribution=@idattribution,idchamps=@idchamps,idstock=@idstock where id=@id", con);
                cmd.Parameters.Add("@idclient", MySqlDbType.Int32).Value=idclient;
                cmd.Parameters.Add("@idattribution", MySqlDbType.Int32).Value=idattribution;
                cmd.Parameters.Add("@idchamps", MySqlDbType.Int32).Value=idchamps;
                cmd.Parameters.Add("@idstock", MySqlDbType.Int32).Value=idstock;
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur server");
                return false;
            }
        }
    }
}
