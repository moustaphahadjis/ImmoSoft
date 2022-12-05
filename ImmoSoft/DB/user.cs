using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace ImmoSoft.DB
{
    internal class user
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public user()
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
                    "select * from user where id=@id and deleted=0", con);
                da.SelectCommand.Parameters.Add("@id", MySqlDbType.Int32).Value=id;

                DataTable ds = new DataTable();
                ds.BeginLoadData();
                da.Fill(ds);
                ds.EndLoadData();
                con.Close();
                return ds;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erreur server");
                return null;
            }
        }
        public DataTable checkUser(string username, string password)
        {
            try
            {
                password = encode(password);
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(
                    "select * from user where username=@username and password=@password and deleted=0", con);

                da.SelectCommand.Parameters.Add("@username", MySqlDbType.VarChar).Value=username;
                da.SelectCommand.Parameters.Add("@password", MySqlDbType.VarChar).Value=password;

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
        public bool usernameExist(string username)
        {
            try
            {
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(
                    "select * from user where username=@username and deleted=0", con);
                da.SelectCommand.Parameters.Add("@username", MySqlDbType.VarChar).Value=username;
                DataTable ds = new DataTable();
                ds.BeginLoadData();
                da.Fill(ds);
                ds.EndLoadData();
                con.Close();
                if (ds.Rows.Count>0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur server");
                return false;
            }
        }
        public bool usernameExist(string username,string id)
        {
            try
            {
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(
                    "select * from user where username=@username and deleted=0", con);
                da.SelectCommand.Parameters.Add("@username", MySqlDbType.VarChar).Value=username;
                DataTable ds = new DataTable();
                ds.BeginLoadData();
                da.Fill(ds);
                ds.EndLoadData();
                con.Close();
                bool r = false;
                if (ds.Rows.Count>0)
                {
                    foreach (DataRow row in ds.Rows)
                        if (row["id"].ToString()!=id)
                            r=true;
                }
                return r;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur server");
                return false;
            }
        }
        public bool checkPassword(string id, string password)
        {
            try
            {
                con.Open();
                 cmd = new MySqlCommand(
                    "select password from user where id=@id and deleted=0", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                var reader = cmd.ExecuteReader();
                bool r=false;
                if (reader.Read())
                    if (reader.GetString(0)==encode(password))
                        r=true;
                reader.Close();
                con.Close();
                return r;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur server");
                return false;
            }
        }
        public bool insert(string nom, string prenom, string address,
            string contact, string username, string password, string admin)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("insert into user " +
                    "(nom, prenom, username, password, admin,adresse,contact) values" +
                    "(@nom, @prenom, @username, @password, @admin, @address, @contact)",con);
                cmd.Parameters.Add("@nom",MySqlDbType.VarChar).Value=nom;
                cmd.Parameters.Add("@address",MySqlDbType.VarChar).Value=address;
                cmd.Parameters.Add("@contact",MySqlDbType.VarChar).Value=contact;
                cmd.Parameters.Add("@prenom", MySqlDbType.VarChar).Value=prenom;
                cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value=username;
                cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value=encode(password);
                cmd.Parameters.Add("@admin", MySqlDbType.VarChar).Value=admin;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erreur lors de l'insertion");
                return false;
            }
        }
        public bool update(string id, string nom, string prenom, string address,
            string contact, string username, string password, string admin)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand(
                    "update user set nom=@nom, prenom=@prenom, adresse=@address," +
                    "contact=@contact, username=@username, password=@password,admin=@admin" +
                    " where id=@id", con);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value=id;
                cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value=nom;
                cmd.Parameters.Add("@address", MySqlDbType.VarChar).Value=address;
                cmd.Parameters.Add("@contact", MySqlDbType.VarChar).Value=contact;
                cmd.Parameters.Add("@prenom", MySqlDbType.VarChar).Value=prenom;
                cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value=username;
                cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value=encode(password);
                cmd.Parameters.Add("@admin", MySqlDbType.VarChar).Value=admin;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'insertion");
                return false;
            }
        }
        public static string encode(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decord your Password
        public string decode(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        public bool delete(string id)
        {
            try
            {
                con.Open();

                cmd = new MySqlCommand("update user set deleted=1 where id=@id", con);
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
