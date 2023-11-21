
using System.Text;

namespace Animales
{
    public abstract class Animales
    {
        /// <summary>
        /// Clase base abstracta que representa a un animal genérico.
        /// </summary>
        public string? nombre;
        public int edad;
        public string? raza;
        public Alimentacion alimentacion;

        public string TipoDeAnimal { get; }

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
        }
        public Animales(string? nombre, string tipoDeAnimal, int edad, Alimentacion alimentacion, string raza) : this(nombre, tipoDeAnimal, edad, raza)
        {
            this.alimentacion = alimentacion;
        }
        
        public static bool operator ==(Animales a1, Animales a2)
        {
            return a1.nombre == a2.nombre && a1.edad == a2.edad;
        }
        public static bool operator !=(Animales a1, Animales a2)
        {
            return !(a1 == a2);
        }
        public virtual void HacerSonido()
        {
            Console.WriteLine("Este animal hace un sonido.");
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nombre: {this.nombre} edad: {this.edad}");
            return sb.ToString();
        }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is Animales otherAnimal)
            {
                return this == otherAnimal;
            }

            return false;
        }

    }
}