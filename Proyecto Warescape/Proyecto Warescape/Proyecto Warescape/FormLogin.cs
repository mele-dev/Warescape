using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Warescape
{
    public partial class Login : Form
    {

        MySqlConnection con = new MySqlConnection("Server=localhost; Database=warescapesrl; Uid=root; Pwd=;");

        string contrasena;
        
        public Login()
        {
            InitializeComponent();
            //crear_con();
        }

        // no se esta usando esto
        private void crear_con()
        {
            try
            {
                con.Open();
                MessageBox.Show("Conectado bro");
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al conectar con la base de datos");
            }
        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            if (campo_usuario == null || campo_usuario.Equals(""))
            {
                MessageBox.Show("Ingrese un nombre de usuario correcto");
                return;
            }
            else if (campo_contra == null || campo_contra.Equals(""))
            {
                MessageBox.Show("Ingrese una contraseña correcta");
                return;
            }
            else 
            {
                try
                {
                    con.Open();
                    MySqlCommand consultaUsuario = new MySqlCommand("Select * from usuario where usuario = '" + campo_usuario.Text + "'", con);

                    MySqlDataReader reader = consultaUsuario.ExecuteReader();

                    while (reader.Read())
                    {
                        contrasena = reader["contrasena"].ToString();
                    }
                    con.Close();
                    con.Open();
                    MySqlCommand hasheo = new MySqlCommand("Select SHA1 ('" + campo_contra.Text + "') hasheo", con);



                    MySqlDataReader reader_hasheo = hasheo.ExecuteReader();

                    string contra_hasheada = "";

                    // validamos que de resultado el query, que no se parte
                    while (reader_hasheo.Read())
                    {
                        contra_hasheada = reader_hasheo["hasheo"].ToString();
                    }


                    con.Close();

                    if (contra_hasheada.Equals(contrasena))
                    {

                        if (campo_usuario.Text.Equals("Empleado"))
                        {
                            this.Hide();
                            Form v1 = new FormVentana_empleado();
                            v1.Show();
                        }
                        else
                        {
                            this.Hide();
                            FormVentana v1 = new FormVentana();
                            v1.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta");
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Error 500: " + ex.Message);
                    throw;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ingreso_Enter(object sender, EventArgs e)
        {
            btn_ingresar_Click(sender, e);
        }

        private void campo_contra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_ingresar_Click(sender, e);
            }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btn_restaurar_Click(object sender, EventArgs e)
        {
            Form restaurar = new FormEmail();
            restaurar.Show();
        }
    }
}
