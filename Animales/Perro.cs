using Animal;
using System.Text;

namespace Animales
{
    public class Perro : Animales, IfechaCarga
    {
        public string? tamaño;
        public string? entrenamiento;
        public string? tipoDeAnimal;

        public Perro()
        {
            // Constructor sin argumentos
        }
        public Perro(string entrenamiento, string nombre, string tipoDeAnimal, int edad, Alimentacion alimentacion, string raza) : base(nombre, tipoDeAnimal, edad, alimentacion, raza)
        {
            this.tamaño = entrenamiento;
            this.tipoDeAnimal = tipoDeAnimal;
        }
        public Perro(string tamaño, string entrenamiento, string nombre, string tipoDeAnimal, int edad, Alimentacion alimentacion, string raza) : this(entrenamiento, nombre, tipoDeAnimal, edad, alimentacion, raza)
        {
            this.tamaño = tamaño;
        }
        public override void HacerSonido()
        {
            Console.WriteLine("Este animal hace un ladrido.");
        }

        public override string ToString()
        {
            return $"{this.nombre}-{this.edad}-{this.raza}-{this.alimentacion}-{this.tamaño}-{this.entrenamiento}-Perro | {cargarFecha()}";
        }

        public string cargarFecha()
        {
            string fecha = DateTime.Now.ToString("dd/MM");
            return fecha;
        }

    }
}
