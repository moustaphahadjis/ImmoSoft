namespace ImmoSoft
{
    partial class Mutation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cancel = new System.Windows.Forms.Button();
            this.confirm = new System.Windows.Forms.Button();
            this.usage = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rest = new System.Windows.Forms.TextBox();
            this.vsmt = new System.Windows.Forms.TextBox();
            this.prix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvC = new ImmoSoft.DGV();
            this.selectD = new System.Windows.Forms.Button();
            this.dgvO = new ImmoSoft.DGV();
            this.selectC = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.selectP = new System.Windows.Forms.Button();
            this.dgvP = new ImmoSoft.DGV();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvD = new ImmoSoft.DGV();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.deleteC = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvO)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvP)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvD)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancel
            // 
            this.cancel.BackColor = System.Drawing.Color.IndianRed;
            this.cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel.ForeColor = System.Drawing.Color.White;
            this.cancel.Location = new System.Drawing.Point(519, 711);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(276, 52);
            this.cancel.TabIndex = 7;
            this.cancel.Text = "Annuler Mutation";
            this.cancel.UseVisualStyleBackColor = false;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // confirm
            // 
            this.confirm.BackColor = System.Drawing.Color.DarkSlateGray;
            this.confirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirm.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirm.ForeColor = System.Drawing.Color.White;
            this.confirm.Location = new System.Drawing.Point(249, 711);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(254, 52);
            this.confirm.TabIndex = 8;
            this.confirm.Text = "Confirmer Mutation";
            this.confirm.UseVisualStyleBackColor = false;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // usage
            // 
            this.usage.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usage.FormattingEnabled = true;
            this.usage.Items.AddRange(new object[] {
            "Habitation",
            "Agriculture",
            "Industrie"});
            this.usage.Location = new System.Drawing.Point(671, 18);
            this.usage.Name = "usage";
            this.usage.Size = new System.Drawing.Size(227, 29);
            this.usage.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.usage);
            this.groupBox4.Controls.Add(this.rest);
            this.groupBox4.Controls.Add(this.vsmt);
            this.groupBox4.Controls.Add(this.prix);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(18, 576);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1003, 116);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Details Payement";
            // 
            // rest
            // 
            this.rest.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rest.Location = new System.Drawing.Point(671, 64);
            this.rest.Name = "rest";
            this.rest.ReadOnly = true;
            this.rest.Size = new System.Drawing.Size(164, 29);
            this.rest.TabIndex = 1;
            // 
            // vsmt
            // 
            this.vsmt.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vsmt.Location = new System.Drawing.Point(165, 71);
            this.vsmt.Name = "vsmt";
            this.vsmt.ReadOnly = true;
            this.vsmt.Size = new System.Drawing.Size(223, 29);
            this.vsmt.TabIndex = 1;
            this.vsmt.TextChanged += new System.EventHandler(this.vsmt_TextChanged);
            // 
            // prix
            // 
            this.prix.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prix.Location = new System.Drawing.Point(165, 24);
            this.prix.Name = "prix";
            this.prix.Size = new System.Drawing.Size(223, 29);
            this.prix.TabIndex = 1;
            this.prix.TextChanged += new System.EventHandler(this.prix_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(848, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "FCFA";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(556, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "Type d\'usage";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(556, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "Reste à payer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(30, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Montant versé";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(397, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "FCFA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(397, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "FCFA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "Frais de mutation";
            // 
            // dgvC
            // 
            this.dgvC.AllowUserToAddRows = false;
            this.dgvC.AllowUserToDeleteRows = false;
            this.dgvC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Cambria", 12F);
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Cambria", 12F);
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvC.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvC.Location = new System.Drawing.Point(155, 21);
            this.dgvC.Name = "dgvC";
            this.dgvC.ReadOnly = true;
            this.dgvC.RowHeadersVisible = false;
            this.dgvC.RowHeadersWidth = 51;
            this.dgvC.RowTemplate.Height = 24;
            this.dgvC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvC.Size = new System.Drawing.Size(842, 92);
            this.dgvC.TabIndex = 2;
            // 
            // selectD
            // 
            this.selectD.BackColor = System.Drawing.Color.DarkSlateGray;
            this.selectD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.selectD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectD.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectD.ForeColor = System.Drawing.Color.White;
            this.selectD.Location = new System.Drawing.Point(12, 21);
            this.selectD.Name = "selectD";
            this.selectD.Size = new System.Drawing.Size(137, 44);
            this.selectD.TabIndex = 1;
            this.selectD.Text = "Choisir";
            this.selectD.UseVisualStyleBackColor = false;
            this.selectD.Click += new System.EventHandler(this.selectD_Click);
            // 
            // dgvO
            // 
            this.dgvO.AllowUserToAddRows = false;
            this.dgvO.AllowUserToDeleteRows = false;
            this.dgvO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvO.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Cambria", 12F);
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvO.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Cambria", 12F);
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvO.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvO.Location = new System.Drawing.Point(149, 21);
            this.dgvO.Name = "dgvO";
            this.dgvO.ReadOnly = true;
            this.dgvO.RowHeadersVisible = false;
            this.dgvO.RowHeadersWidth = 51;
            this.dgvO.RowTemplate.Height = 24;
            this.dgvO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvO.Size = new System.Drawing.Size(842, 94);
            this.dgvO.TabIndex = 2;
            // 
            // selectC
            // 
            this.selectC.BackColor = System.Drawing.Color.DarkSlateGray;
            this.selectC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.selectC.Enabled = false;
            this.selectC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectC.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectC.ForeColor = System.Drawing.Color.White;
            this.selectC.Location = new System.Drawing.Point(6, 44);
            this.selectC.Name = "selectC";
            this.selectC.Size = new System.Drawing.Size(137, 52);
            this.selectC.TabIndex = 1;
            this.selectC.Text = "Choisir";
            this.selectC.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvO);
            this.groupBox2.Controls.Add(this.selectC);
            this.groupBox2.Location = new System.Drawing.Point(21, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1000, 122);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Details Proprietaire";
            // 
            // selectP
            // 
            this.selectP.BackColor = System.Drawing.Color.DarkSlateGray;
            this.selectP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.selectP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectP.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectP.ForeColor = System.Drawing.Color.White;
            this.selectP.Location = new System.Drawing.Point(6, 39);
            this.selectP.Name = "selectP";
            this.selectP.Size = new System.Drawing.Size(137, 52);
            this.selectP.TabIndex = 1;
            this.selectP.Text = "Choisir";
            this.selectP.UseVisualStyleBackColor = false;
            this.selectP.Click += new System.EventHandler(this.selectP_Click);
            // 
            // dgvP
            // 
            this.dgvP.AllowUserToAddRows = false;
            this.dgvP.AllowUserToDeleteRows = false;
            this.dgvP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Cambria", 12F);
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Cambria", 12F);
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvP.DefaultCellStyle = dataGridViewCellStyle22;
            this.dgvP.Location = new System.Drawing.Point(149, 21);
            this.dgvP.Name = "dgvP";
            this.dgvP.ReadOnly = true;
            this.dgvP.RowHeadersVisible = false;
            this.dgvP.RowHeadersWidth = 51;
            this.dgvP.RowTemplate.Height = 24;
            this.dgvP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvP.Size = new System.Drawing.Size(842, 92);
            this.dgvP.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.deleteC);
            this.groupBox3.Controls.Add(this.dgvC);
            this.groupBox3.Controls.Add(this.selectD);
            this.groupBox3.Location = new System.Drawing.Point(15, 313);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1003, 122);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Details Client";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvP);
            this.groupBox1.Controls.Add(this.selectP);
            this.groupBox1.Location = new System.Drawing.Point(15, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1000, 122);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details Parcelle";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.dgvD);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Location = new System.Drawing.Point(15, 441);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1003, 122);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Details Demarcheur";
            // 
            // dgvD
            // 
            this.dgvD.AllowUserToAddRows = false;
            this.dgvD.AllowUserToDeleteRows = false;
            this.dgvD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Cambria", 12F);
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.dgvD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Cambria", 12F);
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvD.DefaultCellStyle = dataGridViewCellStyle24;
            this.dgvD.Location = new System.Drawing.Point(155, 21);
            this.dgvD.Name = "dgvD";
            this.dgvD.ReadOnly = true;
            this.dgvD.RowHeadersVisible = false;
            this.dgvD.RowHeadersWidth = 51;
            this.dgvD.RowTemplate.Height = 24;
            this.dgvD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvD.Size = new System.Drawing.Size(842, 92);
            this.dgvD.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 44);
            this.button1.TabIndex = 1;
            this.button1.Text = "Choisir";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.panel1.Controls.Add(this.label10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1033, 47);
            this.panel1.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(350, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(242, 31);
            this.label10.TabIndex = 0;
            this.label10.Text = "Details de la Mutation";
            // 
            // deleteC
            // 
            this.deleteC.BackColor = System.Drawing.Color.IndianRed;
            this.deleteC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteC.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteC.ForeColor = System.Drawing.Color.White;
            this.deleteC.Location = new System.Drawing.Point(12, 69);
            this.deleteC.Name = "deleteC";
            this.deleteC.Size = new System.Drawing.Size(137, 44);
            this.deleteC.TabIndex = 3;
            this.deleteC.Text = "Supprimer";
            this.deleteC.UseVisualStyleBackColor = false;
            this.deleteC.Click += new System.EventHandler(this.deleteC_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.IndianRed;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(12, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 44);
            this.button2.TabIndex = 4;
            this.button2.Text = "Supprimer";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Mutation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 800);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Mutation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mutation";
            this.Load += new System.EventHandler(this.Mutation_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvO)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvP)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvD)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.ComboBox usage;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox rest;
        private System.Windows.Forms.TextBox vsmt;
        private System.Windows.Forms.TextBox prix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private DGV dgvC;
        private System.Windows.Forms.Button selectD;
        private DGV dgvO;
        private System.Windows.Forms.Button selectC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button selectP;
        private DGV dgvP;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private DGV dgvD;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button deleteC;
        private System.Windows.Forms.Button button2;
    }
}