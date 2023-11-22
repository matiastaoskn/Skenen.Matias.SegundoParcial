using Animal;
using CrudVeterinaria;
using System.Data.SqlClient;

namespace WindFormCrud
{
    public partial class PerroIngreso : AnimalIngresoForm
    {
        public Animales.Animales? animales;
        private bool esActualizacion = false;
        int id;

        public PerroIngreso()
        {
            InitializeComponent();
            TipoAnimal = "Perro";
        }
        public PerroIngreso(Animales.Animales animales, Animales.Perro perros) : this()
        {
            esActualizacion = true;

            this.textBox1.Text = animales.nombre?.ToString();
            this.textBox2.Text = animales.edad.ToString();
            this.comboBox1.Text = animales.alimentacion.ToString();
            this.textBox4.Text = animales.raza?.ToString();
            this.comboBox2.Text = perros.Entrenamiento;
            this.textBox6.Text = perros.Tamaño;

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
        /// Valida los datos ingresados por el usuario en el formulario de ingreso o actualización de perros.
        /// Si los campos son válidos, crea un nuevo objeto Perro o actualiza uno existente en la base de datos.
        /// </summary>
        private void validarInputs()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.textBox1.Text) &&
                    string.IsNullOrWhiteSpace(this.textBox2.Text) &&
                    string.IsNullOrWhiteSpace(this.textBox4.Text) &&
                    string.IsNullOrWhiteSpace(this.comboBox1.Text) &&
                    string.IsNullOrWhiteSpace(this.comboBox2.Text) &&
                    string.IsNullOrWhiteSpace(this.textBox6.Text))
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
                MessageBox.Show("El nombre no puede estar en blanco");
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

            string tamaño = this.comboBox2.Text;
            string entrenamiento = this.textBox6.Text;

            if (int.TryParse(tamaño, out _))
            {
                MessageBox.Show("El tamaño no puede ser un número.");
                return;
            }

            if (int.TryParse(entrenamiento, out _))
            {
                MessageBox.Show("El entrenamiento no puede ser un número.");
                return;
            }

            this.animales = new Animales.Perro(tamaño, entrenamiento, nombre, edad, alimentacion, raza);
            conexcionBaseDatos.Conectar();
            agregarPerro();



        }
        /// <summary>
        /// Confirma el dialogo de la ventana, guardando los datos del tipo "Perro"
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
        private void buttonResetear_Click(object sender, EventArgs e)
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
        /// Compara dos objetos PerroIngreso para determinar si son iguales.
        /// </summary>
        /// <param name="obj">El objeto a comparar con el objeto actual.</param>
        /// <returns>
        /// True si los objetos son iguales; de lo contrario, False.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            PerroIngreso other = (PerroIngreso)obj;

            // Comparar las propiedades relevantes
            return
                this.textBox1.Text == other.textBox1.Text &&
                this.textBox2.Text == other.textBox2.Text &&
                this.textBox4.Text == other.textBox4.Text &&
                this.comboBox1.Text == other.comboBox1.Text &&
                this.comboBox2.Text == other.comboBox2.Text &&
                this.textBox6.Text == other.textBox6.Text;
        }

        public void agregarPerro()
        {
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
                    cmd.Parameters.AddWithValue("@TAMAÑO", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@ENTRENAMIENTO", textBox6.Text);

                    cmd.Parameters.AddWithValue("@VIDAS", DBNull.Value);
                    cmd.Parameters.AddWithValue("@PESO", DBNull.Value);
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
                    string actualizar = "UPDATE ANIMALES SET NOMBRE=@NOMBRE, EDAD=@EDAD, RAZA=@RAZA, ALIMENTACION=@ALIMENTACION, TAMAÑO=@TAMAÑO, ENTRENAMIENTO=@ENTRENAMIENTO WHERE ID=@ID";

                    using (SqlCommand cmd = new SqlCommand(actualizar, conexcionBaseDatos.Conectar()))
                    {
                        cmd.Parameters.AddWithValue("@ID", id); // Usar el nombre original en la condición WHERE
                        cmd.Parameters.AddWithValue("@NOMBRE", textBox2.Text);
                        cmd.Parameters.AddWithValue("@EDAD", textBox2.Text);
                        cmd.Parameters.AddWithValue("@RAZA", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@ALIMENTACION", textBox4.Text);
                        cmd.Parameters.AddWithValue("@TAMAÑO", comboBox2.Text);
                        cmd.Parameters.AddWithValue("@ENTRENAMIENTO", textBox6.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("DATO ACTUALIZADO");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar datos: " + ex.Message);
                }
            }
        }
    }
}
