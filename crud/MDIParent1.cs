using Animales;
using WindFormCrud.Ingresos;
using Newtonsoft.Json.Converters;
using CrudVeterinaria;
using System.Data.SqlClient;
using Animal;
using System.Drawing.Text;
using System.IO;

namespace WindFormCrud
{
    public partial class MDIformularioMain : AnimalIngresoForm, IOperacionesBaseDatos
    {

        private string perfil = UserNameLogin.TipoPerfil;
        private Veterinaria<Animales.Animales, Comida> veterinaria;

        public delegate void ElementoEliminadoDelegate(string nombreElemento);
        public event ElementoEliminadoDelegate ElementoEliminadoEvent;

        public delegate void ActualizacionBaseDatosCompletaDelegate();
        public event ActualizacionBaseDatosCompletaDelegate ActualizacionBaseDatosCompletaEvent;

        /// <summary>
        /// Este contructor, inicializa una nueva lista de tipo Animales
        /// Toma el nombe de usuario guardado en el login
        /// Agrega la fecha y hora en la que inicio el programa
        /// </summary>
        public MDIformularioMain()
        {
            InitializeComponent();
            validarUsuario();
            veterinaria = new Veterinaria<Animales.Animales, Comida>();
            stripUser.Text = UserNameLogin.UserName;

            stripDateTime.Text = DateTime.Now.ToString();
            actualizarCrudBaseDatos();
            ElementoEliminadoEvent += MDIformularioMain_ElementoEliminadoEvent;

        }

        private void MDIformularioMain_ElementoEliminadoEvent(string nombreElemento)
        {
            // Actualiza la vista después de eliminar un elemento
            ActualizarVisor();
        }
        private void ActualizarVisor()
        {
            if (listBoxMenu.InvokeRequired)
            {
                // Si se está llamando desde un subproceso diferente, invocar al hilo principal.
                listBoxMenu.Invoke(new MethodInvoker(() => ActualizarVisor()));
                listBoxMenu.Invoke(new MethodInvoker(async () => await guardarDatosAutomaticoAsync()));
            }
            else
            {
                // Actualizar la interfaz de usuario en el hilo principal.
                this.listBoxMenu.Items.Clear();

                foreach (Animales.Animales paciente in veterinaria.listaPacientes)
                {
                    listBoxMenu.Items.Add(paciente.ToString());
                }

                foreach (Animal.Comida paciente in veterinaria.listaComida)
                {
                    listBoxMenu.Items.Add(paciente.ToString());
                }

            }
        }
        /// <summary>
        /// Este método gestiona el cierre de un formulario de ingreso de animales, añadiendo la información ingresada a una lista.
        /// Detecta el tipo de animal ingresado y lo convierte al tipo correspondiente antes de agregarlo a la lista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void capturaEventoFormClosed(object sender, FormClosedEventArgs e)
        {
            //Guarda el formulario que se abrio en la variable
            AnimalIngresoForm ingresoAnimal = (AnimalIngresoForm)sender;


            //Desoues de guardar el objeto, consulta su dialogo result.
            DialogResult dialogo = ingresoAnimal.DialogResult;

            if (dialogo == DialogResult.OK)
            {
                if (ingresoAnimal.TipoAnimal == "Perro")
                {
                    PerroIngreso IngresoPerroForm = (PerroIngreso)sender;

                    this.veterinaria.listaPacientes.Add(IngresoPerroForm.animales);

                }
                else if (ingresoAnimal.TipoAnimal == "Conejo")
                {
                    ConejoIngreso IngresoConejoForm = (ConejoIngreso)sender;

                    this.veterinaria.listaPacientes.Add(IngresoConejoForm.animales);

                }
                else if (ingresoAnimal.TipoAnimal == "Gato")
                {
                    GatoIngreso IngresoGatoForm = (GatoIngreso)sender;

                    this.veterinaria.listaPacientes.Add(IngresoGatoForm.animales);

                }
                else
                {
                    ProductoIngreso IngresoProducto = (ProductoIngreso)sender;

                    this.veterinaria.listaComida.Add(IngresoProducto.comida);
                }

                this.ActualizarVisor();
            }

        }
        /// <summary>
        /// Permite al usuario eliminar el elemento seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (perfil == "administrador")
            {
                if (this.listBoxMenu.SelectedIndex > -1)
                {
                    int indice = this.listBoxMenu.SelectedIndex;
                    string nombreElementoAEliminar;

                    // Verificar si el índice está dentro del rango de la lista de animales
                    if (indice < veterinaria.listaPacientes.Count)
                    {
                        nombreElementoAEliminar = veterinaria.listaPacientes[indice].nombre;

                        DialogResult resultado = MessageBox.Show($"¿Está seguro de que desea eliminar este animal? {nombreElementoAEliminar}", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            this.veterinaria.listaPacientes.RemoveAt(indice);
                            eliminarElementoBaseDatos(nombreElementoAEliminar);
                        }
                    }
                    else
                    {
                        // Si el índice está fuera del rango de la lista de animales, 
                        int indiceProducto = indice - veterinaria.listaPacientes.Count;

                        if (indiceProducto < veterinaria.listaComida.Count)
                        {
                            nombreElementoAEliminar = veterinaria.listaComida[indiceProducto].nombre;

                            // Preguntar al usuario si realmente quiere eliminar
                            DialogResult resultadoProducto = MessageBox.Show($"¿Está seguro de que desea eliminar este producto? {nombreElementoAEliminar}", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (resultadoProducto == DialogResult.Yes)
                            {
                                this.veterinaria.listaComida.RemoveAt(indiceProducto);
                                eliminarElementoBaseDatos(nombreElementoAEliminar);
                            }
                        }
                    }
                }
            }
            else
            {
                btnEliminarMenu.Enabled = false;
            }
        }
        /// <summary>
        /// Cuando el usuario cierra el formulario, se guardara una copia de seguridad en una carpeta aparte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cerrar la aplicación?", "Cerrar Aplicación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// Cuando el usuario decida guardar, se le permitira elegir la ruta en especifico con el explorador, guardando un archivo tipo JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos JSON (*.json)|*.json";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                try
                {

                    List<ObjetoSerializado> listaSerializada = new List<ObjetoSerializado>();

                    foreach (var paciente in veterinaria.listaPacientes)
                    {
                        // Obtener el nombre del tipo de objeto (Perro, Gato, Conejo)
                        string tipo = paciente.GetType().Name;

                        // Crear un ObjetoSerializado con el tipo y los datos
                        var objetoSerializado = new ObjetoSerializado
                        {
                            Tipo = tipo,
                            Datos = paciente
                        };

                        listaSerializada.Add(objetoSerializado);
                    }


                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(listaSerializada, Newtonsoft.Json.Formatting.Indented);


                    File.WriteAllText(path, json);


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al guardar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Permite al usuario abrir archivos y deserializar archivos tipo JSON, indicando la ruta del archivo brindado por el usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos JSON (*.json)|*.json";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;

                try
                {
                    // Lee el contenido del archivo JSON
                    string jsonContent = File.ReadAllText(path);

                    // Deserializa la lista de objetos serializados
                    var listaSerializada = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ObjetoSerializado>>(jsonContent);

                    // Limpiar la lista de pacientes existente si es necesario

                    veterinaria.listaPacientes.Clear();


                    // Recorrer la lista de objetos serializados

                    foreach (var objetoSerializado in listaSerializada)
                    {
                        // Obtener el tipo almacenado en el ObjetoSerializado
                        string tipo = objetoSerializado.Tipo;

                        switch (tipo)
                        {
                            case "Perro":

                                var perro = Newtonsoft.Json.JsonConvert.DeserializeObject<Perro>(objetoSerializado.Datos.ToString());

                                veterinaria.listaPacientes.Add(perro);

                                break;
                            case "Gato":

                                var gato = Newtonsoft.Json.JsonConvert.DeserializeObject<Gato>(objetoSerializado.Datos.ToString());

                                veterinaria.listaPacientes.Add(gato);

                                break;
                            case "Conejo":

                                var conejo = Newtonsoft.Json.JsonConvert.DeserializeObject<Conejo>(objetoSerializado.Datos.ToString());

                                veterinaria.listaPacientes.Add(conejo);

                                break;
                        }
                    }

                    this.ActualizarVisor();
                }
                catch (Exception ex)
                {
                    // Manejar errores, por ejemplo, mostrar un mensaje de error
                    MessageBox.Show("Error al abrir el archivo JSON: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //Ingresos animales
        /// <summary>
        /// segun el usuario seleccione, se ingresara a su respectivo formulario del animal, generando un nuevo objeto y capturando el cierre del mismo
        /// Este formulario se abre de forma modal, lo cual no permite visualizar su DialogResult.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formIngresoPerro(object sender, EventArgs e)
        {
            PerroIngreso formIngresoPerro = new PerroIngreso();

            formIngresoPerro.MdiParent = this;

            formIngresoPerro.FormClosed += capturaEventoFormClosed;

            formIngresoPerro.Show();
        }
        private void formIngresoGato(object sender, EventArgs e)
        {
            GatoIngreso formIngresoGato = new GatoIngreso();
            formIngresoGato.MdiParent = this;

            formIngresoGato.FormClosed += capturaEventoFormClosed;

            formIngresoGato.Show();
        }
        private void formIngresoConejo(object sender, EventArgs e)
        {
            ConejoIngreso formIngresoConejo = new ConejoIngreso();
            formIngresoConejo.MdiParent = this;

            formIngresoConejo.FormClosed += capturaEventoFormClosed;

            formIngresoConejo.Show();
        }
        private void formIngresoProducto(object sender, EventArgs e)
        {
            ProductoIngreso formIngresoProducto = new ProductoIngreso();
            formIngresoProducto.MdiParent = this;

            formIngresoProducto.FormClosed += capturaEventoFormClosed;

            formIngresoProducto.Show();
        }
        // Modificar animales
        /// <summary>
        /// Se consulta si hay un item selecionado en el listBox, se usa esa referencia como indice y se consulta como referencia en la lista
        /// Si la referencia es del tipo seleccionado, se generan los dos contructores recopilando la informacion del indice
        /// Al confirmar, se actualiza la referencia con los cambios realizados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modificarPerroForm(object sender, EventArgs e)
        {
            if (this.listBoxMenu.SelectedIndex > -1)
            {
                int selectedIndex = listBoxMenu.SelectedIndex;

                string selectedAnimalType = veterinaria.listaPacientes[selectedIndex].TipoDeAnimal;


                if (selectedAnimalType == "Perro")
                {
                    int indice = this.listBoxMenu.SelectedIndex;
                    Animales.Animales a = this.veterinaria.listaPacientes[indice];

                    Animales.Perro p = a as Animales.Perro;

                    PerroIngreso form2 = new PerroIngreso(a, p);

                    form2.ShowDialog();

                    if (form2.DialogResult == DialogResult.OK)
                    {

                        this.veterinaria.listaPacientes[indice] = (form2.animales);

                        this.ActualizarVisor();
                    }


                }
            }
        }
        private void modificarGatoForm(object sender, EventArgs e)
        {
            if (this.listBoxMenu.SelectedIndex > -1)
            {
                int selectedIndex = listBoxMenu.SelectedIndex;

                string selectedAnimalType = veterinaria.listaPacientes[selectedIndex].TipoDeAnimal;

                int indice = this.listBoxMenu.SelectedIndex;
                Animales.Animales a = this.veterinaria.listaPacientes[indice];

                if (selectedAnimalType == "Gato")
                {

                    Animales.Gato p = a as Animales.Gato;


                    GatoIngreso form2 = new GatoIngreso(a, p);

                    form2.ShowDialog();

                    if (form2.DialogResult == DialogResult.OK)
                    {

                        this.veterinaria.listaPacientes[indice] = (form2.animales);

                        this.ActualizarVisor();
                    }
                }
            }
        }
        private void modificarConejoForm(object sender, EventArgs e)
        {
            if (this.listBoxMenu.SelectedIndex > -1)
            {
                int selectedIndex = listBoxMenu.SelectedIndex;

                string selectedAnimalType = veterinaria.listaPacientes[selectedIndex].TipoDeAnimal;


                if (selectedAnimalType == "Conejo")
                {
                    int indice = this.listBoxMenu.SelectedIndex;
                    Animales.Animales a = this.veterinaria.listaPacientes[indice];
                    if (a is Animales.Conejo)
                    {

                        Animales.Conejo p = a as Animales.Conejo;

                        ConejoIngreso form2 = new ConejoIngreso(a, p);

                        form2.ShowDialog();

                        if (form2.DialogResult == DialogResult.OK)
                        {

                            this.veterinaria.listaPacientes[indice] = (form2.animales);

                            this.ActualizarVisor();
                        }
                    }

                }
            }

        }
        private void modificarProducto(object sender, EventArgs e)
        {
            // Verificar si hay un ítem seleccionado en el listBoxMenu
            if (this.listBoxMenu.SelectedIndex > -1)
            {
                int selectedIndex = listBoxMenu.SelectedIndex;

                // Verificar si el índice pertenece a la lista de comida
                if (selectedIndex < veterinaria.listaPacientes.Count + veterinaria.listaComida.Count)
                {
                    // El índice pertenece a la lista de comida
                    int comidaIndex = selectedIndex - veterinaria.listaPacientes.Count;
                    Animal.Comida a = this.veterinaria.listaComida[comidaIndex];

                    // Verificar si es comida y mostrar el formulario correspondiente
                    if (a is Animal.Comida)
                    {
                        Animal.Comida p = a as Animal.Comida;
                        ProductoIngreso form2 = new ProductoIngreso(p);
                        form2.ShowDialog();

                        if (form2.DialogResult == DialogResult.OK)
                        {
                            this.veterinaria.listaComida[comidaIndex] = form2.comida;
                            this.ActualizarVisor();
                        }
                    }
                }
            }
        }
        //Ordenar por Tipo
        /// <summary>
        /// Muestra en la lista solo los de tipoDeAnimal "Perro"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filtroTipoPerro(object sender, EventArgs e)
        {


            foreach (Animales.Animales paciente in veterinaria.listaPacientes)
            {
                if (paciente.TipoDeAnimal == "Perro")
                {
                    this.listBoxMenu.Items.Clear();
                    listBoxMenu.Items.Add(paciente.ToString());
                }
            }

        }
        /// <summary>
        /// Muestra en la lista solo los de tipoDeAnimal "Gato"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filtroTipoGato(object sender, EventArgs e)
        {


            foreach (Animales.Animales paciente in veterinaria.listaPacientes)
            {
                if (paciente.TipoDeAnimal == "Gato")
                {
                    this.listBoxMenu.Items.Clear();
                    listBoxMenu.Items.Add(paciente.ToString());
                }
            }

        }
        private void filtroTipoConejo(object sender, EventArgs e)
        {

            foreach (Animales.Animales paciente in veterinaria.listaPacientes)
            {
                if (paciente.TipoDeAnimal == "Conejo")
                {
                    this.listBoxMenu.Items.Clear();
                    listBoxMenu.Items.Add(paciente.ToString());
                }
            }

        }
        private void normalizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActualizarVisor();
        }

        //Ordenar elementos
        /// <summary>
        /// Ordena la lista de pacientes de la veterinaria de mayor a menor edad.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mayorAmenorOrdenarElementos(object sender, EventArgs e)
        {
            this.listBoxMenu.Items.Clear();


            var pacientesOrdenados = veterinaria.listaPacientes.OrderBy(paciente => paciente.edad);


            foreach (Animales.Animales paciente in pacientesOrdenados)
            {
                // Agrega el paciente ordenado a la lista.
                this.listBoxMenu.Items.Add($"{paciente.nombre} - Edad: {paciente.edad} - Raza: {paciente.raza} - Alimentacion: {paciente.alimentacion} - Tipo: {paciente.TipoDeAnimal} ");
            }
        }
        /// <summary>
        /// Ordena la lista de pacientes de la veterinaria de mayor a menor edad.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menorAmayorOrdenarElementos(object sender, EventArgs e)
        {
            this.listBoxMenu.Items.Clear();


            var pacientesOrdenados = veterinaria.listaPacientes.OrderByDescending(paciente => paciente.edad);


            foreach (Animales.Animales paciente in pacientesOrdenados)
            {
                // Agrega el paciente ordenado a la lista.
                this.listBoxMenu.Items.Add($"{paciente.nombre} - Edad: {paciente.edad} - Raza: {paciente.raza} - Alimentacion: {paciente.alimentacion} - Tipo: {paciente.TipoDeAnimal} ");
            }
        }
        private void validarUsuario()
        {
            switch (perfil)
            {
                case "administrador":

                    break;
                case "supervisor":
                    btnEliminarMenu.Enabled = false;
                    break;
                case "vendedor":
                    btnEliminarMenu.Enabled = false;
                    modificarToolStripMenuItem.Enabled = false;
                    agregarBoton.Enabled = false;
                    break;

            }
        }
        public void actualizarCrudBaseDatos()
        {
            Thread actualizarThread = new Thread(() =>
            {
                updateCrudBaseDatos actualizador = new updateCrudBaseDatos();
                actualizador.actualizarCrudBaseDatos(veterinaria);
                ActualizarVisor();
            });

            actualizarThread.Start();

            ActualizacionBaseDatosCompletaEvent?.Invoke();
        }
        public void eliminarElementoBaseDatos(string nombre)
        {
            Thread eliminarThread = new Thread(() =>
            {
                try
                {
                    conexion.Conectar();
                    string consulta = "DELETE FROM ANIMALES WHERE NOMBRE = @NombreAEliminar";
                    SqlCommand cmd = new SqlCommand(consulta, conexion.Conectar());
                    cmd.Parameters.AddWithValue("@NombreAEliminar", nombre);
                    cmd.ExecuteNonQuery();
                    nombre += " - Elemento Eliminado";
                    ElementoEliminadoEvent?.Invoke(nombre);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
            eliminarThread.Start();
        }
        public async Task guardarDatosAutomaticoAsync()
        {
            try
            {
                // Dominio de la maquina del usuario
                string directorioEjecutable = AppDomain.CurrentDomain.BaseDirectory;
                //Retrocedo de la carpeta bin
                string rutaRelativa = Path.Combine("..", "..", "..", "Registro.JSON");
                string filePath = Path.Combine(directorioEjecutable, rutaRelativa);

                List<ObjetoSerializado> listaSerializada = new List<ObjetoSerializado>();

                foreach (var paciente in veterinaria.listaPacientes)
                {
                    // Obtener el nombre del tipo de objeto (Perro, Gato, Conejo)
                    string tipo = paciente.GetType().Name;

                    // Crear un ObjetoSerializado con el tipo y los datos
                    var objetoSerializado = new ObjetoSerializado
                    {
                        Tipo = tipo,
                        Datos = paciente
                    };

                    listaSerializada.Add(objetoSerializado);
                }

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(listaSerializada, Newtonsoft.Json.Formatting.Indented);

                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new RegistroNoGuardado();
            }
        }


    }

}


