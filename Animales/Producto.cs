using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    public class Producto: IfechaCarga
    {
        public string nombre;
        public string tamaño;
        public Producto()
        {

        }
        public Producto(string? nombre, string tamaño)
        {
            this.nombre = nombre;
            this.tamaño = tamaño;
        }

        public string cargarFecha()
        {
            string fecha = DateTime.Now.ToString("dd/MM");
            return fecha;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nombre: {this.nombre} tamaño: {this.tamaño} - Tipo: Producto | {cargarFecha()}");
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                // No son iguales
                return false;
            }
            Producto productoIngreso = (Producto)obj;
            return string.Equals(nombre, productoIngreso.nombre) &&
                   string.Equals(tamaño, productoIngreso.tamaño);
        }
    }
}
