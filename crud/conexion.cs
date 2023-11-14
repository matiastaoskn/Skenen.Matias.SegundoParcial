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
        public static SqlConnection Conectar()
        {
            SqlConnection cn = new SqlConnection("SERVER=DESKTOP-645MCUC\\SQLEXPRESS;DATABASE=VETERINARIA;integrated security=true");
            cn.Open();
            return cn;
        }
    }
}
