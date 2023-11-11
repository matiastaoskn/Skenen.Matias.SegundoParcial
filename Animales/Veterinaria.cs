namespace Animales
{
    public class Veterinaria
    {

        private int cantMaxPacientes;
        public List<Animales>? listaPacientes;

        public Veterinaria()
        {
            this.listaPacientes = new List<Animales>();
        }

        public static bool operator ==(Animales a, Veterinaria b)
        {
            bool retorno = false;
            if (b.listaPacientes != null)
            {
                foreach (Animales item in b.listaPacientes)
                {
                    if (item == a)
                    {
                        return retorno = true;
                    }
                }
            }
            return retorno;

        }

        public static bool operator !=(Animales a, Veterinaria b)
        {
            return !(a == b);
        }

        public static Veterinaria operator +(Animales a, Veterinaria b)
        {
            if (b.listaPacientes != null)
            {
                if (b.listaPacientes.Count < b.cantMaxPacientes)
                {
                    if (a != b && !(b.listaPacientes.Contains(a)))
                    {
                        b.listaPacientes.Add(a);
                        return b;
                    }
                    else
                    {
                        Console.WriteLine("Este animal ya se encuentra en la veterinaria");
                    }


                }
                else
                {
                    Console.WriteLine("La veterinaria se encuentra llena");
                }
            }
            else
            {
                Console.WriteLine("La lista es null");
            }

            return b;
        }

        public static Veterinaria operator -(Animales a, Veterinaria b)
        {
            if (b.listaPacientes != null)
            {
                if (b.listaPacientes.Contains(a))
                {
                    b.listaPacientes.Remove(a);
                    return b;
                }
                else
                {
                    Console.WriteLine($"{a}, No se ecuentra en la lista");
                }
            }
            else
            {
                Console.WriteLine("La lista es null");
            }

            return b;
        }


        //Para no pedir advertencia  
        public override bool Equals(object? o)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }


    }
}
