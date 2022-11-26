using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmoSoft
{
    class DGV : DataGridView
    {

        public DGV()
        {
            this.refresh();
        }
        void refresh()
        {
            this.DoubleBuffered = true;
            this.ReadOnly = true;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.RowHeadersVisible = false;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

            DataGridViewCellStyle styleHeader = new DataGridViewCellStyle();
            styleHeader.Alignment = DataGridViewContentAlignment.MiddleCenter;
            styleHeader.Font = new Font("Cambria", 11, FontStyle.Regular);

            DataGridViewCellStyle styleRow = new DataGridViewCellStyle();
            styleRow.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //style.WrapMode = DataGridViewTriState.True;
            styleRow.SelectionBackColor= Color.FromArgb(60, 109, 148);
            styleRow.Font = new Font("Arial", 10);

            this.DataSourceChanged+=(s, args) =>
            {
                foreach (DataGridViewColumn col in this.Columns)
                    col.HeaderText= col.HeaderText.ToUpper();
                this.ColumnHeadersDefaultCellStyle = styleHeader;
                this.DefaultCellStyle = styleRow;

                if (this.Columns.Count > 0)
                {
                    if (this.Columns.Contains("id"))
                        this.Columns["id"].Visible=false;
                    if (this.Columns.Contains("deleted"))
                        this.Columns["deleted"].Visible=false;
                    if (this.Columns.Contains("updatedate"))
                        this.Columns["updatedate"].Visible=false;
                    if (this.Columns.Contains("idclient"))
                        this.Columns["idclient"].Visible=false;
                    if (this.Columns.Contains("iddemarcheur"))
                        this.Columns["iddemarcheur"].Visible=false;
                    if (this.Columns.Contains("stockid"))
                        this.Columns["stockid"].Visible=false;
                    if (this.Columns.Contains("siteid"))
                        this.Columns["siteid"].Visible=false;
                    if (this.Columns.Contains("type_usage"))
                        this.Columns["type_usage"].Visible=false;
                    if (this.Columns.Contains("idnew"))
                        this.Columns["idnew"].Visible=false;
                    if (this.Columns.Contains("idold"))
                        this.Columns["idold"].Visible=false;
                    if (this.Columns.Contains("idstock"))
                        this.Columns["idstock"].Visible=false;
                    if (this.Columns.Contains("idchamps"))
                        this.Columns["idchamps"].Visible=false;

                    if (this.Columns.Contains("lot"))
                        this.Columns["lot"].AutoSizeMode= DataGridViewAutoSizeColumnMode.AllCells;
                    if (this.Columns.Contains("section"))
                        this.Columns["section"].AutoSizeMode= DataGridViewAutoSizeColumnMode.AllCells;
                    if (this.Columns.Contains("parcelle"))
                        this.Columns["parcelle"].AutoSizeMode= DataGridViewAutoSizeColumnMode.AllCells;
                    if (this.Columns.Contains("Superficie"))
                        this.Columns["superficie"].AutoSizeMode= DataGridViewAutoSizeColumnMode.AllCells;
                    if (this.Columns.Contains("image"))
                        ((DataGridViewImageColumn)this.Columns["image"]).ImageLayout= DataGridViewImageCellLayout.Zoom;

                }
            };
        }
    }
}
