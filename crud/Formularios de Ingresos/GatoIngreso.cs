using Animal;
using CrudVeterinaria;
using System.Data.SqlClient;

namespace WindFormCrud
{
    public partial class GatoIngreso : AnimalIngresoForm
    {

        public Animales.Animales? animales;
        int id;
        private bool esActualizacion = false;
        public GatoIngreso()
        {
            InitializeComponent();
            TipoAnimal = "Gato";
        }
        public GatoIngreso(Animales.Animales animales, Animales.Gato gato) : this()
        {
            esActualizacion = true;

            this.textBox1.Text = animales.nombre?.ToString();
            this.textBox2.Text = animales.edad.ToString();
            this.textBox4.Text = animales.raza?.ToString();
            this.comboBox1.Text = animales.alimentacion.ToString();
            this.textBox5.Text = gato.Vidas.ToString();
            this.textBox6.Text = gato.Peso.ToString();

            try
            {
                conexcionBaseDatos.Conectar();
                string consulta = $"SELECT ID FROM ANIMALES WHERE NOMBRE = '{textBox1.Text}'";
                SqlCommand cmd2 = new SqlCommand(consulta, conexcionBaseDatos.Conectar());

                id = (int)cmd2.ExecuteScalar();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }
        /// <summary>
        /// Valida los datos ingresados por el usuario en el formulario de ingreso o actualización de Gato.
        /// Si los campos son válidos, crea un nuevo objeto Gato o actualiza uno existente en la base de datos.
        /// </summary>
        private void validarInputs()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.textBox1.Text) &&
                    string.IsNullOrWhiteSpace(this.textBox2.Text) &&
                    string.IsNullOrWhiteSpace(this.textBox4.Text) &&
                    string.IsNullOrWhiteSpace(this.textBox5.Text) &&
                    string.IsNullOrWhiteSpace(this.textBox6.Text) &&
                    string.IsNullOrWhiteSpace(this.comboBox1.Text))
                {
                    throw new CamposVaciosException();
                }
            }
            catch (CamposVaciosException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string tipoDeAnimal = this.TipoAnimal;

            string nombre = this.textBox1.Text;
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("La raza no puede estar en blanco");
                return;
            }
            if (int.TryParse(nombre, out _))
            {
                MessageBox.Show("El nombre no puede ser un número");
                return;
            }

            int edad;
            if (!int.TryParse(this.textBox2.Text, out edad))
            {
                MessageBox.Show("La edad debe ser un número válido.");
                return;
            }

            string raza = this.textBox4.Text;
            if (string.IsNullOrWhiteSpace(raza))
            {
                MessageBox.Show("La raza no puede estar en blanco");
                return;
            }
            if (int.TryParse(raza, out _))
            {
                MessageBox.Show("raza no puede ser un número");
                return;
            }

            Alimentacion alimentacion;
            if (!Enum.TryParse(this.comboBox1.Text, out alimentacion))
            {
                MessageBox.Show("La alimentación no es válida.");
                return;
            }

            int vidas;
            if (!int.TryParse(this.textBox5.Text, out vidas))
            {
                MessageBox.Show("Las vidas deben ser un número válido.");
                return;
            }

            int peso;
            if (!int.TryParse(this.textBox6.Text, out peso))
            {
                MessageBox.Show("El peso debe ser un número válido.");
                return;
            }



            this.animales = new Animales.Gato(peso, vidas, nombre, edad, alimentacion, raza);

            if (esActualizacion == false)
            {
                try
                {
                    string insertar = "INSERT INTO ANIMALES(TIPO,NOMBRE,EDAD,RAZA, ALIMENTACION, VIDAS, PESO, TAMAÑO, ENTRENAMIENTO, HABITAD, COMPORTAMIENTO)VALUES(@TIPO,@NOMBRE,@EDAD,@RAZA,@ALIMENTACION,@VIDAS,@PESO,@TAMAÑO,@ENTRENAMIENTO,@HABITAD,@COMPORTAMIENTO)";
                    SqlCommand cmd = new SqlCommand(insertar, conexcionBaseDatos.Conectar());
                    cmd.Parameters.AddWithValue("@TIPO", TipoAnimal);
                    cmd.Parameters.AddWithValue("@NOMBRE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@EDAD", textBox2.Text);
                    cmd.Parameters.AddWithValue("@RAZA", textBox4.Text);
                    cmd.Parameters.AddWithValue("@ALIMENTACION", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@VIDAS", textBox5.Text);
                    cmd.Parameters.AddWithValue("@PESO", textBox6.Text);


                    cmd.Parameters.AddWithValue("@TAMAÑO", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ENTRENAMIENTO", DBNull.Value);
                    cmd.Parameters.AddWithValue("@HABITAD", DBNull.Value);
                    cmd.Parameters.AddWithValue("@COMPORTAMIENTO", DBNull.Value);



                    cmd.ExecuteNonQuery();
                    MessageBox.Show("DATOS INGRESADO");

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
                    conexcionBaseDatos.Conectar();  // Asumiendo que Conectar() abre la conexión
                    string actualizar = "UPDATE ANIMALES SET NOMBRE=@NOMBRE, EDAD=@EDAD, RAZA=@RAZA, ALIMENTACION=@ALIMENTACION, VIDAS=@VIDAS, PESO=@PESO WHERE ID=@ID";

                    using (SqlCommand cmd = new SqlCommand(actualizar, conexcionBaseDatos.Conectar()))
                    {
                        cmd.Parameters.AddWithValue("@ID", id); // Usar el nombre original en la condición WHERE
                        cmd.Parameters.AddWithValue("@NOMBRE", textBox1.Text);
                        cmd.Parameters.AddWithValue("@EDAD", textBox2.Text);
                        cmd.Parameters.AddWithValue("@RAZA", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@ALIMENTACION", textBox4.Text);
                        cmd.Parameters.AddWithValue("@VIDAS", textBox5.Text);
                        cmd.Parameters.AddWithValue("@PESO", textBox6.Text);

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
        /// <summary>
        /// Confirma el dialogo de la ventana, guardando los datos del tipo "Gato"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.validarInputs();
        }
        /// <summary>
        /// Cancela el dialogo de la ventana del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Este metodo resetea de fabrica los inputs para simplificar al usuario el ingreso de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetear_Click(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
            }
        }
        /// <summary>
        /// Determina si el objeto actual de tipo ConejoIngreso es igual a otro objeto. La comparación se basa en la igualdad
        /// de las propiedades relevantes de dos instancias de ConejoIngreso, incluyendo textBox1, textBox2, textBox4, textBox5, textBox6 y comboBox1.
        /// </summary>
        /// <param name="obj">El objeto a comparar con el objeto actual.</param>
        /// <returns>True si los objetos son iguales; de lo contrario, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            GatoIngreso other = (GatoIngreso)obj;

            // Comparar las propiedades relevantes
            return
                this.textBox1.Text == other.textBox1.Text &&
                this.textBox2.Text == other.textBox2.Text &&
                this.textBox4.Text == other.textBox4.Text &&
                this.textBox5.Text == other.textBox5.Text &&
                this.textBox6.Text == other.textBox6.Text &&
                this.comboBox1.Text == other.comboBox1.Text;
        }
    }
}
