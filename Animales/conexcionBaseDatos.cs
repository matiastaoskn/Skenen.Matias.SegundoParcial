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
        public static SqlConnection Conectar()
        {
            SqlConnection cn = new SqlConnection("SERVER=DESKTOP-645MCUC\\SQLEXPRESS;DATABASE=VETERINARIA;integrated security=true");
            cn.Open();
            return cn;
        }
    }
}
