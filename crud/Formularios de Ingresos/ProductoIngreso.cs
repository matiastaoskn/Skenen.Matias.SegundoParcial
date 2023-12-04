using Animal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindFormCrud;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CrudVeterinaria
{
    public partial class ProductoIngreso : AnimalIngresoForm
    {
        public Animal.Producto? Producto;
        bool esActualizacion = false;
        int id;

        public ProductoIngreso()
        {
            InitializeComponent();
            TipoAnimal = "Producto";
        }

        public ProductoIngreso(Producto producto) : this()
        {
            esActualizacion = true;

            this.textBox1.Text = producto.nombre?.ToString();
            this.textBox2.Text = producto.tamaño?.ToString();

            try
            {
                conexcionBaseDatos.Conectar();
                string consulta = $"SELECT ID FROM ANIMALES WHERE NOMBRE = '{producto.nombre}'";
                SqlCommand cmd2 = new SqlCommand(consulta, conexcionBaseDatos.Conectar());
                id = (int)cmd2.ExecuteScalar();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void agregarRegistro()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.textBox1.Text) &&
                    string.IsNullOrWhiteSpace(this.textBox2.Text))
                {
                    throw new CamposVaciosException();
                }
            }
            catch (CamposVaciosException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string nombre = this.textBox1.Text;
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre no puede estar en blanco");
                return;
            }
            if (int.TryParse(nombre, out _))
            {
                MessageBox.Show("El nombre no puede ser un número");
                return;
            }

            string tamaño = this.textBox2.Text; ;
            if (int.TryParse(tamaño, out _))
            {
                MessageBox.Show("El tamaño no puede ser un número");
                return;
            }

            this.Producto = new Producto(textBox1.Text, textBox2.Text);

            if (!esActualizacion)
            {
                try
                {
                    string insertar = "INSERT INTO ANIMALES(TIPO,NOMBRE,TAMAÑO)VALUES(@TIPO,@NOMBRE,@TAMAÑO)";
                    using (SqlCommand cmd = new SqlCommand(insertar, conexcionBaseDatos.Conectar()))
                    {
                        cmd.Parameters.AddWithValue("@TIPO", "Producto");
                        cmd.Parameters.AddWithValue("@NOMBRE", textBox1.Text);
                        cmd.Parameters.AddWithValue("@TAMAÑO", textBox2.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("DATOS INGRESADOS");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error SQL: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    string actualizar = "UPDATE ANIMALES SET NOMBRE=@NOMBRE, TAMAÑO=@TAMAÑO WHERE ID=@ID";
                    using (SqlCommand cmd = new SqlCommand(actualizar, conexcionBaseDatos.Conectar()))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@NOMBRE", textBox1.Text);
                        cmd.Parameters.AddWithValue("@TAMAÑO", textBox2.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("DATO ACTUALIZADO");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar datos: " + ex.Message);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void agregarBoton(object sender, EventArgs e)
        {
            agregarRegistro();
        }

        private void CancelarBoton(object sender, EventArgs e)
        {
            this.Close();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ProductoIngreso other = (ProductoIngreso)obj;
            return
                this.textBox1.Text == other.textBox1.Text &&
                this.textBox2.Text == other.textBox2.Text;
        }
    }
}
