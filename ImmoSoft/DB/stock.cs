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
        public DataTable refresh(string etat,string action)
        {
            con.Open();
            string aa = "select stock.id,stock.section,stock.lot,stock.parcelle,stock.superficie,client.nom, client.prenom," +
                " demarcheur.nom,demarcheur.prenom, idclient, iddemarcheur" +
                " from stock join demarcheur on demarcheur.id = stock.iddemarcheur" +
                " join client on client.id=stock.idclient"+
                " where stock.etat=@etat and stock.deleted=0";

            MySqlDataAdapter da = new MySqlDataAdapter(
               aa , con);
            da.SelectCommand.Parameters.Add("@etat", MySqlDbType.VarChar).Value=etat;

            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.Columns[5].ColumnName="Client";
            ds.Columns[7].ColumnName="Demarcheur";
            if(ds.Rows.Count>0)
                foreach(DataRow row in ds.Rows)
                {
                    row[5]=row[5]+" "+row[6];
                    row[7]=row[7]+" "+row[8];
                }
            ds.Columns.RemoveAt(7);
            ds.Columns.RemoveAt(5);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public DataTable refresh(string etat, int vendue)
        {
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(
                "select stock.id,stock.section,stock.lot,stock.parcelle,stock.superficie,client.nom,client.prenom," +
                " demarcheur.nom, demarcheur.prenom, stock.idclient, stock.iddemarcheur" +
                " from stock join client on client.id = stock.idclient" +
                " join demarcheur on demarcheur.id = stock.iddemarcheur"+
                " where stock.etat=@etat and stock.deleted=0", con);
            da.SelectCommand.Parameters.Add("@etat", MySqlDbType.VarChar).Value=etat;

            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.Columns[4].ColumnName="Client";
            ds.Columns[6].ColumnName="Demarcheur";
            if (ds.Rows.Count>0)
                foreach (DataRow row in ds.Rows)
                {
                    row[5]=row[5]+" "+row[6];
                    row[7]=row[7]+" "+row[8];
                }
            ds.Columns.RemoveAt(7);
            ds.Columns.RemoveAt(5);
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
                    "idclient, etat) Values (@siteid,@section,@lot,@prcle,@spf,@prix,@mnt,@rest,@client,@etat)", con);
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
                cmd = new MySqlCommand("update stock set site=@site,lot=@lot, parcelle=@prcle," +
                    "superficie=@spfc, section=@section where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.Parameters.Add("@section", MySqlDbType.VarChar).Value=section;
                cmd.Parameters.Add("@site", MySqlDbType.Int32).Value=site;
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
    }
}
