namespace WindFormCrud
{
    partial class MDIformularioMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIformularioMain));
            menuStrip = new MenuStrip();
            btnAbrirMenu = new ToolStripMenuItem();
            agregarBoton = new ToolStripMenuItem();
            btnPerroMenuAgregar = new ToolStripMenuItem();
            btnGatoMenuAgregar = new ToolStripMenuItem();
            btnConejoMenuAgregar = new ToolStripMenuItem();
            productosToolStripMenuItem = new ToolStripMenuItem();
            modificarBoton = new ToolStripMenuItem();
            btnEliminarMenu = new ToolStripMenuItem();
            ordenarMenu = new ToolStripMenuItem();
            edadesToolStripMenuItem = new ToolStripMenuItem();
            btnEdadesMayoraMenor = new ToolStripMenuItem();
            btnEdadesMenoraMayor = new ToolStripMenuItem();
            animalesToolStripMenuItem = new ToolStripMenuItem();
            btnOrdenarPerro = new ToolStripMenuItem();
            btnOrdenarGato = new ToolStripMenuItem();
            btnOrdenarConejo = new ToolStripMenuItem();
            normalizarToolStripMenuItem = new ToolStripMenuItem();
            registroLoginsToolStripMenuItem = new ToolStripMenuItem();
            toolStrip = new ToolStrip();
            btnGuardar = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            stripDateTime = new ToolStripButton();
            tipoUsuario = new ToolStripButton();
            nombreUsuario = new ToolStripButton();
            statusStrip = new StatusStrip();
            toolTip = new ToolTip(components);
            listBoxMenu = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            pictureBox1 = new PictureBox();
            label8 = new Label();
            menuStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.BackColor = Color.CornflowerBlue;
            menuStrip.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            menuStrip.Items.AddRange(new ToolStripItem[] { btnAbrirMenu, agregarBoton, modificarBoton, btnEliminarMenu, ordenarMenu, registroLoginsToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 2, 0, 2);
            menuStrip.Size = new Size(984, 28);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "MenuStrip";
            // 
            // btnAbrirMenu
            // 
            btnAbrirMenu.BackColor = Color.CornflowerBlue;
            btnAbrirMenu.ForeColor = SystemColors.ControlText;
            btnAbrirMenu.Name = "btnAbrirMenu";
            btnAbrirMenu.Size = new Size(54, 24);
            btnAbrirMenu.Text = "Abrir";
            btnAbrirMenu.Click += abrirArchivosJson;
            // 
            // agregarBoton
            // 
            agregarBoton.BackColor = Color.CornflowerBlue;
            agregarBoton.DropDownItems.AddRange(new ToolStripItem[] { btnPerroMenuAgregar, btnGatoMenuAgregar, btnConejoMenuAgregar, productosToolStripMenuItem });
            agregarBoton.ImageTransparentColor = SystemColors.ActiveBorder;
            agregarBoton.Name = "agregarBoton";
            agregarBoton.Size = new Size(75, 24);
            agregarBoton.Text = "&Agregar";
            // 
            // btnPerroMenuAgregar
            // 
            btnPerroMenuAgregar.Name = "btnPerroMenuAgregar";
            btnPerroMenuAgregar.Size = new Size(144, 24);
            btnPerroMenuAgregar.Text = "Perro";
            btnPerroMenuAgregar.Click += formIngresoPerro;
            // 
            // btnGatoMenuAgregar
            // 
            btnGatoMenuAgregar.Name = "btnGatoMenuAgregar";
            btnGatoMenuAgregar.Size = new Size(144, 24);
            btnGatoMenuAgregar.Text = "Gato";
            btnGatoMenuAgregar.Click += formIngresoGato;
            // 
            // btnConejoMenuAgregar
            // 
            btnConejoMenuAgregar.Name = "btnConejoMenuAgregar";
            btnConejoMenuAgregar.Size = new Size(144, 24);
            btnConejoMenuAgregar.Text = "Conejo";
            btnConejoMenuAgregar.Click += formIngresoConejo;
            // 
            // productosToolStripMenuItem
            // 
            productosToolStripMenuItem.Name = "productosToolStripMenuItem";
            productosToolStripMenuItem.Size = new Size(144, 24);
            productosToolStripMenuItem.Text = "Productos";
            productosToolStripMenuItem.Click += formIngresoProducto;
            // 
            // modificarBoton
            // 
            modificarBoton.Name = "modificarBoton";
            modificarBoton.Size = new Size(85, 24);
            modificarBoton.Text = "Modificar";
            modificarBoton.Click += ModificarElementos;
            // 
            // btnEliminarMenu
            // 
            btnEliminarMenu.Name = "btnEliminarMenu";
            btnEliminarMenu.Size = new Size(75, 24);
            btnEliminarMenu.Text = "Eliminar";
            btnEliminarMenu.Click += eliminarElementos;
            // 
            // ordenarMenu
            // 
            ordenarMenu.DropDownItems.AddRange(new ToolStripItem[] { edadesToolStripMenuItem, animalesToolStripMenuItem, normalizarToolStripMenuItem });
            ordenarMenu.Name = "ordenarMenu";
            ordenarMenu.Size = new Size(75, 24);
            ordenarMenu.Text = "&Ordenar";
            // 
            // edadesToolStripMenuItem
            // 
            edadesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { btnEdadesMayoraMenor, btnEdadesMenoraMayor });
            edadesToolStripMenuItem.Name = "edadesToolStripMenuItem";
            edadesToolStripMenuItem.Size = new Size(152, 24);
            edadesToolStripMenuItem.Text = "Edades";
            // 
            // btnEdadesMayoraMenor
            // 
            btnEdadesMayoraMenor.Name = "btnEdadesMayoraMenor";
            btnEdadesMayoraMenor.Size = new Size(183, 24);
            btnEdadesMayoraMenor.Text = "menor a Mayor ";
            btnEdadesMayoraMenor.Click += mayorAmenorOrdenarElementos;
            // 
            // btnEdadesMenoraMayor
            // 
            btnEdadesMenoraMayor.Name = "btnEdadesMenoraMayor";
            btnEdadesMenoraMayor.Size = new Size(183, 24);
            btnEdadesMenoraMayor.Text = "mayor a menor";
            btnEdadesMenoraMayor.Click += menorAmayorOrdenarElementos;
            // 
            // animalesToolStripMenuItem
            // 
            animalesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { btnOrdenarPerro, btnOrdenarGato, btnOrdenarConejo });
            animalesToolStripMenuItem.Name = "animalesToolStripMenuItem";
            animalesToolStripMenuItem.Size = new Size(152, 24);
            animalesToolStripMenuItem.Text = "Animales";
            // 
            // btnOrdenarPerro
            // 
            btnOrdenarPerro.Name = "btnOrdenarPerro";
            btnOrdenarPerro.Size = new Size(131, 24);
            btnOrdenarPerro.Text = "Perros";
            btnOrdenarPerro.Click += filtroTipoPerro;
            // 
            // btnOrdenarGato
            // 
            btnOrdenarGato.Name = "btnOrdenarGato";
            btnOrdenarGato.Size = new Size(131, 24);
            btnOrdenarGato.Text = "Gatos";
            btnOrdenarGato.Click += filtroTipoGato;
            // 
            // btnOrdenarConejo
            // 
            btnOrdenarConejo.Name = "btnOrdenarConejo";
            btnOrdenarConejo.Size = new Size(131, 24);
            btnOrdenarConejo.Text = "Conejos";
            btnOrdenarConejo.Click += filtroTipoConejo;
            // 
            // normalizarToolStripMenuItem
            // 
            normalizarToolStripMenuItem.Name = "normalizarToolStripMenuItem";
            normalizarToolStripMenuItem.Size = new Size(152, 24);
            normalizarToolStripMenuItem.Text = "Normalizar";
            normalizarToolStripMenuItem.Click += normalizarLista;
            // 
            // registroLoginsToolStripMenuItem
            // 
            registroLoginsToolStripMenuItem.Name = "registroLoginsToolStripMenuItem";
            registroLoginsToolStripMenuItem.Size = new Size(123, 24);
            registroLoginsToolStripMenuItem.Text = "Registro Logins";
            registroLoginsToolStripMenuItem.Click += registroLoginsToolStripMenuItem_Click;
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { btnGuardar, toolStripSeparator1, stripDateTime, tipoUsuario, nombreUsuario });
            toolStrip.Location = new Point(0, 28);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(984, 25);
            toolStrip.TabIndex = 1;
            toolStrip.Text = "ToolStrip";
            // 
            // btnGuardar
            // 
            btnGuardar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnGuardar.Image = (Image)resources.GetObject("btnGuardar.Image");
            btnGuardar.ImageTransparentColor = Color.Black;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(23, 22);
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += saveToolStripButton_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // stripDateTime
            // 
            stripDateTime.Alignment = ToolStripItemAlignment.Right;
            stripDateTime.DisplayStyle = ToolStripItemDisplayStyle.Text;
            stripDateTime.Image = (Image)resources.GetObject("stripDateTime.Image");
            stripDateTime.ImageTransparentColor = Color.Magenta;
            stripDateTime.Name = "stripDateTime";
            stripDateTime.Size = new Size(98, 22);
            stripDateTime.Text = "toolStripButton1";
            // 
            // tipoUsuario
            // 
            tipoUsuario.Alignment = ToolStripItemAlignment.Right;
            tipoUsuario.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tipoUsuario.Image = (Image)resources.GetObject("tipoUsuario.Image");
            tipoUsuario.ImageTransparentColor = Color.Magenta;
            tipoUsuario.Name = "tipoUsuario";
            tipoUsuario.Size = new Size(98, 22);
            tipoUsuario.Text = "toolStripButton2";
            // 
            // nombreUsuario
            // 
            nombreUsuario.Alignment = ToolStripItemAlignment.Right;
            nombreUsuario.DisplayStyle = ToolStripItemDisplayStyle.Text;
            nombreUsuario.Image = (Image)resources.GetObject("nombreUsuario.Image");
            nombreUsuario.ImageTransparentColor = Color.Magenta;
            nombreUsuario.Name = "nombreUsuario";
            nombreUsuario.Size = new Size(98, 22);
            nombreUsuario.Text = "toolStripButton2";
            // 
            // statusStrip
            // 
            statusStrip.Location = new Point(0, 611);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(984, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "StatusStrip";
            // 
            // listBoxMenu
            // 
            listBoxMenu.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxMenu.FormattingEnabled = true;
            listBoxMenu.ItemHeight = 28;
            listBoxMenu.Location = new Point(356, 101);
            listBoxMenu.Name = "listBoxMenu";
            listBoxMenu.Size = new Size(616, 424);
            listBoxMenu.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(358, 83);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 6;
            label1.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(414, 83);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 7;
            label2.Text = "Edad";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(453, 83);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 8;
            label3.Text = "Raza";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(490, 83);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 9;
            label4.Text = "Alimentacion";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(574, 83);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 11;
            label5.Text = "Atributo 1";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(640, 83);
            label6.Name = "label6";
            label6.Size = new Size(60, 15);
            label6.TabIndex = 12;
            label6.Text = "Atributo 2";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(941, 71);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(31, 27);
            pictureBox1.TabIndex = 20;
            pictureBox1.TabStop = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(852, 83);
            label8.Name = "label8";
            label8.Size = new Size(83, 15);
            label8.TabIndex = 21;
            label8.Text = "Base de Datos:";
            // 
            // MDIformularioMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(984, 633);
            Controls.Add(label8);
            Controls.Add(pictureBox1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBoxMenu);
            Controls.Add(statusStrip);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            IsMdiContainer = true;
            MainMenuStrip = menuStrip;
            Margin = new Padding(4, 3, 4, 3);
            Name = "MDIformularioMain";
            Text = "Veterinaria";
            FormClosing += MDIParent1_FormClosing;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion


        private MenuStrip menuStrip;
        private ToolStrip toolStrip;
        private StatusStrip statusStrip;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem agregarBoton;
        private ToolStripMenuItem ordenarMenu;
        private ToolTip toolTip;
        private ToolStripMenuItem btnPerroMenuAgregar;
        private ToolStripMenuItem btnGatoMenuAgregar;
        private ToolStripMenuItem btnConejoMenuAgregar;
        private ToolStripButton btnGuardar;
        private ListBox listBoxMenu;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ToolStripMenuItem edadesToolStripMenuItem;
        private ToolStripMenuItem btnEdadesMayoraMenor;
        private ToolStripMenuItem btnEdadesMenoraMayor;
        private ToolStripMenuItem animalesToolStripMenuItem;
        private ToolStripMenuItem btnOrdenarPerro;
        private ToolStripMenuItem btnOrdenarGato;
        private ToolStripMenuItem btnOrdenarConejo;
        private ToolStripMenuItem btnAbrirMenu;
        private Label label5;
        private Label label6;
        private ToolStripButton stripDateTime;
        private ToolStripButton nombreUsuario;
        private ToolStripMenuItem productosToolStripMenuItem;
        private ToolStripMenuItem normalizarToolStripMenuItem;
        private PictureBox pictureBox1;
        private Label label8;
        private ToolStripButton tipoUsuario;
        private ToolStripMenuItem registroLoginsToolStripMenuItem;
        private ToolStripMenuItem modificarBoton;
        private ToolStripMenuItem btnEliminarMenu;
    }
}



