using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmoSoft.DB
{

    internal class historique
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public historique()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
        }
        public DataTable refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select * from historique where deleted=0", con);
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public DataTable getVersement(string idstock)
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter(
                "select action,montant,reste, date from historique where deleted=0 and idstock=@id", con);
            da.SelectCommand.Parameters.Add("@id", MySqlDbType.Int32).Value=idstock;
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public DataTable refresh(string from, string to,string id)
        {
            con.Open();
            string com = "select historique.id,stock.lot,stock.parcelle,stock.superficie," +
                " client.nom, client.prenom, client.contact," +
                " historique.action," +
                " stock.prix, stock.montant, stock.reste," +
                " historique.date from historique" +
                " join stock on stock.id = historique.id" +
                " join client on client.id = historique.idclient" +
                " where historique.deleted=0 and historique.date between @from and @to";
            if (id!=null)
                com+=" and stock.id='"+id+"'";

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
        public bool add(string action, string idstock, string idclient,
            string iddemarcheur, string prix, string montant, string reste, string usage)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("insert into historique (idstock, action, prix, montant,reste, type_usage," +
                    "idclient, iddemarcheur) Values (@idstock,@action,@prix,@montant,@reste,@usage, @idclient,@iddemarcheur)", con);
                cmd.Parameters.Add("@idstock", MySqlDbType.Int32).Value = idstock;
                cmd.Parameters.Add("@action", MySqlDbType.VarChar).Value = action;
                cmd.Parameters.Add("@prix", MySqlDbType.Decimal).Value = prix;
                cmd.Parameters.Add("@montant", MySqlDbType.Decimal).Value = montant;
                cmd.Parameters.Add("@reste", MySqlDbType.Decimal).Value = reste;
                cmd.Parameters.Add("@usage", MySqlDbType.VarChar).Value = usage;
                cmd.Parameters.Add("@idclient", MySqlDbType.Int32).Value = idclient;
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
        public bool annulerVente(string action, string idstock, string idclient,
            string iddemarcheur, string prix, string montant, string reste, string usage)
        {
            try
            {
                con.Open();
                cmd=new MySqlCommand("update historique set deleted='1' where idstock=@idstock and idclient=@idclient", con);
                cmd.Parameters.Add("@idstock", MySqlDbType.Int32).Value = idstock;
                cmd.Parameters.Add("@idclient", MySqlDbType.Int32).Value = idclient;
                cmd.ExecuteNonQuery();


                cmd = new MySqlCommand("insert into historique (idstock, action, prix, montant,reste, type_usage," +
                    "idclient, iddemarcheur) Values (@idstock,@action,@prix,@montant,@reste,@usage, @idclient,@iddemarcheur)", con);
                cmd.Parameters.Add("@idstock", MySqlDbType.Int32).Value = idstock;
                cmd.Parameters.Add("@action", MySqlDbType.VarChar).Value = action;
                cmd.Parameters.Add("@prix", MySqlDbType.Decimal).Value = prix;
                cmd.Parameters.Add("@montant", MySqlDbType.Decimal).Value = montant;
                cmd.Parameters.Add("@reste", MySqlDbType.Decimal).Value = reste;
                cmd.Parameters.Add("@usage", MySqlDbType.VarChar).Value = usage;
                cmd.Parameters.Add("@idclient", MySqlDbType.Int32).Value = idclient;
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
                cmd=new MySqlCommand("select id from historique order by id Desc limit 1", con);
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
