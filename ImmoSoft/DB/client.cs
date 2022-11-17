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
    internal class client
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public client()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
        }
        public DataTable refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select * from client where deleted=0", con);
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public DataTable refresh(string id )
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select * from client where id=@id and deleted=0", con);
            da.SelectCommand.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public bool add(string nom, string prenom, string piece, string numero, string contact, string addresse)
        {
            try
            {
                con.Open();
                cmd=new MySqlCommand("select count(*) from client where contact=@contact and deleted=0", con);
                cmd.Parameters.Add("@contact", MySqlDbType.VarChar).Value = contact;
                if (int.Parse(cmd.ExecuteScalar().ToString())==0)
                {
                    cmd = new MySqlCommand("insert into client (nom,prenom,piece,numero,contact,addresse)" +
                        " Values (@nom,@prenom,@piece,@numero,@contact,@addresse)", con);
                    cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                    cmd.Parameters.Add("@prenom", MySqlDbType.VarChar).Value = prenom;
                    cmd.Parameters.Add("@piece", MySqlDbType.VarChar).Value = piece;
                    cmd.Parameters.Add("@numero", MySqlDbType.VarChar).Value = numero;
                    cmd.Parameters.Add("@contact", MySqlDbType.VarChar).Value = contact;
                    cmd.Parameters.Add("@addresse", MySqlDbType.VarChar).Value = addresse;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("Un autre client existe avec ce contact", "Erreur",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }
        public bool update(string id, string nom, string prenom, string piece, string numero, string contact, string addresse)
        {
            try
            {
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("select id from client where contact=@contact and deleted=0", con);
                da.SelectCommand.Parameters.Add("@contact", MySqlDbType.VarChar).Value = contact;
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                    foreach (DataRow row in dt.Rows)
                        if (row["id"].ToString()!=id)
                        {
                            MessageBox.Show("Un autre client existe avec ce contact", "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return false;
                        }

                cmd = new MySqlCommand("update client set nom=@nom, prenom=@prenom, piece=@piece, " +
                    "numero=@numero, contact=@contact, addresse=@addresse where id=@id", con);
                cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                cmd.Parameters.Add("@prenom", MySqlDbType.VarChar).Value = prenom;
                cmd.Parameters.Add("@piece", MySqlDbType.VarChar).Value = piece;
                cmd.Parameters.Add("@numero", MySqlDbType.VarChar).Value = numero;
                cmd.Parameters.Add("@contact", MySqlDbType.VarChar).Value = contact;
                cmd.Parameters.Add("@addresse", MySqlDbType.VarChar).Value = addresse;
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
        public bool delete(string id)
        {
            try
            {
                con.Open();
                cmd= new MySqlCommand("update client set deleted=1 where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
