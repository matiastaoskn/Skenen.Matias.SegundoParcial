﻿using Animal;
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

        private Thread tiempoThread;
        private bool detenerHilo = false;
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


            tiempoThread = new Thread(MostrarTiempo);
            tiempoThread.Start();

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
                }

                foreach (Producto paciente in veterinaria.listaComida)
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
        private async void eliminarElementos(object sender, EventArgs e)
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
                else
                {
                    MessageBox.Show("Seleccione un Elemento");
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
            DetenerHilo();
            if (MessageBox.Show("¿Está seguro de que desea cerrar la aplicación?", "Cerrar Aplicación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                // Si el usuario elige "No", cancelar el cierre del formulario.

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
                    modificarBoton.Enabled = false;
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
            catch (RegistroNoGuardadoException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error de registro TXT", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Maneja el evento de eliminación de un elemento, actualizando la vista del formulario principal después de eliminar un elemento.
        /// </summary>
        private void MDIformularioMain_ElementoEliminadoEvent(string nombreElemento)
        {
            ActualizarVisor();
        }
        /// <summary>
        /// Actualiza continuamente un control con la hora actual en la interfaz de usuario.
        /// </summary>
        private void MostrarTiempo()
        {
            while (!detenerHilo)
            {
                if (IsHandleCreated)
                {
                    // Verifica si el identificador de ventana está creado antes de usar Invoke
                    Invoke(new Action(() => stripDateTime.Text = DateTime.Now.ToString()));
                }
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// Detiene el hilo creado por la función MostrarTiempo.
        /// </summary>
        private void DetenerHilo()
        {
            detenerHilo = true;
            tiempoThread.Join();  // Espera a que el hilo termine
        }
        /// <summary>
        /// Maneja el evento de clic en la opción de menú "registroLoginsToolStripMenuItem".
        /// Abre y muestra el formulario de registro de logins como un formulario secundario en el formulario principal MDI actual.
        /// </summary>
        private void registroLoginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistroLogs registro = new RegistroLogs();
            registro.MdiParent = this; // Establecer el formulario principal como el formulario MDI padre
            registro.Show();
        }
        /// <summary>
        /// Maneja el evento de modificación de elementos en respuesta a la selección de un ítem en listBoxMenu.
        /// Determina si el índice seleccionado corresponde a un paciente o a una comida y llama al método correspondiente de modificación.
        /// </summary>
        private void ModificarElementos(object sender, EventArgs e)
        {
            if (this.listBoxMenu.SelectedIndex > -1)
            {
                int indice = listBoxMenu.SelectedIndex;

                if (indice < veterinaria.listaPacientes.Count)
                {
                    ModificarPaciente(indice);
                }
                else if (indice < veterinaria.listaPacientes.Count + veterinaria.listaComida.Count)
                {
                    ModificarComida(indice);
                }
            }
        }
        /// <summary>
        /// Modifica un paciente en la lista de pacientes de la veterinaria según su tipo de animal.
        /// Invoca métodos específicos de modificación para diferentes tipos de animales (Conejo, Gato, Perro).
        /// </summary>
        private void ModificarPaciente(int indice)
        {
            string selectedAnimalType = veterinaria.listaPacientes[indice].TipoDeAnimal;
            Animales.Animales a = this.veterinaria.listaPacientes[indice];

            if (selectedAnimalType == "Conejo" && a is Animales.Conejo)
            {
                Animales.Conejo p = a as Animales.Conejo;
                ConejoIngreso formularioModificacionConejo = new ConejoIngreso(a, p);
                MostrarYActualizar(formularioModificacionConejo, indice);
            }
            else if (selectedAnimalType == "Gato" && a is Animales.Gato)
            {
                Animales.Gato p = a as Animales.Gato;
                GatoIngreso formularioModificacionGato = new GatoIngreso(a, p);
                MostrarYActualizar(formularioModificacionGato, indice);
            }
            else if (selectedAnimalType == "Perro" && a is Animales.Perro)
            {
                Animales.Perro p = a as Animales.Perro;
                PerroIngreso formularioModificacionPerro = new PerroIngreso(a, p);
                MostrarYActualizar(formularioModificacionPerro, indice);
            }
        }
        /// <summary>
        /// Modifica un elemento de comida en la lista de comida de la veterinaria.
        /// Crea un formulario de modificación para el elemento de comida, lo muestra y actualiza la lista.
        /// </summary>
        private void ModificarComida(int indice)
        {
            int comidaIndex = indice - veterinaria.listaPacientes.Count;
            Animal.Producto elemento = this.veterinaria.listaComida[comidaIndex];

            if (elemento is Animal.Producto)
            {
                Animal.Producto p = elemento as Animal.Producto;
                ProductoIngreso formularioModificacionComida = new ProductoIngreso(p);
                MostrarYActualizar(formularioModificacionComida, comidaIndex);
            }
        }
        /// <summary>
        /// Muestra un formulario y actualiza la lista correspondiente en la veterinaria según el resultado del formulario.
        /// </summary>
        private void MostrarYActualizar(Form form, int index)
        {
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                if (form is ConejoIngreso)
                {
                    this.veterinaria.listaPacientes[index] = ((ConejoIngreso)form).animales;
                }
                else if (form is GatoIngreso)
                {
                    this.veterinaria.listaPacientes[index] = ((GatoIngreso)form).animales;
                }
                else if (form is PerroIngreso)
                {
                    this.veterinaria.listaPacientes[index] = ((PerroIngreso)form).animales;
                }
                else if (form is ProductoIngreso)
                {
                    this.veterinaria.listaComida[index] = ((ProductoIngreso)form).Producto;
                }
                this.ActualizarVisor();
            }
        }
    }
}


