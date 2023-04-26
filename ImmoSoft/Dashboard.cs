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
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Charts;
using LiveCharts.Wpf;

namespace ImmoSoft
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        void setChart(DataTable dt, DateTime from)
        {
            List<double> ventes = new List<double>();
            List<double> mutations = new List<double>();
            DataTable table = new DataTable();
            table.Columns.Add("Ventes");
            table.Columns.Add("Mutations");
            table.Columns.Add("Date");
            double vtotal = 0, mtotal = 0, ctotal=0;
            while (from.Date < DateTime.Now.Date)
            {
                var res = getCount(dt, from.Date.ToString().Split(' ')[0]);
                vtotal+=res.Item1;
                mtotal+=res.Item2;
                ctotal+=res.Item3;

                ventes.Add(res.Item1);
                mutations.Add(res.Item2);

                table.Rows.Add(res.Item1, res.Item2, from.Date.ToString().Split(' ')[0]);
                from=from.AddDays(1);
            }
            venteNum.Text= vtotal.ToString();
            muteNum.Text= mtotal.ToString();
            clotNum.Text= ctotal.ToString();
            dgv1.DataSource = table;


            try
            {
                cartesianChart1.Series.Add(new LineSeries
                {
                    Values = new ChartValues<double>(ventes),
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(29, 137, 152)),
                    //Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                });
                cartesianChart1.Series.Add(new LineSeries
                {
                    Values = new ChartValues<double>(mutations),
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(29, 137, 152)),
                    //Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                });
               
            }
            catch (Exception ex) { }
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            DB.historique historique = new DB.historique();
            DataTable dt = new DataTable();
            DateTime from = DateTime.Now.AddDays(-30);
            dt=historique.refresh(from.Year.ToString() + "-" + from.Month.ToString() + "-" + from.Day.ToString() + " 00:00:01",
                DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 23:59:59", null);

            //dgv1.DataSource = dt;

            setChart(dt, from);
        }
        (double,double,double) getCount(DataTable dt,string date)
        {
            double vcount = 0, mcount=0, ccount=0;
            if(dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["action"].ToString().ToLower().Contains("vente assigné") && dr["date"].ToString().Contains(date))
                        vcount++;
                    else if (dr["action"].ToString().ToLower().Contains("mutation assigné") && dr["date"].ToString().Contains(date))
                        mcount++;
                    else if (dr["action"].ToString().ToLower().Contains("cloture") && dr["date"].ToString().Contains(date))
                        ccount++;
                }
            }
            return (vcount,mcount,ccount);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            using (System.Drawing.Drawing2D.LinearGradientBrush brush =
           new System.Drawing.Drawing2D.LinearGradientBrush(this.ClientRectangle,
                                                          ColorTranslator.FromHtml("#FE4C4B"),
                                                          ColorTranslator.FromHtml("#FFFFFF"),
                                                          90F))
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            using (System.Drawing.Drawing2D.LinearGradientBrush brush =
          new System.Drawing.Drawing2D.LinearGradientBrush(this.ClientRectangle,
                                                         ColorTranslator.FromHtml("#3C6D94"),
                                                         ColorTranslator.FromHtml("#FFFFFF"),
                                                         90F))
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            using (System.Drawing.Drawing2D.LinearGradientBrush brush =
          new System.Drawing.Drawing2D.LinearGradientBrush(this.ClientRectangle,
                                                         ColorTranslator.FromHtml("#008080"),
                                                         ColorTranslator.FromHtml("#FFFFFF"),
                                                         90F))
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }
    }
}
