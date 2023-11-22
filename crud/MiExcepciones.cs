using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudVeterinaria
{

    public class CredencialesInvalidasException : Exception
    {
        public CredencialesInvalidasException() : base() { }
        public CredencialesInvalidasException(string message) : base(message) { }
        public CredencialesInvalidasException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class RegistroNoGuardadoException : Exception
    {
        public RegistroNoGuardadoException() : base() { }
        public RegistroNoGuardadoException(string message) : base(message) { }
        public RegistroNoGuardadoException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CamposVaciosException : Exception
    {
        public CamposVaciosException() : base("Se encontraron campos vacíos.") { }
        public CamposVaciosException(string message) : base(message) { }
        public CamposVaciosException(string message, Exception innerException) : base(message, innerException) { }
    }


    public class AnimalIncorrectoException : Exception
    {
        public AnimalIncorrectoException() : base() { }
        public AnimalIncorrectoException(string message) : base(message) { }
        public AnimalIncorrectoException(string message, Exception innerException) : base(message, innerException) { }
    }

}
