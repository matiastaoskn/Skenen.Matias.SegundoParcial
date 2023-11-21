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
        public Animal.Producto? comida;
        bool esActualizacion = false;
        int id;
        public ProductoIngreso()
        {
            InitializeComponent();
            TipoAnimal = "Producto";
        }

        //Chekear esto
        public ProductoIngreso(Producto comida) : this()
        {
            esActualizacion = true;

            this.textBox1.Text = comida.nombre?.ToString();

            try
            {
                conexion.Conectar();
                string consulta = $"SELECT ID FROM ANIMALES WHERE NOMBRE = '{textBox1.Text}'";
                SqlCommand cmd2 = new SqlCommand(consulta, conexion.Conectar());

                id = (int)cmd2.ExecuteScalar();

                MessageBox.Show($"{id}");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }
        /// <summary>
        /// Agrega un nuevo registro de Producto en la base de datos o actualiza uno existente.
        /// </summary>
        public void agregarRegistro()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.textBox1.Text) &&
                    string.IsNullOrWhiteSpace(this.textBox2.Text));
                {
                }
            }
            catch (CamposVaciosException)
            {
                MessageBox.Show($"Campos vacios", "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new CamposVaciosException();
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
            string tamaño = this.textBox2.Text;
            if (string.IsNullOrWhiteSpace(tamaño))
            {
                MessageBox.Show("El tamaño no puede estar en blanco");
                return;
            }
            if (int.TryParse(tamaño, out _))
            {
                MessageBox.Show("El tamaño no puede ser un número");
                return;
            }

            if (esActualizacion == false)
            {
                try
                {
                    string insertar = "INSERT INTO ANIMALES(TIPO,NOMBRE,EDAD,RAZA, ALIMENTACION, VIDAS, PESO, TAMAÑO, ENTRENAMIENTO, HABITAD, COMPORTAMIENTO)VALUES(@TIPO,@NOMBRE,@EDAD,@RAZA,@ALIMENTACION,@VIDAS,@PESO,@TAMAÑO,@ENTRENAMIENTO,@HABITAD,@COMPORTAMIENTO)";
                    SqlCommand cmd = new SqlCommand(insertar, conexion.Conectar());
                    cmd.Parameters.AddWithValue("@TIPO", "Producto");
                    cmd.Parameters.AddWithValue("@NOMBRE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@TAMAÑO", textBox2.Text);

                    cmd.Parameters.AddWithValue("@EDAD", DBNull.Value);
                    cmd.Parameters.AddWithValue("@RAZA", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ALIMENTACION", DBNull.Value);
                    cmd.Parameters.AddWithValue("@HABITAD", DBNull.Value);
                    cmd.Parameters.AddWithValue("@COMPORTAMIENTO", DBNull.Value);
                    cmd.Parameters.AddWithValue("@VIDAS", DBNull.Value);
                    cmd.Parameters.AddWithValue("@PESO", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ENTRENAMIENTO", DBNull.Value);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("DATOS INGRESADOS");

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error SQL: {ex.Message}");
                }

                this.comida = new Producto(textBox1.Text, "grande");
            }
            else
            {
                try
                {
                    conexion.Conectar();  // Asumiendo que Conectar() abre la conexión
                    string actualizar = "UPDATE ANIMALES SET NOMBRE=@NOMBRE, TAMAÑO=@TAMAÑO WHERE ID=@ID";

                    using (SqlCommand cmd = new SqlCommand(actualizar, conexion.Conectar()))
                    {
                        cmd.Parameters.AddWithValue("@ID", id); // Usar el nombre original en la condición WHERE
                        cmd.Parameters.AddWithValue("@NOMBRE", textBox1.Text);
                        cmd.Parameters.AddWithValue("@TAMAÑO", textBox2.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar datos: " + ex.Message);
                }
            }
        }

        
        /// <summary>
        /// Evento que se dispara al hacer clic en el botón de agregar.
        /// Agrega el registro y cierra el formulario.
        /// </summary>
        private void agregarBoton(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
            agregarRegistro();
            this.Close();
        }
        /// <summary>
        /// Evento que se dispara al hacer clic en el botón de cancelar.
        /// Cierra el formulario sin agregar o actualizar el registro.
        /// </summary>
        private void CancelarBoton(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Determina si el objeto actual es igual a otro objeto.
        /// La comparación se realiza mediante las propiedades relevantes de la clase.
        /// </summary>
        /// <param name="obj">El objeto a comparar con el objeto actual.</param>
        /// <returns>
        /// True si el objeto actual es igual al parámetro obj; de lo contrario, False.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ProductoIngreso other = (ProductoIngreso)obj;

            // Comparar las propiedades relevantes
            return
                this.textBox1.Text == other.textBox1.Text &&
                this.textBox2.Text == other.textBox2.Text;
        }

    }
}
