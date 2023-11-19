﻿using Animal;
using Animales;
using System.Data.SqlClient;
namespace Animal
{

    public class updateCrudBaseDatos
    {
        public Animales.Animales? animales;
        private Veterinaria<Animales.Animales, Comida> veterinaria;

        public void actualizarCrudBaseDatos(Veterinaria<Animales.Animales, Comida> veterinaria)
        {
            try
            {
                conexcionBaseDatos.Conectar();
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


                                animales = new Animales.Gato(peso, vidas, valorColumna1, "Gato", edad, alimentacion, valorColumna3);
                                veterinaria.listaPacientes.Add(animales);

                            }
                            else if (reader["TIPO"].ToString() == "Gato")
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

                                this.animales = new Animales.Perro(valorColumna5, valorColumna6, valorColumna1, "Perro", edad, alimentacion, valorColumna3);
                                veterinaria.listaPacientes.Add(animales);
                            }
                            else if (reader["TIPO"].ToString() == "Conejo")
                            {
                                string valorColumna1 = reader["NOMBRE"].ToString();
                                string valorColumna2 = reader["EDAD"].ToString();
                                string valorColumna3 = reader["RAZA"].ToString();
                                string valorColumna4 = reader["ALIMENTACION"].ToString();
                                string valorColumna5 = reader["HABITAD"].ToString();
                                string valorColumna6 = reader["COMPORTAMIENTO"].ToString();

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

                                this.animales = new Animales.Conejo(valorColumna5, valorColumna6, valorColumna1, "Conejo", edad, alimentacion, valorColumna3);
                                veterinaria.listaPacientes.Add(animales);
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
    }
}
