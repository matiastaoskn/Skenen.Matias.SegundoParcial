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
    }
}