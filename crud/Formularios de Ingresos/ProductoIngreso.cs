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
        public Animal.Comida? comida;
        bool esActualizacion = false;
        int id;
        public ProductoIngreso()
        {
            InitializeComponent();
            TipoAnimal = "Producto";
        }

        //Chekear esto
        public ProductoIngreso(Comida comida) : this()
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

        public void agregarRegistro()
        {
            this.comida = new Comida(textBox1.Text, "grande");
            if (esActualizacion == false)
            {
                try
                {
                    string insertar = "INSERT INTO ANIMALES(TIPO,NOMBRE,EDAD,RAZA, ALIMENTACION, VIDAS, PESO, TAMAÑO, ENTRENAMIENTO, HABITAD, COMPORTAMIENTO)VALUES(@TIPO,@NOMBRE,@EDAD,@RAZA,@ALIMENTACION,@VIDAS,@PESO,@TAMAÑO,@ENTRENAMIENTO,@HABITAD,@COMPORTAMIENTO)";
                    SqlCommand cmd = new SqlCommand(insertar, conexion.Conectar());
                    cmd.Parameters.AddWithValue("@TIPO", "Producto");
                    cmd.Parameters.AddWithValue("@NOMBRE", textBox1.Text);

                    cmd.Parameters.AddWithValue("@EDAD", DBNull.Value);
                    cmd.Parameters.AddWithValue("@RAZA", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ALIMENTACION", DBNull.Value);
                    cmd.Parameters.AddWithValue("@HABITAD", DBNull.Value);
                    cmd.Parameters.AddWithValue("@COMPORTAMIENTO", DBNull.Value);
                    cmd.Parameters.AddWithValue("@VIDAS", DBNull.Value);
                    cmd.Parameters.AddWithValue("@PESO", DBNull.Value);
                    cmd.Parameters.AddWithValue("@TAMAÑO", DBNull.Value);
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

            }
            else
            {
                try
                {
                    conexion.Conectar();  // Asumiendo que Conectar() abre la conexión
                    string actualizar = "UPDATE ANIMALES SET NOMBRE=@NOMBRE WHERE ID=@ID";

                    using (SqlCommand cmd = new SqlCommand(actualizar, conexion.Conectar()))
                    {
                        cmd.Parameters.AddWithValue("@ID", id); // Usar el nombre original en la condición WHERE
                        cmd.Parameters.AddWithValue("@NOMBRE", textBox1.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar datos: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            this.DialogResult = DialogResult.OK;
            agregarRegistro();
            this.Close();
        }
    }
}
