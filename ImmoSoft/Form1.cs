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
            this.FormBorderStyle = FormBorderStyle.Sizable;
            if (Properties.Settings.Default.admin.ToLower()!="admin")

            {
                button14.Enabled=false;
                button14.Visible=false; 
            }
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
            if( mainPanel.Contains(madeby))
                mainPanel.Controls.Remove(madeby);
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
            title.Image=btn.Image;
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

        private void button8_Click(object sender, EventArgs e)
        {
            checkPassword check = new checkPassword(Properties.Settings.Default.id);
            bool r = false;
            DataRow row;
            check.FormClosing+=(s, a) => 
            {
                r=check.isPassword;
            };
            check.ShowDialog();
            if (r)
            {
                addUser user = new addUser(Properties.Settings.Default.id);
                user.ShowDialog();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voules-vous vraiment quitter l'application?", "Fermer la fenetre",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form tmp = new Site();
            changeForm(tmp);
            showTitle(button9);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form tmp = new Champs();
            changeForm(tmp);
            showTitle(button13);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form tmp = new enMutation();
            changeForm(tmp);
            showTitle(button11);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form tmp = new Mutees();
            changeForm(tmp);
            showTitle(button12);
        }


        private void button14_Click(object sender, EventArgs e)
        {
            Form tmp = new Attribution();
            changeForm(tmp);
            showTitle(button14);
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            int x, y;
            x=panel1.Size.Width/2- title.Width/2;
            title.Location= new Point(x,title.Location.Y);
        }

        private void menuPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainPanel_SizeChanged(object sender, EventArgs e)
        {
            int x, y;
            x=mainPanel.Size.Width/2- madeby.Width/2;

            y=mainPanel.Size.Height/4;
            madeby.Location= new Point(x, y);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Form tmp = new Files();
            changeForm(tmp);
            showTitle(button7);
        }
    }
}
