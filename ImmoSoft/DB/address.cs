using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmoSoft.DB
{
   
    internal class address
    {
        string ip, user, pass;
        public address()
        {

            //ip="localhost";
            ip = "192.168.1.5";
            user = "root";
            pass = "";
        }
        public string getAddress()
        {
            //return "datasource=" + address + ";port=3306;username="+user+"; password="+pass+ ";database=ayinde2;";
            return "datasource=" + ip + ";port=3308;username=" + user + "; password=" + pass + ";database=immosoft;";
        }

    }
}
