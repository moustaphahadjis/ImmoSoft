using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Security.Policy;

namespace ImmoSoft.DB
{
    internal class stock
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public stock()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
        }
        public DataTable refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter(
                "select * from stock where deleted=0", con);
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public DataTable refresh(string id)
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter(
                "select * from stock where id=@id and deleted=0", con);
            da.SelectCommand.Parameters.Add("@id",MySqlDbType.Int32).Value=id;

            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public DataTable refreshStock(string etat, string siteid)
        {
            con.Open();
            MySqlDataAdapter da;
            string aa = "select stock.id,stock.section,stock.lot,stock.parcelle,stock.superficie," +
                "CONCAT (client.nom,' ', client.prenom) as Client," +
                " CONCAT (demarcheur.nom,' ',demarcheur.prenom) as Demarcheur" +
                ", idclient, iddemarcheur" +
                " from stock join demarcheur on demarcheur.id = stock.iddemarcheur" +
                " join client on client.id=stock.idclient"+
                " where stock.etat=@etat and stock.deleted=0";
            if (siteid!="0")
            {
                aa+= " and stock.siteid=@siteid";
            }
            da = new MySqlDataAdapter(aa , con);
            da.SelectCommand.Parameters.Add("@etat", MySqlDbType.VarChar).Value=etat;
            if (siteid!="0")
            {
                da.SelectCommand.Parameters.Add("@siteid", MySqlDbType.Int32).Value=siteid;
            }
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public DataTable refreshVendue(string etat, string siteid)
        {
            con.Open();
            string aa =
                "select stock.id,stock.section,stock.lot,stock.parcelle,stock.superficie," +
                "CONCAT (client.nom,' ',client.prenom) as Client," +
                " CONCAT(demarcheur.nom,' ', demarcheur.prenom) as Demarcheur" +
                ", stock.idclient, stock.iddemarcheur," +
                "Montant, Reste, cloture" +
                " from stock join client on client.id = stock.idclient" +
                " join demarcheur on demarcheur.id = stock.iddemarcheur"+
                " where stock.etat=@etat and stock.deleted=0";

            if (siteid!="0")
            {
                aa+= " and stock.siteid=@siteid";
            }
            MySqlDataAdapter da = new MySqlDataAdapter(aa, con);
            da.SelectCommand.Parameters.Add("@etat", MySqlDbType.VarChar).Value=etat;
            if (siteid!="0")
            {
                da.SelectCommand.Parameters.Add("@siteid", MySqlDbType.Int32).Value=siteid;
            }

            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public bool add(string siteid,string section, string lot, string prcle, string spf, 
            string prix, string mnt, string rest,string client,  string etat)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("insert into stock (siteid,section,lot, parcelle, superficie, prix, montant,reste," +
                    "idclient, etat,proprietaire) Values (@siteid,@section,@lot,@prcle,@spf,@prix,@mnt,@rest,@client,@etat,@proprietaire)", con);
                cmd.Parameters.Add("@siteid", MySqlDbType.Int32).Value = siteid;
                cmd.Parameters.Add("@section", MySqlDbType.VarChar).Value = section;
                cmd.Parameters.Add("@lot",MySqlDbType.Int32).Value = lot;
                cmd.Parameters.Add("@prcle", MySqlDbType.VarChar).Value = prcle;
                cmd.Parameters.Add("@spf", MySqlDbType.Decimal).Value = spf;
                cmd.Parameters.Add("@prix", MySqlDbType.Decimal).Value = prix;
                cmd.Parameters.Add("@mnt", MySqlDbType.Decimal).Value = mnt;
                cmd.Parameters.Add("@rest", MySqlDbType.Decimal).Value = rest;
                cmd.Parameters.Add("@client", MySqlDbType.Int32).Value = client;
                cmd.Parameters.Add("@etat", MySqlDbType.VarChar).Value = etat;
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
        public bool add(string siteid,string section, string lot, string prcle, string spf, string etat)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("insert into stock (siteid,section,lot, parcelle,superficie, etat) " +
                    "Values (@siteid,@section,@lot,@prcle,@spf,@etat)", con);

                cmd.Parameters.Add("@siteid", MySqlDbType.Int32).Value = siteid;
                cmd.Parameters.Add("@section", MySqlDbType.VarChar).Value = section;
                cmd.Parameters.Add("@lot", MySqlDbType.Int32).Value = lot;
                cmd.Parameters.Add("@prcle", MySqlDbType.VarChar).Value = prcle;
                cmd.Parameters.Add("@spf", MySqlDbType.Decimal).Value = spf;
                cmd.Parameters.Add("@etat", MySqlDbType.VarChar).Value = etat;
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
        public bool vente(string id, string idclient,string iddemarcheur, 
            string prix, string montant, string reste, string usage, string etat)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("update stock set idclient=@idclient,iddemarcheur=@iddemarcheur, prix=@prix, " +
                    "montant=@montant, type_usage=@usage, reste=@reste, etat=@etat where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.Parameters.Add("@idclient", MySqlDbType.Int32).Value=idclient;
                cmd.Parameters.Add("@iddemarcheur", MySqlDbType.Int32).Value=iddemarcheur;
                cmd.Parameters.Add("@prix", MySqlDbType.Decimal).Value=prix;
                cmd.Parameters.Add("@montant", MySqlDbType.Decimal).Value=montant;
                cmd.Parameters.Add("@reste", MySqlDbType.Decimal).Value=reste;
                cmd.Parameters.Add("@usage", MySqlDbType.VarChar).Value=usage;
                cmd.Parameters.Add("@etat", MySqlDbType.VarChar).Value=etat;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool update(string id, string site,string section, string lot, string prcle, string spfc)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("update stock set siteid=@siteid,lot=@lot, parcelle=@prcle," +
                    "superficie=@spfc, section=@section where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.Parameters.Add("@section", MySqlDbType.VarChar).Value=section;
                cmd.Parameters.Add("@siteid", MySqlDbType.Int32).Value=site;
                cmd.Parameters.Add("@lot", MySqlDbType.Int32).Value=lot;
                cmd.Parameters.Add("@prcle", MySqlDbType.VarChar).Value=prcle;
                cmd.Parameters.Add("@spfc", MySqlDbType.Decimal).Value=spfc;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool update(string id, string idclient, string iddemarcheur,
            string montant, string prix,  string reste, string etat, string usage)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("update stock set idclient=@idclient,iddemarcheur=@iddemarcheur, montant=@montant," +
                    "prix=@prix, reste=@reste,etat=@etat where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.Parameters.Add("@idclient", MySqlDbType.Int32).Value=idclient;
                cmd.Parameters.Add("@iddemarcheur", MySqlDbType.Int32).Value=iddemarcheur;
                cmd.Parameters.Add("@montant", MySqlDbType.Decimal).Value=montant;
                cmd.Parameters.Add("@prix", MySqlDbType.Decimal).Value=prix;
                cmd.Parameters.Add("@reste", MySqlDbType.Decimal).Value=reste;
                cmd.Parameters.Add("@etat", MySqlDbType.VarChar).Value=etat;
                cmd.Parameters.Add("@usage", MySqlDbType.VarChar).Value=usage;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool setDemarcheur(string id, string iddemarcheur)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("update stock set iddemarcheur=@iddemarcheur where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.Parameters.Add("@iddemarcheur", MySqlDbType.Int32).Value=iddemarcheur;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool setClient(string id, string idclient)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("update stock set idclient=@idclient where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.Parameters.Add("@idclient", MySqlDbType.Int32).Value=idclient;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool setVersement(string id, string montant, string reste,string etat)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand(
                    "update stock set montant=@montant, reste=@reste, etat=@etat where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.Parameters.Add("@montant", MySqlDbType.Decimal).Value=montant;
                cmd.Parameters.Add("@reste", MySqlDbType.Decimal).Value=reste;
                cmd.Parameters.Add("@etat", MySqlDbType.VarChar).Value=etat;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool delete(string id)
        {
            try
            {
                con.Open();

                cmd = new MySqlCommand("update stock set deleted=1 where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
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
        public bool cloturer(string id)
        {
            try
            {
                con.Open();

                cmd = new MySqlCommand("update stock set cloture=1 where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
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
    }
}
