using System.Text;

namespace Animales
{
    public class Gato : Animales
    {
        public int? vidas;
        public int? peso;
        public string tipoDeAnimal = "Gato";

        public Gato()
        {
            // Constructor sin argumentos
        }
        public Gato(int vidas, string nombre, string tipoDeAnimal, int edad, Alimentacion alimentacion, string raza) : base(nombre, tipoDeAnimal, edad, alimentacion, raza)
        {
            this.vidas = vidas;
            this.tipoDeAnimal = tipoDeAnimal;
        }
        public Gato(int peso, int vidas, string nombre, string tipoDeAnimal, int edad, Alimentacion alimentacion, string raza) : this(vidas, nombre, tipoDeAnimal, edad, alimentacion, raza)
        {
            this.peso = peso;
        }

        public override void HacerSonido()
        {
            Console.WriteLine("Este animal maulla.");
        }

        public override string ToString()
        {
            return $"{this.nombre}-{this.edad}-{this.raza}-{this.alimentacion}-{this.vidas}-{this.peso}-Gato";
        }
    }
}
