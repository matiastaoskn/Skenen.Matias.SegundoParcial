using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Animal
{
    public class conexcionBaseDatos
    {
        public SqlConnection conexcion;
        public static string cadena_conexcion;
        
        public static SqlConnection Conectar()
        {
            SqlConnection conexion = new SqlConnection(cadena_conexcion);
            conexion.Open();
            return conexion;

        }
        
        static conexcionBaseDatos()
        {
            conexcionBaseDatos.cadena_conexcion = Properties.Resources.miConexion;
            //conexcionBaseDatos.cadena_conexcion = @"Data Source=localhost\\SQLEXPRESS;Initial Catalog=VETERINARIA;Integrated Security=True";
        }

        public conexcionBaseDatos()
        {
            this.conexcion = new SqlConnection(conexcionBaseDatos.cadena_conexcion);
        }

        public bool probarConexcion()
        {
            bool retorno = false;

            try
            {
                this.conexcion.Open();
                retorno = true;
            }
            catch
            {

            }
            finally
            {
                if(this.conexcion.State == System.Data.ConnectionState.Open)
                {
                    this.conexcion.Close();
                }
                
            }
            return retorno;
        }
    }
}
