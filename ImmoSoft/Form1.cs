using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ImmoSoft
{
    public partial class Form1 : Form
    {
        Form activeForm = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void changeForm(Form newForm)
        {
            if (activeForm!=null)
            {
                activeForm.Close();
            }
            activeForm = newForm;
            activeForm.FormBorderStyle = FormBorderStyle.None;
            activeForm.BackColor = mainPanel.BackColor;
            activeForm.TopLevel = false;
            activeForm.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(activeForm);
            activeForm.Show();
        }
        void showTitle(Button btn)
        {
            title.Text = btn.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form tmp = new Stock();
            changeForm(tmp);
            showTitle(button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form tmp = new enVente();
            changeForm(tmp);
            showTitle(button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form tmp = new Vendues();
            changeForm(tmp);
            showTitle(button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form tmp = new historique();
            changeForm(tmp);
            showTitle(button4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form tmp = new Clients();
            changeForm(tmp);
            showTitle(button5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form tmp = new Demarcheurs();
            changeForm(tmp);
            showTitle(button6);
        }
    }
}
