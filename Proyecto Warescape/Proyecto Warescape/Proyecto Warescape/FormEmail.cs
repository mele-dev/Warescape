﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;

namespace Proyecto_Warescape
{
    public partial class FormEmail : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost; Database=warescapesrl; Uid=Admin; Pwd=warescape;");
        public FormEmail()
        {
            InitializeComponent();
            pnl_cambiar_pass.Hide();
            txt_pass1.PasswordChar = '*';
            txt_pass2.PasswordChar = '*';
        }
        

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.No;
            {
                Email enviarmail = new Email();
                int num = enviarmail.Enviar(txt_receptor.Text);
                int resultado = 0;
                if (num != 0)
                {
                    try
                    {
                        resultado = int.Parse(Interaction.InputBox("Ingrese el digito", "Verificacion"));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Caracteres no permitidos, ingrese NUMEROS.");
                        result = MessageBox.Show("Debido al error, ¿Desea volver a enviar el correo?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    }
                    if (num == resultado)
                    {
                        MessageBox.Show("Los numeros coinciden");
                        result = DialogResult.No;
                        pnl_cambiar_pass.Show();
                    }
                }
            }
        }

        private void btn_cambiar_Click(object sender, EventArgs e)
        {
            if(txt_pass1.Text != txt_pass2.Text)
            {
                MessageBox.Show("Ambas contraseñas deben ser iguales.", "Ingrese nuevamente");
            }
            else
            {
                string usuario = cmb_usuario.Text;
                con.Open();
                MySqlCommand eliminarusuario = new MySqlCommand("delete from usuario where usuario='"+usuario+"'", con);
                eliminarusuario.ExecuteNonQuery();
                con.Close();
                con.Open();
                MySqlCommand crearusuario = new MySqlCommand("insert into usuario values ('" + usuario + "', SHA1('" + txt_pass1.Text + "'))", con);
                crearusuario.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Se ha cambiado la contraseña correctamente", "Accion realizada con exito");
                Form login = new Login();
                this.Hide();
                
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txt_pass1.Text.Equals("") || txt_pass2.Equals("") || txt_receptor.Text.Equals("")) 
            {
                
                MessageBox.Show("Ingrese todo los parametros");
            }
            else
            {

                    if (txt_pass1.Text != txt_pass2.Text)
                    {
                        MessageBox.Show("Ambas contraseñas deben ser iguales.", "Ingrese nuevamente");
                    }
                    else
                    {
                        string usuario = cmb_usuario.Text;
                        con.Open();
                        MySqlCommand eliminarusuario = new MySqlCommand("delete from usuario where usuario='" + usuario + "'", con);
                        eliminarusuario.ExecuteNonQuery();
                        con.Close();
                        con.Open();
                        MySqlCommand crearusuario = new MySqlCommand("insert into usuario values ('" + usuario + "', SHA1('" + txt_pass1.Text + "'))", con);
                        crearusuario.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Se ha cambiado la contraseña correctamente", "Accion realizada con exito");
                        Form login = new Login();
                        this.Hide();
                    }
            }
    }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.No;
                {
                    Email enviarmail = new Email();
                    int num = enviarmail.Enviar(txt_receptor.Text);
                    int resultado = 0;
                    if (num != 0)
                    {
                        try
                        {
                            resultado = int.Parse(Interaction.InputBox("Ingrese el digito", "Verificacion"));
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Caracteres no permitidos, ingrese NUMEROS.");
                            result = MessageBox.Show("Debido al error, ¿Desea volver a enviar el correo?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        }
                        if (num == resultado)
                        {
                            MessageBox.Show("Los numeros coinciden");
                            result = DialogResult.No;
                            pnl_cambiar_pass.Show();
                        }
                    }
                }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.No;
            {
                Email enviarmail = new Email();
                int num = enviarmail.Enviar(txt_receptor.Text);
                int resultado = 0;
                if (num != 0)
                {
                    try
                    {
                        resultado = int.Parse(Interaction.InputBox("Ingrese el digito", "Verificacion"));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Caracteres no permitidos, ingrese NUMEROS.");
                        result = MessageBox.Show("Debido al error, ¿Desea volver a enviar el correo?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    }
                    if (num == resultado)
                    {
                        MessageBox.Show("Los numeros coinciden");
                        result = DialogResult.No;
                        pnl_cambiar_pass.Show();
                    }
                }
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.No;
            {
                Email enviarmail = new Email();
                int num = enviarmail.Enviar(txt_receptor.Text.Trim() + lbl_gmail.Text);
                int resultado = 0;
                if (num != 0)
                {
                    try
                    {
                        resultado = int.Parse(Interaction.InputBox("Ingrese el digito", "Verificacion"));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Caracteres no permitidos, ingrese NUMEROS.");
                        result = MessageBox.Show("Debido al error, ¿Desea volver a enviar el correo?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    }
                    if (num == resultado)
                    {
                        MessageBox.Show("Los numeros coinciden");
                        result = DialogResult.No;
                        pnl_cambiar_pass.Show();
                    }
                }
            }
        }

        

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void FormEmail_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void FormEmail_Load(object sender, EventArgs e)
        {

        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (txt_pass1.Text.Equals("")  || txt_pass2.Text.Equals("") || txt_receptor.Text.Equals("")) 
            {

                MessageBox.Show("Ingrese todo los parametros");
            }
            else
            {

                if (txt_pass1.Text != txt_pass2.Text)
                {
                    MessageBox.Show("Ambas contraseñas deben ser iguales.", "Ingrese nuevamente");
                }
                else
                {
                    string usuario = cmb_usuario.Text;
                    con.Open();
                    MySqlCommand eliminarusuario = new MySqlCommand("delete from usuario where usuario='" + usuario + "'", con);
                    eliminarusuario.ExecuteNonQuery();
                    con.Close();
                    con.Open();
                    MySqlCommand crearusuario = new MySqlCommand("insert into usuario values ('" + usuario + "', SHA1('" + txt_pass1.Text + "'))", con);
                    crearusuario.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Se ha cambiado la contraseña correctamente", "Accion realizada con exito");
                    Form login = new Login();
                    this.Hide();
                }
            }
        }
    }
}
