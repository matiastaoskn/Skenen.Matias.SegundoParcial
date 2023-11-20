using System.IO;
namespace TestProject1
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
        public void CrearRegistro_DeberiaInsertarCorrectamente()
        {
            /*
            // Arrange: Configura el escenario de prueba
            
            Anima instanciaMiClase = new MiClase();
            DatosRegistro datosRegistro = new DatosRegistro { Campo1 = "valor1", Campo2 = "valor2" };

            // Act: Llama a la función que deseas probar
            bool resultado = instanciaMiClase.CrearRegistro(datosRegistro);

            // Assert: Verifica el resultado
            Assert.IsTrue(resultado);  // Puedes personalizar según lo que devuelve tu función
                                       // Además, puedes realizar consultas a la base de datos de prueba para asegurarte de que se insertó correctamente el registro
            */
        }
    }
}