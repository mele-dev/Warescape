﻿using System;
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
    public partial class FormVentana : Form
    {
        public FormVentana()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            VentanasPrincipal ventana = new VentanasPrincipal();
            ventana.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormFinanzas finanzas = new FormFinanzas();
            finanzas.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AbrirFormInPanel(new VentanasPrincipal());
            pnl_cerrar.BackColor = Color.DarkGray;

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AbrirFormInPanel(new FormFinanzas());
            pnl_cerrar.BackColor = Color.DarkGray;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new FormGraficas());
            pnl_cerrar.BackColor = Color.DarkGray;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new FormMarketing());
            pnl_cerrar.BackColor = Color.DarkGray;
        }

        private void AbrirFormInPanel(object formHijo)
        {
            if (this.panel1.Controls.Count > 0)
                this.panel1.Controls.RemoveAt(0);
            Form fh = formHijo as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(fh);
            this.panel1.Tag = fh;
            fh.Show();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Ventana_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        
        private void ImagenVentana_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            Login ventana_login = new Login();
            ventana_login.Show();


        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            
        }

        private void panel7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ventana_Load(object sender, EventArgs e)
        {

        }
    }
}