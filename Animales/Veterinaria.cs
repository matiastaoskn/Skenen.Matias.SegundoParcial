using Animal;

namespace Animales
{
    public class Veterinaria<T, P> where T : Animales
    {

        private int cantMaxPacientes;
        public List<T>? listaPacientes;
        public List<P>? listaComida;

        public Veterinaria()
        {
            this.listaPacientes = new List<T>();
            this.listaComida = new List<P>();
        }

        public static bool operator ==(Animales a, Veterinaria<T, P> b)
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

        public static bool operator !=(Animales a, Veterinaria<T, P> b)
        {
            return !(a == b);
        }

        public static Veterinaria<T, P> operator +(Animales a, Veterinaria<T, P> b)
        {
            if (b.listaPacientes != null)
            {
                if (b.listaPacientes.Count < b.cantMaxPacientes)
                {
                    if (a != b && !(b.listaPacientes.Contains(a)))
                    {
                        b.listaPacientes.Add((T)a);
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



        public static Veterinaria<T, P> operator -(Animales a, Veterinaria<T, P> b)
        {
            if (b.listaPacientes != null)
            {
                if (b.listaPacientes.Contains(a))
                {
                    b.listaPacientes.Remove((T)a);
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
