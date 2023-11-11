
using System.Text;

namespace Animales
{
    public abstract class Animales
    {
        public string? nombre;
        public int edad;
        public string? raza;
        public Alimentacion alimentacion;

        public string TipoDeAnimal;

        public Animales()
        {

        }
        public Animales(string? nombre, string tipoDeAnimal)
        {
            this.nombre = nombre;
            TipoDeAnimal = tipoDeAnimal;
        }
        public Animales(string? nombre, string tipoDeAnimal, int edad, string raza) : this(nombre, tipoDeAnimal)
        {
            this.edad = edad;
            this.raza = raza;
            this.nombre = nombre;
            TipoDeAnimal = tipoDeAnimal;
        }
        public Animales(string? nombre, string tipoDeAnimal, int edad, Alimentacion alimentacion, string raza) : this(nombre, tipoDeAnimal, edad, raza)
        {
            this.alimentacion = alimentacion;
            this.nombre = nombre;
            TipoDeAnimal = tipoDeAnimal;
            this.edad = edad;
            this.raza = raza;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nombre: {this.nombre} edad: {this.edad}");
            return sb.ToString();
        }
        public override bool Equals(object? obj)
        {
            bool retorno = false;
            if (obj is Animales)
            {
                retorno = this == (Animales)obj;
            }
            return retorno;
        }

        public static bool operator ==(Animales a1, Animales a2)
        {
            if (a1.nombre == a2.nombre && a1.edad == a2.edad)
            {
                return false;
            }
            return true;
        }

        public static bool operator !=(Animales a1, Animales a2)
        {
            return !(a1 == a2);
        }

        public virtual void HacerSonido()
        {
            Console.WriteLine("Este animal hace un sonido.");
        }

        public override int GetHashCode()
        {
            return 0;
        }

    }
}