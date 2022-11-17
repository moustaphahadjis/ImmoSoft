using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmoSoft.DB
{
    internal class common
    {
        public common()
        {

        }
        public bool isNumber(string tmp)
        {
            bool r = true;
            foreach(char c in tmp)
                if(!char.IsDigit(c))
                    r = false;
            return r;
        }
        public bool exist(DataTable dt, string tmp)
        {
            bool r= false;
            foreach(DataRow row in dt.Rows)
            {
                if (row["nom"].ToString().ToUpper().Trim() == tmp.ToUpper().Trim())
                    r = true;
            }
            return r;
        }
        public bool isPhone(string tmp)
        {
            bool r = true;
            if (tmp.Length>0)
            {
                foreach (char c in tmp)
                    if (!char.IsDigit(c)
                        && c!='+'
                        && c!='-'
                        && c!='/')
                        r= false;
            }
            else
                r=false;
            return r;
        }
        public DGV searchPerson(string tmp, DGV dgv)
        {
            if (dgv.Rows.Count>1)
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells["nom"].Value.ToString().ToUpper().Contains(tmp.ToUpper()) ||
                            row.Cells["prenom"].Value.ToString().ToUpper().Contains(tmp.ToUpper()) ||
                            row.Cells["contact"].Value.ToString().ToUpper().Contains(tmp.ToUpper()))
                    {
                        row.Selected = true;
                        dgv.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                    else
                    {
                        row.Selected = false;
                    }
                }
            return dgv;
        }
    }
}
