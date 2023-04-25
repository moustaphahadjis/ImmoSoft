using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;

namespace ImmoSoft.DB
{
    internal class template
    {
        MySqlConnection con;
        MySqlCommand cmd;

        public template()
        {
            address add = new address();
            con = new MySqlConnection(add.getAddress());
            con.Close();
        }
        public bool save( string nom, string path)
        {
            try
            {
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                UInt32 size = (UInt32)file.Length;
                byte[] data = new byte[size];

                file.Read(data, 0, data.Length);
                file.Close();

                if (con.State==ConnectionState.Open)
                    con.Close();
                con.Open();
                cmd= new MySqlCommand("Insert into template (nom,file,size) values(@nom,@file,@size)", con);
                cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value=nom;
                cmd.Parameters.Add("@file", MySqlDbType.LongBlob).Value=data;
                cmd.Parameters.Add("@size", MySqlDbType.UInt32).Value=size;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erreur lors de l'enregistrement");
                return false;
            }
        }

        public string open(string name)
        {
            try
            {
                con.Open();
                cmd= new MySqlCommand(
                    "Select nom,size,file from template where nom=@nom", con);
                cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value=name;
                string nom;
                UInt32 FileSize;
                byte[] rawData;
                FileStream fs;
                MySqlDataReader myData = cmd.ExecuteReader();

                if (!myData.HasRows)
                    throw new Exception("There are no BLOBs to save");
                myData.Read();
                nom=myData.GetString(myData.GetOrdinal("nom"));
                FileSize = myData.GetUInt32(myData.GetOrdinal("size"));
                rawData = new byte[FileSize];
                myData.GetBytes(myData.GetOrdinal("file"), 0, rawData, 0, (int)FileSize);

                string filepath = System.IO.Path.GetTempPath()+ DateTime.Now.ToString("HH-mm")+" "+nom;
                fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(rawData, 0, (int)FileSize);
                fs.Close();
                con.Close();
                return filepath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'enregistrement");
                return null;
            }
        }
    }
}
