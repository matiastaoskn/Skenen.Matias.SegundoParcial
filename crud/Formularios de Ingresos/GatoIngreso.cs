namespace WindFormCrud
{
    public partial class GatoIngreso : AnimalIngresoForm
    {

        public Animales.Animales? animales;
        public GatoIngreso()
        {
            InitializeComponent();
            TipoAnimal = "Gato";
        }
        public GatoIngreso(Animales.Animales animales, Animales.Gato gato) : this()
        {
            this.textBox1.Text = animales.nombre?.ToString();
            this.textBox2.Text = animales.edad.ToString();
            this.comboBox1.Text = animales.raza?.ToString();
            this.textBox4.Text = animales.alimentacion.ToString();
            this.textBox5.Text = gato.vidas.ToString();
            this.textBox6.Text = gato.peso.ToString();
        }
        /// <summary>
        /// Este metodo valida los datos ingresados por el usuario para no generar error de tipo null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validarInputs()
        {
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

            this.animales = new Animales.Gato(peso, vidas, nombre, TipoAnimal, edad, alimentacion, raza);
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
    }


}
