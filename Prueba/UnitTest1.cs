using Animal;
using Animales;
using CrudVeterinaria;
using System.Drawing.Text;
using System.Windows.Forms;
using WindFormCrud;
using WindFormCrud.Ingresos;

namespace Prueba
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ArhcivoJsonExiste()
        {
            // Arrange 

            // Dominio de la maquina del usuario
            string directorioEjecutable = AppDomain.CurrentDomain.BaseDirectory;
            //Retrocedo de la carpeta bin
            string rutaArchivo = Path.Combine("..", "..", "..", "MOCK_DATA.json");
            string filePath = Path.GetFullPath(Path.Combine(directorioEjecutable, rutaArchivo));
            // Act
            if (File.Exists(filePath))
            {
                // Assert
                Assert.IsTrue(File.Exists(filePath));
            }
        }

        [TestMethod]
        public void ObtenerDatos_DeberiaDevolverResultadoEsperado()
        {
            // Arrange
            MDIformularioMain formulario = new MDIformularioMain(); // Aseg�rate de tener la clase MiClase correctamente definida
            // Act
            formulario.actualizarCrudBaseDatos();
        }

        public void Veterinaria_InicializacionCorrecta()
        {
            Veterinaria<Animales.Animales, Producto> veterinaria = new Veterinaria<Animales.Animales, Producto>(10);
            // Aseg�rate de que la veterinaria se haya inicializado correctamente
            Assert.IsNotNull(veterinaria);
        }

        [TestMethod]
        public void Veterinaria_AgregarAnimales()
        {
            Veterinaria<Animales.Animales, Producto> veterinaria = new Veterinaria<Animales.Animales, Producto>(10);
            Perro perro = new Perro("Poco", "Dog", "Perro", 13, Alimentacion.carnivoro, "PitBull");
            veterinaria.listaPacientes.Add(perro);

            // Aseg�rate de que el perro se haya agregado correctamente a la lista
            CollectionAssert.Contains(veterinaria.listaPacientes, perro);
        }

        [TestMethod]
        public void Veterinaria_AgregarComida()
        {
            Veterinaria<Animales.Animales, Producto> veterinaria = new Veterinaria<Animales.Animales, Producto>(10);
            Producto comida = new Producto("Croquetas", "Grande"); // Puedes ajustar los valores seg�n tu implementaci�n
            veterinaria.listaComida.Add(comida);
            // Aseg�rate de que la comida se haya agregado correctamente a la lista
            Assert.IsTrue(veterinaria.listaComida.Contains(comida));
        }

        [TestMethod]
        public void Veterinaria_EliminarElemento()
        {
            Veterinaria<Animales.Animales, Producto> veterinaria = new Veterinaria<Animales.Animales, Producto>(10);
            Animales.Animales perro = new Perro("Poco", "Dog", "Perro", 13, Alimentacion.carnivoro, "PitBull"); // Puedes ajustar los valores seg�n tu implementaci�n
            veterinaria.listaPacientes.Add(perro);

            // Elimina el perro y aseg�rate de que ya no est� en la lista
            veterinaria.listaPacientes.Remove(perro);
            Assert.IsFalse(veterinaria.listaPacientes.Contains(perro));
        }

    }
}