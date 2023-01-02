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
using System.Text.RegularExpressions;

namespace Proyecto_Warescape
{
    public partial class FormFinanzas : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost; Database=warescapesrl; Uid=root; Pwd=;");


        public FormFinanzas()
        {
            InitializeComponent();
        }

        private void panelfinanzas_Paint(object sender, PaintEventArgs e)
        {
            this.Hide();
            Form finanzas = new FormFinanzas();
            finanzas.Show();

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            VentanasPrincipal ventanas = new VentanasPrincipal();
            ventanas.Show();

        }

        private void finanzas_Load(object sender, EventArgs e)
        {
            actualizar_ventas();
            dgv_lista.ReadOnly = true;
            obtenerMarketing(con);
            cargarLibrosFinanzas();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            txt_boleta.Text = txt_boleta.Text.Replace(" ", string.Empty);
            txt_cantidad.Text = txt_cantidad.Text.Replace(" ", string.Empty);
            txt_precio.Text = txt_precio.Text.Replace(" ", string.Empty);
            if (txt_boleta.Text.Equals("") || txt_precio.Text.Equals("") || txt_cantidad.Text.Equals("") || cmb_libros.Text.Equals(""))
            {
                MessageBox.Show("Ingresar todos los parametros");
            }
            else
            {
                int n = dgv_lista.Rows.Add();

                dgv_lista.Rows[n].Cells[0].Value = txt_boleta.Text;
                dgv_lista.Rows[n].Cells[1].Value = fecha_venta.Value.ToString("yyyy-MM-dd");
                dgv_lista.Rows[n].Cells[2].Value = txt_precio.Text;
                dgv_lista.Rows[n].Cells[3].Value = txt_cantidad.Text;
                dgv_lista.Rows[n].Cells[4].Value = cmb_libros.SelectedItem.ToString();
                if (!cmb_publicidad.Text.Equals("")) 
                { 
                dgv_lista.Rows[n].Cells[5].Value = cmb_publicidad.SelectedItem.ToString();
                }

            }
            txt_boleta.Text = "";
            txt_precio.Text = "";
            txt_cantidad.Text = "";
            cmb_libros.Text = "";
            cmb_publicidad.Text = "";

           
        }

        private void Registrar_ventra_Click(object sender, EventArgs e)
        {
            

            if (dgv_lista.Rows.Count == 1)
            {
                MessageBox.Show("Agrege una venta");
            }
            else
            {
                for (int i = 0; i < dgv_lista.Rows.Count - 1; i++)
                {



                    if (dgv_lista.Rows[i].Cells[0].Value.ToString().Trim().Equals("") || dgv_lista.Rows[i].Cells[2].Value.ToString().Trim().Equals("") || dgv_lista.Rows[i].Cells[3].Value.ToString().Trim().Equals(""))
                    {
                        MessageBox.Show("Tienes que ingresar numeros en campo");
                        break;
                    }
                    
                    int n_de_boleta = int.Parse(dgv_lista.Rows[i].Cells[0].Value.ToString().Trim());
                    string fecha = dgv_lista.Rows[i].Cells[1].Value.ToString();
                    long precio = int.Parse(dgv_lista.Rows[i].Cells[2].Value.ToString().Trim());
                    int cantidad_vendida = int.Parse(dgv_lista.Rows[i].Cells[3].Value.ToString().Trim());
                    string libro = dgv_lista.Rows[i].Cells[4].Value.ToString().Trim();
                    string viene="";
                    string id_publi = "";
                    
                    if (dgv_lista.Rows[i].Cells[5].Value != null)
                    {

                        viene = Convert.ToString(dgv_lista.Rows[i].Cells[5].Value).Trim();
                         id_publi = dgv_lista.Rows[i].Cells[5].Value.ToString().Split('-').Last().Trim();
                        
                    }
                   
                   
                    string id_libro = dgv_lista.Rows[i].Cells[4].Value.ToString().Split('-').Last().Trim();  






                    con.Open();
                    string verificar_boleta = "SELECT n_de_boleta from ventas where n_de_boleta =" + n_de_boleta + ";";
                    MySqlCommand comando = new MySqlCommand(verificar_boleta, con);
                    MySqlDataReader reador = comando.ExecuteReader();
                    string comparar = "";

                    while (reador.Read())
                    {
                        string a = reador["n_de_boleta"].ToString();
                        if (a.Equals(n_de_boleta.ToString()))
                        {
                            comparar = "son iguales";


                        }

                    }
                    con.Close();

                    if(comparar.Equals("son iguales"))
                    {

                        con.Open();

                        try
                        {
                            string ingresar_generan = "INSERT INTO generan values(" + n_de_boleta + "," + int.Parse(id_libro) + "," + cantidad_vendida + "," + precio + ")";
                            MySqlCommand query_genera = new MySqlCommand(ingresar_generan, con);
                            query_genera.ExecuteNonQuery();
                            con.Close();
                            con.Open();
                            if (!viene.Equals(""))
                            {
                                string insertar_publicidad = "INSERT INTO se_registran values(" + int.Parse(id_publi) + "," + n_de_boleta + ");";
                                MySqlCommand query_se_registran = new MySqlCommand(insertar_publicidad, con);
                                query_se_registran.ExecuteNonQuery();



                            }
                            con.Close();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("No se puede ingresar datos duplicados");
                            break;
                        }

                        con.Open();
                        string extraer_monto = "SELECT monto from ventas where  n_de_boleta=" + n_de_boleta.ToString() + "; ";
                        MySqlCommand query_extraer = new MySqlCommand(extraer_monto, con);
                        MySqlDataReader monto = query_extraer.ExecuteReader();
                        
                        string b = "";

                        while (monto.Read())
                        {
                             b = monto["monto"].ToString();

                        }
                        con.Close();
                        con.Open();
                        precio = precio * cantidad_vendida;
                        long cuenta = precio + long.Parse(b);
                        string monto_cambiar = "UPDATE ventas set monto =" + cuenta + " where n_de_boleta =" + n_de_boleta + ";";

                        MySqlCommand cambiar_monto = new MySqlCommand(monto_cambiar, con);
                        cambiar_monto.ExecuteNonQuery();
                        con.Close();

                        Services.LibrosService.actualizar_stock_quitar(con,int.Parse(id_libro),cantidad_vendida);
                    }
                    else
                    {
                        con.Open();
                        long monto = precio * cantidad_vendida;
                        string ingresar_venta = "INSERT INTO ventas values("+n_de_boleta+ ",'" + fecha + "'," + monto+");";
                        MySqlCommand query_ingersar_venta = new MySqlCommand(ingresar_venta, con);
                        query_ingersar_venta.ExecuteNonQuery();
                        con.Close();
                        con.Open();
                        string ingresar_generan = "INSERT INTO generan values(" + n_de_boleta + "," + int.Parse(id_libro) + "," + cantidad_vendida+ "," + precio + ")";
                        MySqlCommand query_genera = new MySqlCommand(ingresar_generan, con);
                        query_genera.ExecuteNonQuery();
                        con.Close();
                        con.Open();
                        if (!viene.Equals(""))
                        {
                            string insertar_publicidad = "INSERT INTO se_registran values(" + int.Parse(id_publi) + "," + n_de_boleta + ");";
                            MySqlCommand query_se_registran = new MySqlCommand(insertar_publicidad, con);
                            query_se_registran.ExecuteNonQuery();

                        }
                        con.Close();

                    }
                

                    con.Close();





                }
                dgv_lista.Rows.Clear();
                actualizar_ventas();





            }








        }

        private void dgv_lista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

           
            
                int n = e.RowIndex;
                if (n != -1 && n !=0)
                {
                    dgv_lista.Rows.RemoveAt(n);
                }

            
            

        }
        public void solo_numeros(KeyPressEventArgs e)
        {

            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public void solo_letras(KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;

            }

        }

        private void txt_boleta_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            solo_numeros(e);
        }

        private void txt_precio_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            solo_numeros(e);
        }
        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            solo_numeros(e);
        }

        private void dgv_lista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void actualizar_ventas()
        {
            try
            {
                con.Open();
                MySqlCommand query_mostrar_ventas = new MySqlCommand("select v.n_de_boleta 'N de boleta         ', v.fecha_de_venta 'Fecha', v.monto 'Monto', g.precio 'Precio', g.cantidad_vendida 'Cantidad comprada', l.nombre 'Nombre' from ventas v join generan g on v.n_de_boleta=g.n_de_boleta join libros l on l.id_libro=g.id_libro;", con);
                MySqlDataReader reader_mostrar_ventas = query_mostrar_ventas.ExecuteReader();
                while (reader_mostrar_ventas.Read())
                {
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = query_mostrar_ventas;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgv_ventas.DataSource = tabla;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Error actualizar_ventas = " + ex.Message);
                throw;
            }
        }

        private void Borrar_Click(object sender, EventArgs e)
        {
            
            if (!lbl_n_de_boleta.Text.Equals("")) 
            {
                    DialogResult resul = MessageBox.Show("Seguro que quiere eliminar la venta?", "Eliminar Registro", MessageBoxButtons.YesNo);
                    if (resul == DialogResult.Yes)
                    {
                        long n_de_boleta = long.Parse(lbl_n_de_boleta.Text);
                        string borrar_venta = "DELETE From ventas where n_de_boleta="+n_de_boleta+";";
                        con.Open();
                        MySqlCommand borrar = new MySqlCommand(borrar_venta,con);
                        borrar.ExecuteNonQuery();
                        con.Close();
                    }
                }
            else
            {
                MessageBox.Show("Seleccione una venta para borrar");
            }
            actualizar_ventas();
            
        }

        private void dgv_ventas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = e.RowIndex;
            if (n != -1)
            {
                if (dgv_ventas.CurrentRow.Cells[0].Value.ToString() != "")
                {

                    long a = long.Parse(dgv_ventas.CurrentRow.Cells[0].Value.ToString());
                    lbl_n_de_boleta.Text = (Convert.ToInt32(a)).ToString();
                }

            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Form caja = new FormControl_de_caja();
            caja.Show();
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btn_estadisticas_Click(object sender, EventArgs e)
        {
            FormFinanzas finanzas = new FormFinanzas();
            finanzas.Hide();

            FormEstadisticas_mensuales estadisticas_Mensuales = new FormEstadisticas_mensuales();
            estadisticas_Mensuales.Show();
        }

        private void cmb_libros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_publicidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void fecha_venta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void obtenerMarketing(MySqlConnection con)
        {
            try
            {
                con.Open();
                MySqlCommand obtener_marketing = new MySqlCommand("SELECT nombre,id_publicidad from publicidad", con);
                MySqlDataReader registro_marketing = obtener_marketing.ExecuteReader();
                while (registro_marketing.Read())
                {
                    string nombre = registro_marketing["nombre"].ToString();
                    string id_de_publicidad = registro_marketing["id_publicidad"].ToString();
                    cmb_publicidad.Items.Add(nombre + "-" + id_de_publicidad);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Error obtenerMarketing = " + ex.Message);
                throw;
            }
        }

        public void cargarLibrosFinanzas()
        {
            try
            {
                MySqlCommand query = new MySqlCommand("SELECT nombre,id_libro from libros", con);
                con.Open();
                MySqlDataReader reader = query.ExecuteReader();
                while (reader.Read())
                {
                    string nombre = reader["nombre"].ToString();
                    string id_libro = reader["id_libro"].ToString();
                    cmb_libros.Items.Add(nombre + "-" + id_libro);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Error cargarLibrosFinanzas = " + ex.Message);
                throw;
            }
        }
    }
    }

