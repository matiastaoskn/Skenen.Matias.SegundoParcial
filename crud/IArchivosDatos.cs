using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudVeterinaria
{
    public interface IArchivosDatos
    {
        void GuardarDatos(string rutaArchivo);
        void CargarDatos(string rutaArchivos);
    }
}
