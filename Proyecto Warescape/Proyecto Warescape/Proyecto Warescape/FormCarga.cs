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
    public partial class FormCarga : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost; Database=warescapesrl; Uid=root; Pwd=;");

        public FormCarga()
        {
            InitializeComponent();
        }
        int comienzo = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            comienzo += 4;
            progreso.Value = comienzo;
            lblporcentaje.Text = comienzo + "%";
            if (progreso.Value == 100)
            {

                progreso.Value = 0;
                timer1.Stop();
                Login log = new Login();
                log.Show();
                this.Hide();
              
                

            }
        }

        private void carga_Load(object sender, EventArgs e)
        {
            checkearBD(con);
            timer1.Start();
        }

        public void checkearBD(MySqlConnection con)
        {
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al chequear base de datos! (CHECKEAR WAMP SERVER) = " + ex.Message);
                throw;
            }
        }
    }
}
