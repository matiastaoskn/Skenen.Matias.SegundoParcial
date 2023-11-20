using Animal;
using System.Text;

namespace Animales
{
    public class Perro : Animales, IfechaCarga
    {
        public string? Tamaño { get; set; }
        public string? Entrenamiento { get; set; }

        public Perro()
        {
            // Constructor sin argumentos
        }

        public Perro(string entrenamiento, string nombre, int edad, Alimentacion alimentacion, string raza) : base(nombre, "Perro", edad, alimentacion, raza)
        {
            this.Entrenamiento = entrenamiento;
        }

        public Perro(string tamaño, string entrenamiento, string nombre, int edad, Alimentacion alimentacion, string raza) : this(entrenamiento, nombre, edad, alimentacion, raza)
        {
            this.Tamaño = tamaño;
        }

        public override void HacerSonido()
        {
            Console.WriteLine("Este animal hace un ladrido.");
        }

        public override string ToString()
        {
            return $"{this.nombre}-{this.edad}-{this.raza}-{this.alimentacion}-{this.Tamaño}-{this.Entrenamiento}Perro | {cargarFecha()}";
        }

        public string cargarFecha()
        {
            string fecha = DateTime.Now.ToString("dd/MM");
            return fecha;
        }

        public override bool Equals(object obj)
        {
            // Comprobar si el objeto proporcionado es nulo o no es del mismo tipo
            if (obj == null || GetType() != obj.GetType())
            {
                // No son iguales
                return false;
            }

            Perro perroingreso = (Perro)obj;

            return base.Equals(obj) &&
                   object.Equals(Tamaño, perroingreso.Tamaño) &&
                   object.Equals(Entrenamiento, perroingreso.Entrenamiento);
        }
    }
}
