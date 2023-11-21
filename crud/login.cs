using CrudVeterinaria;
using System.Text.Json;

namespace WindFormCrud
{
    public partial class login : Form
    {

        private int acumuladorDeIntentos;
        /// <summary>
        /// Incializa el formulario de login y modifica su posicion de inicio
        /// </summary>
        public login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }
        /// <summary>
        /// Este metodo cancela el dialogo con el formulario, cerrando su ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Este metodo reinicia los textbox, para simplificar al usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            textUser.Clear();
            textPass.Clear();
        }
        /// <summary>
        /// Este metodo es el que confirma el dialogo cuando el usuario termina de completar lo datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (acumuladorDeIntentos < 4)
            {
                this.label4.Text = acumuladorDeIntentos.ToString();
                acumuladorDeIntentos++;
                deserializarJson();
            }
            else
            {
                this.textUser.Enabled = false;
                this.textPass.Enabled = false;
                this.btnAceptar.Enabled = false;
                this.btnReiniciar.Enabled = false;
            }

        }
        /// <summary>
        /// Este metodo se encarga de deserializar desde una ruta relativa
        /// analiza los datos y busca los nombres, claves
        /// Si condicen estas dos, el usuario entra al programa, guardando su nombre
        /// </summary>
        private void deserializarJson()
            {
                // Dominio de la maquina del usuario
                string directorioEjecutable = AppDomain.CurrentDomain.BaseDirectory;
                //Retrocedo de la carpeta bin
                string rutaArchivo = Path.Combine("..", "..", "..", "MOCK_DATA.json");
                string filePath = Path.GetFullPath(Path.Combine(directorioEjecutable, rutaArchivo));

                if (File.Exists(filePath))
                {
                    string json_str = File.ReadAllText(filePath);

                    List<Persona> personas = JsonSerializer.Deserialize<List<Persona>>(json_str);

                    try
                    {
                        foreach (var persona in personas)
                        {
                        
                            if (textUser.Text == persona.correo && textPass.Text == persona.clave)
                            {
                                if(comboBox1.Text == persona.perfil)
                                {
                                    UserNameLogin.setTipoPerfil(comboBox1.Text);
                                    UserNameLogin.setUserName(persona.nombre);

                                    agregarRegistroTxt(persona.nombre);
                                    this.DialogResult = DialogResult.OK;
                                }
                                else
                                {
                                    MessageBox.Show("Seleccione correctamente su perfil");
                                }
                            }
                        }

                    }
                    catch (CredencialesInvalidasException ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw new CredencialesInvalidasException();
                    }


                }

            }
        /// <summary>
        /// Este metodo deja un registro con el nombre y la fecha actual, en un archivo txt
        /// </summary>
        /// <param name="usuario"></param>
        private void agregarRegistroTxt(string usuario)
        {
            try
            {
                // Dominio de la maquina del usuario
                string directorioEjecutable = AppDomain.CurrentDomain.BaseDirectory;
                //Retrocedo de la carpeta bin
                string rutaRelativa = Path.Combine("..", "..", "..", "RegistroLogin.txt");
                string filePath = Path.GetFullPath(Path.Combine(directorioEjecutable, rutaRelativa));

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine($"Usuario: {usuario} - Fecha y hora de inicio de sesión: {DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error de registro TXT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new RegistroNoGuardado();
            }
        }
    }

}
