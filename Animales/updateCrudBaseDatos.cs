using Animales;
using System.Data.SqlClient;
namespace Animal
{

    public class updateCrudBaseDatos
    {
        public Animales.Animales? animales;
        public Producto comida;
        private Veterinaria<Animales.Animales, Producto>? veterinaria;

        public void actualizarCrudBaseDatos(Veterinaria<Animales.Animales, Producto> veterinaria)
        {
            conexcionBaseDatos ado = new conexcionBaseDatos();

            if (ado.probarConexcion())
            {
                Console.WriteLine("Se conecto");

                try
                {

                    // Definir la consulta SQL
                    string query = "SELECT ID, TIPO, NOMBRE, EDAD,RAZA, ALIMENTACION, VIDAS, PESO, TAMAÑO, ENTRENAMIENTO, HABITAD, COMPORTAMIENTO FROM ANIMALES";

                    // Crear un objeto SqlCommand
                    using (SqlCommand command = new SqlCommand(query, conexcionBaseDatos.Conectar()))
                    {
                        // Crear un lector de datos
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Iterar a través de los resultados
                            while (reader.Read())
                            {

                                if (reader["TIPO"].ToString() == "Gato")
                                {
                                    string valorColumna1 = reader["NOMBRE"].ToString();
                                    string valorColumna2 = reader["EDAD"].ToString();
                                    string valorColumna3 = reader["RAZA"].ToString();
                                    string valorColumna4 = reader["ALIMENTACION"].ToString();
                                    string valorColumna5 = reader["VIDAS"].ToString();
                                    string valorColumna6 = reader["PESO"].ToString();

                                    Alimentacion alimentacion;
                                    if (!Enum.TryParse(valorColumna4, out alimentacion))
                                    {
                                    }

                                    int vidas;
                                    if (int.TryParse(valorColumna5, out vidas))
                                    {
                                        // La conversión fue exitosa, y el valor de vidas se encuentra en la variable 'vidas'
                                    }


                                    int edad;
                                    if (int.TryParse(valorColumna2, out edad))
                                    {
                                        // La conversión fue exitosa, y el valor de vidas se encuentra en la variable 'vidas'
                                    }

                                    int peso;
                                    if (int.TryParse(valorColumna6, out peso))
                                    {
                                        // La conversión fue exitosa, y el valor de vidas se encuentra en la variable 'vidas'
                                    }


                                    animales = new Animales.Gato(peso, vidas, valorColumna1, edad, alimentacion, valorColumna3);
                                    veterinaria.listaPacientes.Add(animales);

                                }
                                else if (reader["TIPO"].ToString() == "Perro")
                                {
                                    string valorColumna1 = reader["NOMBRE"].ToString();
                                    string valorColumna2 = reader["EDAD"].ToString();
                                    string valorColumna3 = reader["RAZA"].ToString();
                                    string valorColumna4 = reader["ALIMENTACION"].ToString();
                                    string valorColumna5 = reader["TAMAÑO"].ToString();
                                    string valorColumna6 = reader["ENTRENAMIENTO"].ToString();

                                    Alimentacion alimentacion;
                                    if (!Enum.TryParse(valorColumna4, out alimentacion))
                                    {
                                    }

                                    int vidas;
                                    if (int.TryParse(valorColumna5, out vidas))
                                    {
                                        // La conversión fue exitosa, y el valor de vidas se encuentra en la variable 'vidas'
                                    }


                                    int edad;
                                    if (int.TryParse(valorColumna2, out edad))
                                    {
                                        // La conversión fue exitosa, y el valor de vidas se encuentra en la variable 'vidas'
                                    }

                                    int peso;
                                    if (int.TryParse(valorColumna6, out peso))
                                    {
                                        // La conversión fue exitosa, y el valor de vidas se encuentra en la variable 'vidas'
                                    }

                                    this.animales = new Animales.Perro(valorColumna5, valorColumna6, valorColumna1, edad, alimentacion, valorColumna3);
                                    veterinaria.listaPacientes.Add(animales);
                                }
                                else if (reader["TIPO"].ToString() == "Conejo")
                                {
                                    string nombre = reader["NOMBRE"].ToString();
                                    string edadValor = reader["EDAD"].ToString();
                                    string raza = reader["RAZA"].ToString();
                                    string alimentacionvalor = reader["ALIMENTACION"].ToString();
                                    string habitad = reader["HABITAD"].ToString();
                                    string comportamiento = reader["COMPORTAMIENTO"].ToString();

                                    int edad;
                                    if (int.TryParse(edadValor, out edad))
                                    {
                                        // La conversión fue exitosa, y el valor de vidas se encuentra en la variable 'vidas'
                                    }

                                    Alimentacion alimentacion;
                                    if (!Enum.TryParse(alimentacionvalor, out alimentacion))
                                    {
                                    }

                                    this.animales = new Animales.Conejo(habitad, comportamiento, nombre, edad, alimentacion, raza);
                                    veterinaria.listaPacientes.Add(animales);
                                }
                                else
                                {
                                    string valorColumna1 = reader["NOMBRE"].ToString();
                                    string valorColumna2 = reader["TIPO"].ToString();

                                    this.comida = new Animal.Producto(valorColumna1, valorColumna2);
                                    veterinaria.listaComida.Add(comida);
                                }

                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción
                    Console.WriteLine($"Error: {ex.Message}");
                }

            }
            else
            {
                Console.WriteLine("No se conecto");
            }

            
        }
    }
}
