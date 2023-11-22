using Animal;
using Animales;
using CrudVeterinaria;
using System.Data.SqlClient;
using WindFormCrud.Ingresos;

namespace WindFormCrud
{
    public partial class MDIformularioMain : AnimalIngresoForm, IOperacionesBaseDatos
    {

        private string perfil = UserNameLogin.TipoPerfil;
        private Veterinaria<Animales.Animales, Producto> veterinaria;

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
            this.StartPosition = FormStartPosition.CenterScreen;
            validarUsuario();
            veterinaria = new Veterinaria<Animales.Animales, Producto>(10);
            actualizarCrudBaseDatos();

            nombreUsuario.Text = UserNameLogin.UserName;
            tipoUsuario.Text = UserNameLogin.TipoPerfil;
            stripDateTime.Text = DateTime.Now.ToString();

            ElementoEliminadoEvent += MDIformularioMain_ElementoEliminadoEvent;

        }
        /// <summary>
        /// Actualiza el visor de la interfaz de usuario, limpiando y llenando un ListBox con la información de pacientes y productos.
        /// Si se llama desde un subproceso diferente, invoca al hilo principal y guarda automáticamente los datos.
        /// </summary>
        private async void ActualizarVisor()
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
                    barraprogreso();
                }

                foreach (Producto paciente in veterinaria.listaComida)
                {
                    listBoxMenu.Items.Add(paciente.ToString());
                    barraprogreso();

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

                    this.veterinaria.listaComida.Add(IngresoProducto.Producto);
                }

                this.ActualizarVisor();
            }

        }
        /// <summary>
        /// Permite al usuario eliminar el elemento seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
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
                            await cargarImagenAsync();
                            await eliminarElementoBaseDatos(nombreElementoAEliminar);
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
                                await cargarImagenAsync();
                                await eliminarElementoBaseDatos(nombreElementoAEliminar);
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
        /// Abre un archivo JSON seleccionado mediante un cuadro de diálogo de apertura de archivos.
        /// Lee el contenido del archivo JSON, deserializa la lista de objetos serializados y los agrega a la lista de pacientes de la veterinaria.
        /// Actualiza el visor después de la operación.
        /// </summary>
        private void abrirArchivosJson(object sender, EventArgs e)
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
        /// Abre un formulario de ingreso de datos para un perro.
        /// Configura el formulario como hijo del formulario principal.
        /// Captura el evento FormClosed para ejecutar acciones específicas cuando el formulario se cierra.
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
        /// <summary>
        /// Abre un formulario de ingreso de datos para un gato.
        /// Configura el formulario como hijo del formulario principal.
        /// Captura el evento FormClosed para ejecutar acciones específicas cuando el formulario se cierra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formIngresoGato(object sender, EventArgs e)
        {
            GatoIngreso formIngresoGato = new GatoIngreso();
            formIngresoGato.MdiParent = this;

            formIngresoGato.FormClosed += capturaEventoFormClosed;

            formIngresoGato.Show();
        }
        /// <summary>
        /// Abre un formulario de ingreso de datos para un conejo.
        /// Configura el formulario como hijo del formulario principal.
        /// Captura el evento FormClosed para ejecutar acciones específicas cuando el formulario se cierra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formIngresoConejo(object sender, EventArgs e)
        {
            ConejoIngreso formIngresoConejo = new ConejoIngreso();
            formIngresoConejo.MdiParent = this;

            formIngresoConejo.FormClosed += capturaEventoFormClosed;

            formIngresoConejo.Show();
        }
        /// <summary>
        /// Abre un formulario de ingreso de datos para un producto.
        /// Configura el formulario como hijo del formulario principal.
        /// Captura el evento FormClosed para ejecutar acciones específicas cuando el formulario se cierra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formIngresoProducto(object sender, EventArgs e)
        {
            ProductoIngreso formIngresoProducto = new ProductoIngreso();
            formIngresoProducto.MdiParent = this;

            formIngresoProducto.FormClosed += capturaEventoFormClosed;

            formIngresoProducto.Show();
        }
        // Modificar animales
        /// <summary>
        /// Abre un formulario de modificación de datos para un perro si hay un perro seleccionado en la lista.
        /// Obtiene el perro seleccionado, crea un formulario de ingreso de perro con los datos existentes y lo muestra.
        /// Actualiza la lista de pacientes si se confirma la modificación desde el formulario de ingreso.
        /// </summary>
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
        /// <summary>
        /// Abre un formulario de modificación de datos para un gato si hay un "gato" seleccionado en la lista.
        /// Obtiene el "gato" seleccionado, crea un formulario de ingreso de "gato" con los datos existentes y lo muestra.
        /// Actualiza la lista de pacientes si se confirma la modificación desde el formulario de ingreso.
        /// </summary>
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
        /// <summary>
        /// Abre un formulario de modificación de datos para un conejo si hay un "conejo" seleccionado en la lista.
        /// Obtiene el "conejo" seleccionado, crea un formulario de ingreso de "conejo" con los datos existentes y lo muestra.
        /// Actualiza la lista de pacientes si se confirma la modificación desde el formulario de ingreso.
        /// </summary>
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
        /// <summary>
        /// Obtiene el "producto" seleccionado, crea un formulario de ingreso de "producto" con los datos existentes y lo muestra.
        /// Actualiza la lista de pacientes si se confirma la modificación desde el formulario de ingreso.
        /// </summary>
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
                    Animal.Producto a = this.veterinaria.listaComida[comidaIndex];

                    // Verificar si es comida y mostrar el formulario correspondiente
                    if (a is Animal.Producto)
                    {
                        Animal.Producto p = a as Animal.Producto;
                        ProductoIngreso form2 = new ProductoIngreso(p);
                        form2.ShowDialog();

                        if (form2.DialogResult == DialogResult.OK)
                        {
                            this.veterinaria.listaComida[comidaIndex] = form2.Producto;
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
        /// <summary>
        /// Filtra y muestra en el listBox solo los pacientes de tipo "Conejo".
        /// Limpia el listBox antes de agregar los resultados del filtro.
        /// </summary>
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
        /// <summary>
        /// Actualiza el visor para mostrar la lista de pacientes en su estado actual.
        /// </summary>
        private void normalizarLista(object sender, EventArgs e)
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
        /// <summary>
        /// Valida el perfil del usuario y ajusta la habilitación de ciertos controles 
        /// en función del perfil.
        /// </summary>
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
        /// <summary>
        /// Inicia un hilo para actualizar la base de datos a través de un objeto 'updateCrudBaseDatos'.
        /// Después de la actualización, se llama a ActualizarVisor() y se dispara el evento ActualizacionBaseDatosCompletaEvent.
        /// </summary>
        public async void actualizarCrudBaseDatos()
        {
            await Task.Run(() =>
            {
                updateCrudBaseDatos actualizador = new updateCrudBaseDatos();
                actualizador.actualizarCrudBaseDatos(veterinaria);
                ActualizarVisor();
                cargarImagenAsync();
            });


            ActualizacionBaseDatosCompletaEvent?.Invoke();
        }
        /// <summary>
        /// Inicia un hilo para eliminar un elemento de la base de datos según el nombre proporcionado.
        /// Después de la eliminación, se dispara el evento ElementoEliminadoEvent con el nombre del elemento eliminado.
        /// </summary>
        public async Task eliminarElementoBaseDatos(string nombre)
        {
            await Task.Run(() =>
            {
                try
                {
                    conexcionBaseDatos.Conectar();
                    string consulta = "DELETE FROM ANIMALES WHERE NOMBRE = @NombreAEliminar";
                    SqlCommand cmd = new SqlCommand(consulta, conexcionBaseDatos.Conectar());
                    cmd.Parameters.AddWithValue("@NombreAEliminar", nombre);
                    cmd.ExecuteNonQuery();
                    ElementoEliminadoEvent?.Invoke(nombre);
                    MessageBox.Show("DATO ELIMINADO");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
        /// <summary>
        /// Guarda de forma asíncrona los datos de la lista de pacientes de la veterinaria en un archivo JSON.
        /// </summary>
        /// <returns>Una tarea que representa la operación de guardado asíncrono.</returns>
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
        /// <summary>
        /// Carga una imagen de forma asíncrona en un PictureBox según el estado proporcionado.
        /// </summary>
        private async Task cargarImagenAsync()
        {
            // Dominio de la maquina del usuario
            string directorioEjecutable = AppDomain.CurrentDomain.BaseDirectory;
            // Retrocedo de la carpeta bin
            string rutaRelativa = Path.Combine("..", "..", "..", "verificado.png");
            if (!string.IsNullOrEmpty(rutaRelativa))
            {
                string filePath = Path.Combine(directorioEjecutable, rutaRelativa);

                // Cargar la imagen original de forma asíncrona
                await Task.Run(() =>
                {
                    Image imagenOriginal = Image.FromFile(filePath);

                    int ancho = 30;
                    int altura = 30;
                    Image imagenPequeña = imagenOriginal.GetThumbnailImage(ancho, altura, null, IntPtr.Zero);

                    pictureBox1.Image = imagenPequeña;

                    // Esperar dos segundos
                    Task.Delay(2000).Wait();

                    // Establecer la imagen del pictureBox en null después de dos segundos
                    pictureBox1.Image = null;
                });
            }
        }
        /// <summary>
        /// Realiza una operación adicional después de actualizar la base de datos, como cargar una imagen de confirmación.
        /// </summary>


        private void barraprogreso()
        {
            // Inicializa la barra de progreso
            progressBar1.Value = 0;

            int totalElementos = 10;

            Thread thread = new Thread(() =>
            {

                for (int i = 0; i < totalElementos; i++)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        int progreso = (int)((veterinaria.listaPacientes.Count / (double)totalElementos) * 100);
                        progressBar1.Value = progreso;

                        this.SetStyle(ControlStyles.UserPaint, true);
                        progressBar1.ForeColor = Color.Yellow;

                    });

                }

                if (veterinaria.listaPacientes.Count == 10)
                {
                    // Muestra un mensaje al completar
                    MessageBox.Show("Lista de pacientes completada");
                    progressBar1.Value = 0;
                }
            });

            thread.Start();
        }

        /// <summary>
        /// Maneja el evento de eliminación de un elemento, actualizando la vista del formulario principal después de eliminar un elemento.
        /// </summary>
        private void MDIformularioMain_ElementoEliminadoEvent(string nombreElemento)
        {
            ActualizarVisor();
        }
    }

}


