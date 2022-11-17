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
            this.DoubleBuffered = true;
            this.ReadOnly = true;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.RowHeadersVisible = false;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.ColumnHeadersDefaultCellStyle = style;
            style.Font = new Font("Cambria", 13);

            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //style.WrapMode = DataGridViewTriState.True;
            style.SelectionBackColor= Color.Teal;
            style.Font = new Font("Cambria", 12);

            this.DefaultCellStyle = style;
            this.DataSourceChanged+=(s, args) =>
            {
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
                }
            };
        }
        
    }
}
