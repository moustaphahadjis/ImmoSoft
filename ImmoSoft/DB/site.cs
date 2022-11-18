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
    internal class site
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public site()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
        }
        public DataTable refresh()
        {
            con.Open();


            MySqlDataAdapter da = new MySqlDataAdapter("select * from site where deleted=0", con);
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


            MySqlDataAdapter da = new MySqlDataAdapter("select * from site where id=@id deleted=0", con);
            da.SelectCommand.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
            DataTable ds = new DataTable();
            ds.BeginLoadData();
            da.Fill(ds);
            ds.EndLoadData();

            con.Close();
            return ds;
        }
        public string add(string nom, string ville, string taille)
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter("Select * from site where nom=@nom and deleted=0", con);
                da.SelectCommand.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                da.Fill(dt);
                if (dt.Rows.Count==0)
                {
                    cmd = new MySqlCommand("insert into site (nom,ville,taille) Values (@nom,@ville,@taille)", con);
                    cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                    cmd.Parameters.Add("@ville", MySqlDbType.VarChar).Value = ville;
                    cmd.Parameters.Add("@taille", MySqlDbType.VarChar).Value = taille;
                    cmd.ExecuteNonQuery();
                    da= new MySqlDataAdapter("select id from site Order By id Desc Limit 1", con);
                    DataTable dt2 = new DataTable();
                    da.Fill(dt2);
                    return dt2.Rows[0]["id"].ToString();
                }
                else
                {
                    con.Close();
                    return dt.Rows[0]["id"].ToString();
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return "0";
            }
        }
        public bool update(string id, string nom, string ville, string taille)
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter("Select * from site where nom=@nom and deleted=0", con);
                da.SelectCommand.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                da.Fill(dt);
                if (dt.Rows.Count==0)
                {
                    cmd = new MySqlCommand("update site set nom=@nom, ville=@ville, taille=@taille where id=@id)", con);
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                    cmd.Parameters.Add("@ville", MySqlDbType.VarChar).Value = ville;
                    cmd.Parameters.Add("@taille", MySqlDbType.VarChar).Value = taille;
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Un autre site existe avec ce nom!");
                    return false;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }
    }
}
