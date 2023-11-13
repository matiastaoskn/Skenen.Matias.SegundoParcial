﻿using CrudVeterinaria;

namespace WindFormCrud
{
    public partial class PerroIngreso : AnimalIngresoForm
    {
        public Animales.Animales? animales;

        public PerroIngreso(Animales.Animales animales, Animales.Perro perros) : this()
        {
            this.textBox1.Text = animales.nombre?.ToString();
            this.textBox2.Text = animales.edad.ToString();
            this.comboBox1.Text = animales.alimentacion.ToString();
            this.textBox4.Text = animales.raza?.ToString();
            this.comboBox2.Text = perros.entrenamiento;
            this.textBox6.Text = perros.tamaño;
        }
        public PerroIngreso()
        {
            InitializeComponent();
            TipoAnimal = "Perro";
        }
        /// <summary>
        /// Este metodo valida los datos ingresados por el usuario para no generar error de tipo null
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
                }
            }
            catch (CamposVaciosException)
            {
                MessageBox.Show($"Campos vacios", "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new CamposVaciosException();
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

            this.animales = new Animales.Perro(tamaño, entrenamiento, nombre, tipoDeAnimal, edad, alimentacion, raza);
            this.DialogResult = DialogResult.OK;
            this.Close();

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
    }
}
