using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    public class Comida
    {
        public string nombre;
        public string tamaño;
        public Comida()
        {

        }
        public Comida(string? nombre, string tamaño)
        {
            this.nombre = nombre;
            this.tamaño = tamaño;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nombre: {this.nombre} tamaño: {this.tamaño}");
            return sb.ToString();
        }
        /*
        public override bool Equals(object? obj)
        {
            bool retorno = false;
            if (obj is Animales)
            {
                retorno = this == (Animales)obj;
            }
            return retorno;
        }
        */
    }
}
