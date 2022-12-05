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
    internal class demarcheur
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public demarcheur()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
        }
        public DataTable refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select * from demarcheur where deleted=0", con);
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.Rows.Remove(ds.Rows[0]);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public DataTable refresh(string id)
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select * from demarcheur where id=@id and deleted=0", con);
            da.SelectCommand.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public bool add(byte[] image,string nom, string prenom, string piece,
            string numero, string delivrance, string profession,
            string contact, string addresse, string matrimonial)
        {
            try
            {
                con.Open();
                cmd=new MySqlCommand("select count(*) from demarcheur where contact=@contact and deleted=0", con);
                cmd.Parameters.Add("@contact", MySqlDbType.VarChar).Value = contact;
                if (int.Parse(cmd.ExecuteScalar().ToString())==0)
                {
                    cmd = new MySqlCommand("insert into demarcheur " +
                        "(image,nom,prenom,piece,numero,contact,addresse,delivrance,profession,matrimonial)" +
                        " Values (@image,@nom,@prenom,@piece,@numero,@contact,@addresse,delivrance,profession,matrimonial)", con);
                cmd.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = image;
                    cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                    cmd.Parameters.Add("@prenom", MySqlDbType.VarChar).Value = prenom;
                    cmd.Parameters.Add("@piece", MySqlDbType.VarChar).Value = piece;
                    cmd.Parameters.Add("@numero", MySqlDbType.VarChar).Value = numero;
                    cmd.Parameters.Add("@contact", MySqlDbType.VarChar).Value = contact;
                    cmd.Parameters.Add("@addresse", MySqlDbType.VarChar).Value = addresse;
                    cmd.Parameters.Add("@delivrance", MySqlDbType.VarChar).Value = delivrance;
                    cmd.Parameters.Add("@profession", MySqlDbType.VarChar).Value = profession;
                    cmd.Parameters.Add("@matrimonial", MySqlDbType.VarChar).Value = matrimonial;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("Un autre demarcheur existe avec ce contact", "Erreur",
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
        public bool update(byte[] image,string id, string nom, string prenom, string piece,
            string numero, string delivrance, string profession,
            string contact, string addresse, string matrimonial)
        {
            try
            {
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("select id from demarcheur where contact=@contact and deleted=0", con);
                da.SelectCommand.Parameters.Add("@contact", MySqlDbType.VarChar).Value = contact;
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                    foreach (DataRow row in dt.Rows)
                        if (row["id"].ToString()!=id)
                        {
                            MessageBox.Show("Un autre demarcheur existe avec ce contact", "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return false;
                        }

                cmd = new MySqlCommand("update demarcheur  set image=@image, nom=@nom, prenom=@prenom, piece=@piece, " +
                    "numero=@numero, contact=@contact, addresse=@addresse, delivrance=@delivrance," +
                    "profession=@profession, matrimonial=@matrimonial where id=@id", con);
                cmd.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = image;
                cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                cmd.Parameters.Add("@prenom", MySqlDbType.VarChar).Value = prenom;
                cmd.Parameters.Add("@piece", MySqlDbType.VarChar).Value = piece;
                cmd.Parameters.Add("@numero", MySqlDbType.VarChar).Value = numero;
                cmd.Parameters.Add("@contact", MySqlDbType.VarChar).Value = contact;
                cmd.Parameters.Add("@addresse", MySqlDbType.VarChar).Value = addresse;
                cmd.Parameters.Add("@delivrance", MySqlDbType.VarChar).Value = delivrance;
                cmd.Parameters.Add("@profession", MySqlDbType.VarChar).Value = profession;
                cmd.Parameters.Add("@matrimonial", MySqlDbType.VarChar).Value = matrimonial;
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
                cmd= new MySqlCommand("update demarcheur set deleted=1 where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.ExecuteNonQuery();
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
