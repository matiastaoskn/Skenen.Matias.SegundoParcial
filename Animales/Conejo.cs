using Animal;
using System.Text;

namespace Animales
{
    /// <summary>
    /// Clase que representa a un conejo, heredando de la clase base Animales.
    /// </summary>
    public class Conejo : Animales, IfechaCarga
    {
        public string Comportamiento { get; set; }
        public string Habitad { get; set; }
        /// <summary>
        /// Constructor que inicializa un conejo con comportamiento, nombre, edad, alimentación y raza.
        /// </summary>
        public Conejo()
        {
            // Constructor sin argumentos
        }
        public Conejo(string comportamiento, string nombre, int edad, Alimentacion alimentacion, string raza)
            : base(nombre, "Conejo", edad, alimentacion, raza)
        {
            this.Comportamiento = comportamiento;
        }
        public Conejo(string habitad, string comportamiento, string nombre, int edad, Alimentacion alimentacion, string raza)
            : this(comportamiento, nombre, edad, alimentacion, raza)
        {
            this.Habitad = habitad;
        }

        public override string ToString()
        {
            return $"{this.nombre}-{this.edad}-{this.raza}-{this.alimentacion}-{this.Comportamiento}-{this.Habitad} - Tipo: {TipoDeAnimal} | {cargarFecha()}";
        }
        public string cargarFecha()
        {
            string fecha = DateTime.Now.ToString("dd/MM");
            return fecha;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Conejo conejoingreso = (Conejo)obj;
            return base.Equals(obj) &&
                   Comportamiento == conejoingreso.Comportamiento &&
                   Habitad == conejoingreso.Habitad;
        }
    }
}
