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
    internal class champsfe
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public champsfe()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
        }
        public DataTable refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter(
                "select * from champs where deleted=0", con);
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
                "select * from champs where id=@id and deleted=0", con);
            da.SelectCommand.Parameters.Add("@id", MySqlDbType.Int32).Value=id;

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
            string aa = "select champs.id,champs.section,champs.lot,champs.parcelle,champs.superficie," +
                " CONCAT(pro.nom,' ',pro.prenom) as Proprietaire," +
                " CONCAT(cl.nom,' ',cl.prenom) as Client," +
                " CONCAT(demarcheur.nom,' ',demarcheur.prenom) as Demarcheur," +
                " champs.idold, champs.idnew, iddemarcheur" +
                " from champs join demarcheur on demarcheur.id = champs.iddemarcheur" +
                " join client pro on pro.id=champs.idold" +
                " join client cl on cl.id = champs.idnew"+
                " where champs.etat=@etat and champs.deleted=0";
            if (siteid!="0")
            {
                aa+= " and champs.siteid=@siteid";
            }
            da = new MySqlDataAdapter(aa, con);
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
                "select champs.id,champs.section,champs.lot,champs.parcelle,champs.superficie," +
                " CONCAT(pro.nom,' ',pro.prenom) as Proprietaire," +
                " CONCAT(cl.nom,' ',cl.prenom) as Client," +
                " CONCAT(demarcheur.nom,' ',demarcheur.prenom) as Demarcheur,"+
                " champs.idold, champs.iddemarcheur, champs.idnew," +
                " champs.montant, champs.reste, cloture from champs" +
                " join client pro on pro.id=champs.idold" +
                " join client cl on cl.id = champs.idnew"+
                " join demarcheur on demarcheur.id = champs.iddemarcheur"+
                " where champs.etat=@etat and champs.deleted=0";

            if (siteid!="0")
            {
                aa+= " and champs.siteid=@siteid";
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
        public bool add(string siteid, string section, string lot, string prcle, string spf,
            string prix, string mnt, string rest, string client, string etat)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("insert into champs (siteid,section,lot, parcelle, superficie, prix, montant,reste," +
                    "idclient, etat,proprietaire) Values (@siteid,@section,@lot,@prcle,@spf,@prix,@mnt,@rest,@client,@etat,@proprietaire)", con);
                cmd.Parameters.Add("@siteid", MySqlDbType.Int32).Value = siteid;
                cmd.Parameters.Add("@section", MySqlDbType.VarChar).Value = section;
                cmd.Parameters.Add("@lot", MySqlDbType.Int32).Value = lot;
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
        public bool add(string siteid, string section, string lot, string prcle, string spf, string etat, string idold)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("insert into champs (siteid,section,lot, parcelle,superficie, etat,idold) " +
                    "Values (@siteid,@section,@lot,@prcle,@spf,@etat,@idold)", con);

                cmd.Parameters.Add("@siteid", MySqlDbType.Int32).Value = siteid;
                cmd.Parameters.Add("@section", MySqlDbType.VarChar).Value = section;
                cmd.Parameters.Add("@lot", MySqlDbType.Int32).Value = lot;
                cmd.Parameters.Add("@prcle", MySqlDbType.VarChar).Value = prcle;
                cmd.Parameters.Add("@spf", MySqlDbType.Decimal).Value = spf;
                cmd.Parameters.Add("@etat", MySqlDbType.VarChar).Value = etat;
                cmd.Parameters.Add("@idold", MySqlDbType.Int32).Value = idold;
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
        public bool mutation(string id,string idold, string idclient, string iddemarcheur,
            string prix, string montant, string reste, string usage, string etat)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("update champs set idold=@idold,idnew=@idclient,iddemarcheur=@iddemarcheur, prix=@prix, " +
                    "montant=@montant, type_usage=@usage, reste=@reste, etat=@etat where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.Parameters.Add("@idold", MySqlDbType.Int32).Value=idold;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool update(string id, string site, string section, string lot, string prcle, string spfc,string idold)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("update champs set site=@site,lot=@lot, parcelle=@prcle," +
                    "superficie=@spfc, section=@section,@idold=idold where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.Parameters.Add("@section", MySqlDbType.VarChar).Value=section;
                cmd.Parameters.Add("@site", MySqlDbType.Int32).Value=site;
                cmd.Parameters.Add("@lot", MySqlDbType.Int32).Value=lot;
                cmd.Parameters.Add("@prcle", MySqlDbType.VarChar).Value=prcle;
                cmd.Parameters.Add("@spfc", MySqlDbType.Decimal).Value=spfc;
                cmd.Parameters.Add("@idold", MySqlDbType.Int32).Value=idold;
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
        public bool update(string id, string idnew, string iddemarcheur,
            string montant, string prix, string reste, string etat, string usage)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("update champs set idnew=@idnew,iddemarcheur=@iddemarcheur, montant=@montant," +
                    "prix=@prix, reste=@reste,etat=@etat where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.Parameters.Add("@idnew", MySqlDbType.Int32).Value=idnew;
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
                cmd = new MySqlCommand("update champs set iddemarcheur=@iddemarcheur where id=@id", con);
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
                cmd = new MySqlCommand("update champs set idnew=@idclient where id=@id", con);
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
        public bool setVersement(string id, string montant, string reste, string etat)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand(
                    "update champs set montant=@montant, reste=@reste, etat=@etat where id=@id", con);
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

                cmd = new MySqlCommand("update champs set deleted=1 where id=@id", con);
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

                cmd = new MySqlCommand("update champs set cloture=1 where id=@id", con);
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
