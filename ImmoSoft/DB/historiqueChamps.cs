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
    internal class historiqueChampsnlk
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public historiqueChampsnlk()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
        }
        public DataTable refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select * from historiqueChamps where deleted=0", con);
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public DataTable getVersement(string idchamps)
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter(
                "select action,montant,reste,date from historiqueChamps where deleted=0 and idchamps=@id", con);
            da.SelectCommand.Parameters.Add("@id", MySqlDbType.Int32).Value=idchamps;
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public DataTable refresh(string from, string to, string id)
        {
            con.Open();
            string com = "select historiqueChamps.id,champs.lot,champs.parcelle,champs.superficie," +
                " client.nom, client.prenom, client.contact," +
                " historiqueChamps.action," +
                " stock.prix, stock.montant, stock.reste," +
                " historiqueChamps.date from historiqueChamps" +
                " join champs on champs.id = historiqueChamps.id" +
                " join client on client.id = historiqueChamps.idclient" +
                " where historiqueChamps.deleted=0 and historiqueChamps.date between @from and @to";
            if (id!=null)
                com+=" and id='"+id+"'";

            MySqlDataAdapter da = new MySqlDataAdapter(com, con);
            da.SelectCommand.Parameters.Add("@from", MySqlDbType.DateTime).Value=from;
            da.SelectCommand.Parameters.Add("@to", MySqlDbType.DateTime).Value=to;
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public bool add(string action, string idchamps, string idold, string idnew,
            string iddemarcheur, string prix, string montant, string reste, string usage)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("insert into historiqueChamps (idchamps,idold,idnew, action, prix, montant,reste, type_usage," +
                    " iddemarcheur) Values (@idchamps,@idold,@idnew,@action,@prix,@montant,@reste,@usage,@iddemarcheur)", con);
                cmd.Parameters.Add("@idchamps", MySqlDbType.Int32).Value = idchamps;
                cmd.Parameters.Add("@action", MySqlDbType.VarChar).Value = action;
                cmd.Parameters.Add("@prix", MySqlDbType.Decimal).Value = prix;
                cmd.Parameters.Add("@montant", MySqlDbType.Decimal).Value = montant;
                cmd.Parameters.Add("@reste", MySqlDbType.Decimal).Value = reste;
                cmd.Parameters.Add("@usage", MySqlDbType.VarChar).Value = usage;
                cmd.Parameters.Add("@idold", MySqlDbType.Int32).Value = idold;
                cmd.Parameters.Add("@idnew", MySqlDbType.Int32).Value = idnew;
                cmd.Parameters.Add("@iddemarcheur", MySqlDbType.Int32).Value = iddemarcheur;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }
        public bool annulerVente(string action, string idstock, string idnew,
            string iddemarcheur, string prix, string montant, string reste, string usage)
        {
            try
            {
                con.Open();
                cmd=new MySqlCommand("update historique set deleted='1' where idstock=@idstock and idnew=@idnew", con);
                cmd.Parameters.Add("@idstock", MySqlDbType.Int32).Value = idstock;
                cmd.Parameters.Add("@idnew", MySqlDbType.Int32).Value = idnew;
                cmd.ExecuteNonQuery();


                cmd = new MySqlCommand("insert into historique (idstock, action, prix, montant,reste, type_usage," +
                    "idnew, iddemarcheur) Values (@idstock,@action,@prix,@montant,@reste,@usage, @idnew,@iddemarcheur)", con);
                cmd.Parameters.Add("@idstock", MySqlDbType.Int32).Value = idstock;
                cmd.Parameters.Add("@action", MySqlDbType.VarChar).Value = action;
                cmd.Parameters.Add("@prix", MySqlDbType.Decimal).Value = prix;
                cmd.Parameters.Add("@montant", MySqlDbType.Decimal).Value = montant;
                cmd.Parameters.Add("@reste", MySqlDbType.Decimal).Value = reste;
                cmd.Parameters.Add("@usage", MySqlDbType.VarChar).Value = usage;
                cmd.Parameters.Add("@idnew", MySqlDbType.Int32).Value = idnew;
                cmd.Parameters.Add("@iddemarcheur", MySqlDbType.Int32).Value = iddemarcheur;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }
        public int getLast()
        {
            try
            {
                con.Open();
                cmd=new MySqlCommand("select id from historiqueChamps order by id Desc limit 1", con);
                var reader = cmd.ExecuteReader();
                int r = 0;
                if (reader.Read())
                {
                    r=reader.GetInt32(0);
                }
                con.Close();
                return r;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
