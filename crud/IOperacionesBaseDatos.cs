using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudVeterinaria
{
    public interface IOperacionesBaseDatos
    {
        public void actualizarCrudBaseDatos();

        Task eliminarElementoBaseDatos(string nombre);
    }
}
