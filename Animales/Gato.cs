using Animal;
using System.Text;

namespace Animales
{
    /// <summary>
    /// Clase que representa a un gato, heredando de la clase base Animales.
    /// </summary>
    public class Gato : Animales, IfechaCarga
    {
        public int? Vidas { get; set; }
        public int? Peso { get; set; }

        public Gato()
        {
            // Constructor sin argumentos
        }

        public Gato(int vidas, string nombre, int edad, Alimentacion alimentacion, string raza) : base(nombre, "Gato", edad, alimentacion, raza)
        {
            this.Vidas = vidas;
        }

        public Gato(int peso, int vidas, string nombre, int edad, Alimentacion alimentacion, string raza) : this(vidas, nombre, edad, alimentacion, raza)
        {
            this.Peso = peso;
        }

        public override void HacerSonido()
        {
            Console.WriteLine("Este animal maulla.");
        }
        public string cargarFecha()
        {
            string fecha = DateTime.Now.ToString("dd/MM");
            return fecha;
        }

        public override string ToString()
        {
            return $"{this.nombre}-{this.edad}-{this.raza}-{this.alimentacion}-{this.Vidas}-{this.Peso} - Tipo: {TipoDeAnimal} | {cargarFecha()}";
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                // No son iguales
                return false;
            }
            Gato gatoingreso = (Gato)obj;

            return base.Equals(obj) &&
                   object.Equals(Vidas, gatoingreso.Vidas) &&
                   object.Equals(Peso, gatoingreso.Peso);
        }
    }
}
