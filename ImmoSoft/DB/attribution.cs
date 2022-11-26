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
    internal class attribution
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public attribution()
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
                    "select * from attribution where id=@id and deleted=0", con);
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
        public DataTable refresh(string from, string to, string siteid)
        {
            try
            {
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(
                    "select attribution.id, attribution.idnew, attribution.idold," +
                    " attribution.idchamps, attribution.idstock," +
                    " stock.lot, stock.parcelle, stock.superficie," +
                    " CONCAT (client.nom,' ', client.prenom) as Client" +
                    " , attribution.date from attribution" +
                    " join stock on stock.id = attribution.idstock" +
                    " join client on client.id = attribution.idnew" +
                    " where attribution.idchamps LIKE '%0%' and " +
                    "attribution.deleted=0 and attribution.date between @from and @to" +
                    " union " +
                    "select attribution.id, attribution.idnew, attribution.idold," +
                    " attribution.idchamps, attribution.idstock," +
                    " champs.lot, champs.parcelle, champs.superficie," +
                    " CONCAT (client.nom, client.prenom) as Client" +
                    " , attribution.date from attribution" +
                    " join champs on champs.id = attribution.idchamps" +
                    " join client on client.id = attribution.idnew" +
                    " where attribution.idstock LIKE '%0%' and " +
                    "attribution.deleted=0 and attribution.date between @from and @to"
                    , con);
                da.SelectCommand.Parameters.Add("@from", MySqlDbType.DateTime).Value=from;
                da.SelectCommand.Parameters.Add("@to", MySqlDbType.DateTime).Value=to;
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
        public bool insert(string idchamps, string idstock, string idold, string idnew, bool attribue)
        {
            try
            {
                con.Open();
                int att = 0;
                if(attribue)
                    att= 1;
                cmd = new MySqlCommand("insert into attribution (idchamps,idstock,idold,idnew,attribue)" +
                    " Values(@idchamps, @idstock, @idold, @idnew,@attribue)", con);
                cmd.Parameters.Add("@idchamps", MySqlDbType.Int32).Value=idchamps;
                cmd.Parameters.Add("@idstock", MySqlDbType.Int32).Value=idstock;
                cmd.Parameters.Add("@idold", MySqlDbType.Int32).Value=idold;
                cmd.Parameters.Add("@idnew", MySqlDbType.Int32).Value=idnew;
                cmd.Parameters.Add("@attribue", MySqlDbType.Int32).Value=att;
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
