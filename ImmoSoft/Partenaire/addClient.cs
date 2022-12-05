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
    public partial class addClient : Form
    {
        bool modify;
        string id; 
        public addClient()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            delivrance.Format = DateTimePickerFormat.Custom;
            delivrance.CustomFormat = "dd/MM/yyyy";
            modify = false;
        }
        public addClient(string ID)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            delivrance.Format = DateTimePickerFormat.Custom;
            delivrance.CustomFormat = "dd/MM/yyyy";
            modify = true;
            id=ID;
            DB.client client = new DB.client();
            DataRow row = client.refresh(id).Rows[0];
            nom.Text=row["nom"].ToString();
            prenom.Text=row["prenom"].ToString();
            piece.Text=row["piece"].ToString();
            numero.Text=row["numero"].ToString();
            contact.Text=row["contact"].ToString();
            addresse.Text=row["addresse"].ToString();
            profession.Text=row["profession"].ToString();
            matrimonial.Text=row["matrimonial"].ToString();
            string[] date = row["delivrance"].ToString().Split('/');
            delivrance.Value=new DateTime(Int32.Parse(
                date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));
            if (row["image"]!=DBNull.Value)
            picture.Image=Image.FromStream(new MemoryStream((byte[])row["image"]));
        }
        public addClient(string ID, bool check)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            delivrance.Format = DateTimePickerFormat.Custom;
            delivrance.CustomFormat = "dd/MM/yyyy";
            modify = true;
            id=ID;
            DB.client client = new DB.client();
            DataRow row = client.refresh(id).Rows[0];
            nom.Text=row["nom"].ToString();
            prenom.Text=row["prenom"].ToString();
            piece.Text=row["piece"].ToString();
            numero.Text=row["numero"].ToString();
            contact.Text=row["contact"].ToString();
            addresse.Text=row["addresse"].ToString();
            profession.Text=row["profession"].ToString();
            matrimonial.Text=row["matrimonial"].ToString();
            string[] date = row["delivrance"].ToString().Split('/');
            delivrance.Value=new DateTime(Int32.Parse(
                date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));
            if (row["image"]!=DBNull.Value)
                picture.Image=Image.FromStream(new MemoryStream((byte[])row["image"]));

            nom.Enabled=false;
            prenom.Enabled=false;
            piece.Enabled=false;
            numero.Enabled=false;
            contact.Enabled=false;
            addresse.Enabled=false;
            profession.Enabled=false;
            delivrance.Enabled=false;
            button1.Enabled=false;
            matrimonial.Enabled=false;

            button4.Enabled=false;
            button4.Visible=false;
            button5.Visible=false;
            button5.Enabled=false;
           // button3.Enabled=false;

            button1.Visible=false;
            //button3.Visible=false;
        }
        void add()
        {
            DB.client client = new DB.client();
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

                    if (client.add(data,nom.Text.TrimStart().TrimEnd().ToUpper(),
                        prenom.Text.TrimStart().TrimEnd().ToUpper(),
                        piece.Text,
                        numero.Text.TrimStart().TrimEnd().ToUpper(),
                        delivrance.Value.ToString("dd/MM/yyyy"),
                        profession.Text.TrimStart().TrimEnd().ToUpper(),
                        matrimonial.Text.TrimStart().TrimEnd().ToUpper(),
                        contact.Text.Trim().ToUpper(),
                        addresse.Text.TrimStart().TrimEnd().ToUpper()))
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
            DB.client client = new DB.client();
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

                    if (client.update(data, id, nom.Text.TrimStart().TrimEnd().ToUpper(),
                        prenom.Text.TrimStart().TrimEnd().ToUpper(),
                        piece.Text,
                        numero.Text.TrimStart().TrimEnd().ToUpper(),
                        delivrance.Value.ToString("dd/MM/yyyy"),
                        profession.Text.TrimStart().TrimEnd().ToUpper(),
                        matrimonial.Text.TrimStart().TrimEnd().ToUpper(),
                        contact.Text.Trim().ToUpper(),
                        addresse.Text.TrimStart().TrimEnd().ToUpper()))
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (modify)
                update();
            else
                add();
        }

        private void addClient_Load(object sender, EventArgs e)
        {

        }

        private void delivrance_ValueChanged(object sender, EventArgs e)
        {

        }

        private void piece_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void profession_TextChanged(object sender, EventArgs e)
        {

        }

        private void numero_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
