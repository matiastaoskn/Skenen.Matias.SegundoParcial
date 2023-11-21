using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudVeterinaria
{
    public class CredencialesInvalidasException : Exception
    {
        public CredencialesInvalidasException() : base("Credenciales inválidas.")
        {
        }
    }

    public class RegistroNoGuardado : Exception
    {
        public RegistroNoGuardado() : base("El registro de usuario no se guardo")
        {
        }
    }

    public class CamposVaciosException: Exception
    {
        public CamposVaciosException() : base("Todos los campos estan vacios")
        {
        }
    }

    public class InsuficientePermisoException : Exception
    {
        public InsuficientePermisoException() : base("Todos los campos estan vacios")
        {
        }
    }

    public class DatosIngresadosIncorrectos : Exception
    {
        public DatosIngresadosIncorrectos() : base("Datos ingresados incorrectos")
        {
        }
    }

}
