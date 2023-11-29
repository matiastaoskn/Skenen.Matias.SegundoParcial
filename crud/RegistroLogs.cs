using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CrudVeterinaria
{
    public partial class RegistroLogs : Form
    {
        public RegistroLogs()
        {
            InitializeComponent();
            cargarDatos();
        }

        private void cargarDatos()
        {
            // Dominio de la maquina del usuario
            string directorioEjecutable = AppDomain.CurrentDomain.BaseDirectory;
            // Retrocedo de la carpeta bin
            string rutaRelativa = Path.Combine("..", "..", "..", "RegistroLogin.txt");
            if (!string.IsNullOrEmpty(rutaRelativa))
            {
                string filePath = Path.Combine(directorioEjecutable, rutaRelativa);
                try
                {
                    // Lee todas las líneas del archivo
                    string[] lineas = File.ReadAllLines(filePath);

                    // Muestra cada línea en el TextBox
                    foreach (string linea in lineas)
                    {
                        listBox1.Items.Add(linea);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
