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
    public partial class ventana : Form
    {
        public ventana()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

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
            finanzas finanzas = new finanzas();
            finanzas.Show();
        }

        private void ventana_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form ir_libros = new VentanasPrincipal();
            ir_libros.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form ir_finanza = new finanzas();
            ir_finanza.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form ir_marketing = new marketing();
            ir_marketing.Show();
            this.Hide();
        }
    }
}