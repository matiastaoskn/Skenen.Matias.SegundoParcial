using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CrudVeterinaria
{
    public class conexion
    {
        /// <summary>
        /// Establece una conexión a la base de datos utilizando la cadena de conexión predeterminada.
        /// </summary>
        /// <returns>Una instancia de SqlConnection abierta y lista para su uso.</returns>
        public static SqlConnection Conectar()
        {
            SqlConnection cn = new SqlConnection("SERVER=DESKTOP-645MCUC\\SQLEXPRESS;DATABASE=VETERINARIA;integrated security=true");
            cn.Open();
            return cn;
        }
    }
}
