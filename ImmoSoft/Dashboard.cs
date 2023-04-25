using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts.Charts;

namespace ImmoSoft
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            DB.historique historique = new DB.historique();
            DataTable dt = new DataTable();

            dt=historique.refresh(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + "1" + " 00:00:01",
                DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 23:59:59", null);

            dgv1.DataSource = dt;

            cartesianChart1.AxisX.Clear();
            DataTable venteDT= new DataTable();
            venteDT.Columns.Add("num");
            venteDT.Columns.Add("date", typeof(DateTime));
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["action"].ToString().ToLower().Contains("vente assigné"))
                    venteDT.Rows.Add("1", DateTime.Parse( dr["date"].ToString()));
            }
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis { Title="Vente" }) ;
        }
    }
}
