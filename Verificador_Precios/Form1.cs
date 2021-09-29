using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Verificador_Precios
{
    public partial class Form1 : Form
    {
        private String codigo = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(this.Width / 2 - pictureBox1.Width / 2, 250);
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, pictureBox2.Height + 300);
            label3.Location = new Point(this.Width / 2 - label1.Width / 2, pictureBox2.Height + 330);
            pictureBox2.Location = new Point(this.Width / 2 - pictureBox2.Width / 2, this.Height / 2);
            pictureBox3.Visible = false;

        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //MessageBox.Show("vamos a buscar el producto "+codigo);
                try
                {
                    MySqlConnection servidor;
                    servidor = new MySqlConnection("server = 127.0.0.1; user = root; database = productos_usuarios; SSL Mode = None; ");
                    servidor.Open();
                    string query = "SELECT Nombre_del_producto, Precio, Imagen_producto FROM productos WHERE id =" + codigo + ";";
                    //MessageBox.Show(query);
                    MySqlCommand consulta;
                    consulta = new MySqlCommand(query, servidor);
                    MySqlDataReader resultado = consulta.ExecuteReader();
                    if (resultado.HasRows)
                    {
                        resultado.Read();
                        //MessageBox.Show(resultado.GetString(1));
                        pictureBox1.Visible = false;
                        pictureBox2.Visible = false;
                        pictureBox3.Visible = true;
                        label1.Text = resultado.GetString(0);
                        label3.Text = Environment.NewLine+"$"+ resultado.GetString(1);
                        pictureBox3.ImageLocation = resultado.GetString(2);
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                        label3.ForeColor = Color.Red;
                        label1.Left = (this.Width - label1.Width) / 2;
                        label3.Left = (this.Width - label3.Width) / 2;
                    }
                    else
                    {
                        MessageBox.Show("Llame al supervisor el producto no fue encontrado");
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.ToString(), "Titulo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                codigo = "";
            }
            else
            {
                codigo += e.KeyChar;
            }
        }

 
    }
}
