namespace ImmoSoft
{
    partial class Site
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.parcellesDisponiblesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enCoursDeVenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.venteEnCoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parcellesVenduesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mutationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mutationEnCoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parcellesMutéesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv1 = new ImmoSoft.DGV();
            this.groupBox1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(397, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(213, 158);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkSlateGray;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(4, 110);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(204, 37);
            this.button3.TabIndex = 0;
            this.button3.Text = "Supprimer Site";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkSlateGray;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(4, 68);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(204, 37);
            this.button2.TabIndex = 0;
            this.button2.Text = "Modifier Site";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(4, 27);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(204, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ajouter Site";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parcellesDisponiblesToolStripMenuItem,
            this.enCoursDeVenteToolStripMenuItem,
            this.mutationToolStripMenuItem});
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(184, 92);
            // 
            // parcellesDisponiblesToolStripMenuItem
            // 
            this.parcellesDisponiblesToolStripMenuItem.Name = "parcellesDisponiblesToolStripMenuItem";
            this.parcellesDisponiblesToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.parcellesDisponiblesToolStripMenuItem.Text = "Parcelles disponibles";
            // 
            // enCoursDeVenteToolStripMenuItem
            // 
            this.enCoursDeVenteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.venteEnCoursToolStripMenuItem,
            this.parcellesVenduesToolStripMenuItem});
            this.enCoursDeVenteToolStripMenuItem.Name = "enCoursDeVenteToolStripMenuItem";
            this.enCoursDeVenteToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.enCoursDeVenteToolStripMenuItem.Text = "Vente";
            // 
            // venteEnCoursToolStripMenuItem
            // 
            this.venteEnCoursToolStripMenuItem.Name = "venteEnCoursToolStripMenuItem";
            this.venteEnCoursToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.venteEnCoursToolStripMenuItem.Text = "Vente en cours";
            // 
            // parcellesVenduesToolStripMenuItem
            // 
            this.parcellesVenduesToolStripMenuItem.Name = "parcellesVenduesToolStripMenuItem";
            this.parcellesVenduesToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.parcellesVenduesToolStripMenuItem.Text = "Parcelles vendues";
            // 
            // mutationToolStripMenuItem
            // 
            this.mutationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mutationEnCoursToolStripMenuItem,
            this.parcellesMutéesToolStripMenuItem});
            this.mutationToolStripMenuItem.Name = "mutationToolStripMenuItem";
            this.mutationToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.mutationToolStripMenuItem.Text = "Mutation";
            // 
            // mutationEnCoursToolStripMenuItem
            // 
            this.mutationEnCoursToolStripMenuItem.Name = "mutationEnCoursToolStripMenuItem";
            this.mutationEnCoursToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.mutationEnCoursToolStripMenuItem.Text = "Mutation en cours";
            // 
            // parcellesMutéesToolStripMenuItem
            // 
            this.parcellesMutéesToolStripMenuItem.Name = "parcellesMutéesToolStripMenuItem";
            this.parcellesMutéesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.parcellesMutéesToolStripMenuItem.Text = "Parcelles mutées";
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToDeleteRows = false;
            this.dgv1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Cambria", 12F);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            this.dgv1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Cambria", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv1.Location = new System.Drawing.Point(9, 8);
            this.dgv1.Margin = new System.Windows.Forms.Padding(2);
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.RowHeadersVisible = false;
            this.dgv1.RowHeadersWidth = 51;
            this.dgv1.RowTemplate.Height = 24;
            this.dgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv1.Size = new System.Drawing.Size(383, 484);
            this.dgv1.TabIndex = 10;
            this.dgv1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv1_MouseClick);
            // 
            // Site
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 502);
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Site";
            this.Text = "Site";
            this.Load += new System.EventHandler(this.Site_Load);
            this.groupBox1.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DGV dgv1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem parcellesDisponiblesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enCoursDeVenteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem venteEnCoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parcellesVenduesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mutationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mutationEnCoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parcellesMutéesToolStripMenuItem;
    }
}