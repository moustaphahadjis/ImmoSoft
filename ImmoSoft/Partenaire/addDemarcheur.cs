using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmoSoft
{
    public partial class addDemarcheur : Form
    {
        bool modify;
        string id;
        public addDemarcheur()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            modify = false;
        }
        public addDemarcheur(string ID)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            modify = true;
            id=ID;
            DB.demarcheur client = new DB.demarcheur();
            DataRow row = client.refresh(id).Rows[0];
            nom.Text=row["nom"].ToString();
            prenom.Text=row["prenom"].ToString();
            piece.Text=row["piece"].ToString();
            numero.Text=row["numero"].ToString();
            contact.Text=row["contact"].ToString();
            addresse.Text=row["addresse"].ToString();
            nom.Text=row["nom"].ToString();
            profession.Text=row["profession"].ToString();
            matrimonial.Text=row["matrimonial"].ToString();
            string[] date = row["delivrance"].ToString().Split('/');
            delivrance.Value=new DateTime(Int32.Parse(
                date[2]), Int32.Parse(date[0]), Int32.Parse(date[1]));
            if (row["image"]!=DBNull.Value)
                picture.Image=Image.FromStream(new MemoryStream((byte[])row["image"]));
        }
        public addDemarcheur(string ID, bool check)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            modify = true;
            id=ID;
            DB.demarcheur demarcheur = new DB.demarcheur();
            DataRow row = demarcheur.refresh(id).Rows[0];
            nom.Text=row["nom"].ToString();
            prenom.Text=row["prenom"].ToString();
            piece.Text=row["piece"].ToString();
            numero.Text=row["numero"].ToString();
            contact.Text=row["contact"].ToString();
            addresse.Text=row["addresse"].ToString();
            nom.Text=row["nom"].ToString();
            profession.Text=row["profession"].ToString();
            matrimonial.Text=row["matrimonial"].ToString();
            string[] date = row["delivrance"].ToString().Split('/');
            delivrance.Value=new DateTime(Int32.Parse(
                date[2]), Int32.Parse(date[0]), Int32.Parse(date[1]));
            if (row["image"]!=DBNull.Value)
                picture.Image=Image.FromStream(new MemoryStream((byte[])row["image"]));


            nom.Enabled=false;
            prenom.Enabled=false;
            piece.Enabled=false;
            numero.Enabled=false;
            contact.Enabled=false;
            addresse.Enabled=false;
            button1.Enabled=false;
            //button3.Enabled=false;

            button1.Visible=false;
            //button3.Visible=false;

            button4.Enabled=false;
            button4.Visible=false;
            button5.Visible=false;
            button5.Enabled=false;
        }
        void add()
        {
            DB.demarcheur demarcheur = new DB.demarcheur();
            DB.common com = new DB.common();

            if (!string.IsNullOrEmpty(nom.Text)&&
                !string.IsNullOrEmpty(prenom.Text)&&
                !string.IsNullOrEmpty(contact.Text))
                if (com.isPhone(contact.Text.Trim()))
                {
                    MemoryStream ms = new MemoryStream();
                    byte[] data = null;
                    if (picture.Image!=null)
                    {
                        picture.Image.Save(ms, ImageFormat.Jpeg);
                        data=ms.ToArray();
                    }
                    ms.Close();

                    if (demarcheur.add(data,nom.Text.TrimStart().TrimEnd().ToUpper(),
                        prenom.Text.TrimStart().TrimEnd().ToUpper(),
                        piece.Text,
                        numero.Text.TrimStart().TrimEnd().ToUpper(),
                        delivrance.Text, profession.Text,
                        contact.Text.Trim().ToUpper(),
                        addresse.Text.TrimStart().TrimEnd().ToUpper(),matrimonial.Text))
                    {
                        MessageBox.Show("Nouveau client ajouté avec succès!");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez bien verifier le numero de telephone!");
                }
        }
        void update()
        {
            DB.demarcheur demarcheur = new DB.demarcheur();
            DB.common com = new DB.common();

            if (!string.IsNullOrEmpty(nom.Text)&&
                !string.IsNullOrEmpty(prenom.Text)&&
                !string.IsNullOrEmpty(contact.Text))
            {
                if (com.isPhone(contact.Text.Trim()))
                {
                    MemoryStream ms = new MemoryStream();
                    byte[] data = null;
                    if (picture.Image!=null)
                    {
                        picture.Image.Save(ms, ImageFormat.Jpeg);
                        data=ms.ToArray();
                    }
                    ms.Close();

                    if (demarcheur.update(data,id, nom.Text.TrimStart().TrimEnd().ToUpper(),
                        prenom.Text.TrimStart().TrimEnd().ToUpper(),
                        piece.Text,
                        numero.Text.TrimStart().TrimEnd().ToUpper(),
                        delivrance.Text, profession.Text,
                        contact.Text.Trim().ToUpper(),
                        addresse.Text.TrimStart().TrimEnd().ToUpper(),
                        matrimonial.Text))
                    {
                        MessageBox.Show("Client mis à jour avec succès!");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez bien verifier le numero de telephone!");
                }
            }
        }
       

        private void addDemarcheur_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (modify)
                update();
            else
                add();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Camera cam = new Camera();
            cam.FormClosing+=(s, a) =>
            {
                if (cam.taken)
                    picture.Image=cam.picture.Image;
            };
            cam.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (dialog.FileName.EndsWith(".jpg") ||
                    dialog.FileName.EndsWith(".jpeg") ||
                    dialog.FileName.EndsWith(".png"))
                {
                    picture.Image=Image.FromFile(dialog.FileName);
                }
                else
                {
                    throw new FileLoadException("Erreur de type de fichier");
                }

            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void picture_DoubleClick(object sender, EventArgs e)
        {

            if (picture.Image!=null)
            {
                Image img = picture.Image;
                string path = System.IO.Path.GetTempPath()+@"\"+DateTime.Now.ToString("dd mm ss")+".jpeg";
                img.Save(path, ImageFormat.Jpeg);
                Process.Start(path);
            }
        }
    }
}
