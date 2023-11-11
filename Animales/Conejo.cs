using System.Text;

namespace Animales
{
    public class Conejo : Animales
    {
        public string? comportamiento;
        public string? habitad;
        public string tipoDeAnimal = "Conejo";

        public Conejo()
        {
            // Constructor sin argumentos
        }
        public Conejo(string comportamiento, string nombre, string tipoDeAnimal, int edad, Alimentacion alimentacion, string raza) : base(nombre, tipoDeAnimal, edad, alimentacion, raza)
        {
            this.comportamiento = comportamiento;
            this.tipoDeAnimal = tipoDeAnimal;
        }
        public Conejo(string habitad, string comportamiento, string nombre, string tipoDeAnimal, int edad, Alimentacion alimentacion, string raza) : this(comportamiento, nombre, tipoDeAnimal, edad, alimentacion, raza)
        {
            this.habitad = habitad;
        }
        public override string ToString()
        {
            return $"{this.nombre}-{this.edad}-{this.raza}-{this.alimentacion}-{this.comportamiento}-{this.habitad}-Conejo";
        }

    }
}
