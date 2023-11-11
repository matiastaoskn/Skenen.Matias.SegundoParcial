namespace WindFormCrud.Ingresos
{
    public partial class ConejoIngreso : AnimalIngresoForm
    {
        public Animales.Animales? animales;
        public ConejoIngreso()
        {
            InitializeComponent();
            TipoAnimal = "Conejo";
        }

        public ConejoIngreso(Animales.Animales animales, Animales.Conejo conejo): this()
        {
            this.textBox1.Text = animales.nombre?.ToString();
            this.textBox2.Text = animales.edad.ToString();
            this.comboBox1.Text = animales.alimentacion.ToString();
            this.textBox4.Text = animales.raza?.ToString();
            this.textBox5.Text = conejo.comportamiento;
            this.textBox6.Text = conejo.habitad;
        }
        /// <summary>
        /// Este metodo valida los datos ingresados por el usuario para no generar error de tipo null
        /// </summary>
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

            string comportamiento = this.textBox5.Text;
            if (int.TryParse(comportamiento, out _))
            {
                MessageBox.Show("El comportamiento no puede ser un número");
                return;
            }
            string habitad = this.textBox6.Text;
            if (int.TryParse(habitad, out _))
            {
                MessageBox.Show("El habitad no puede ser un número");
                return;
            }

            this.animales = new Animales.Conejo(habitad, comportamiento, nombre, tipoDeAnimal, edad, alimentacion, raza);
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
        /// <summary>
        /// Confirma el dialogo de la ventana, guardando los datos del tipo "Conejo"
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            validarInputs();
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
        private void btnReiniciar_Click(object sender, EventArgs e)
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
