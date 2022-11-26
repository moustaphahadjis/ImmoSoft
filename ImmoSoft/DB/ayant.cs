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
    internal class ayant
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public ayant()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
        }
        public DataTable refresh(string attribution, string idayant,
            string idclient,string idstock, string idchamps)
        {
            try
            {
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(
                    "select * from ayant where id=@id and deleted=0", con);
                //da.SelectCommand.Parameters.Add("@id", MySqlDbType.Int32).Value=id;

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

        public bool insert(string attribution, string idayant,
            string idclient, string idstock, string idchamps)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("insert into ayantdroit (attribution,idayant,idclient,idstock,idchamps)" +
                    " Values(@attribution, @idayant, @idclient, @idstock,@idchamps)", con);
                cmd.Parameters.Add("@attribution", MySqlDbType.Int32).Value=attribution;
                cmd.Parameters.Add("@idayant", MySqlDbType.Int32).Value=idayant;
                cmd.Parameters.Add("@idclient", MySqlDbType.Int32).Value=idclient;
                cmd.Parameters.Add("@idstock", MySqlDbType.Int32).Value=idstock;
                cmd.Parameters.Add("@idchamps", MySqlDbType.Int32).Value=idchamps;
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
        public bool update(string id, string idchamps, string idstock, string idold, string idnew, bool attribue)
        {
            try
            {
                con.Open();
                int att = 0;
                if (attribue)
                    att= 1;
                cmd = new MySqlCommand("update attribution set idchamps=@idchamps," +
                    "idstock=@idstock,idold=@idold,idnew=@idnew, attribue=@attribue where id=@id", con);
                cmd.Parameters.Add("@idchamps", MySqlDbType.Int32).Value=idchamps;
                cmd.Parameters.Add("@idstock", MySqlDbType.Int32).Value=idstock;
                cmd.Parameters.Add("@idold", MySqlDbType.Int32).Value=idold;
                cmd.Parameters.Add("@idnew", MySqlDbType.Int32).Value=idnew;
                cmd.Parameters.Add("@attribue", MySqlDbType.VarChar).Value=att;
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
