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
            fileMenu = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripSeparator5 = new ToolStripSeparator();
            btnPerroMenuAgregar = new ToolStripMenuItem();
            btnGatoMenuAgregar = new ToolStripMenuItem();
            btnConejoMenuAgregar = new ToolStripMenuItem();
            productosToolStripMenuItem = new ToolStripMenuItem();
            editMenu = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            toolStripSeparator7 = new ToolStripSeparator();
            modificarToolStripMenuItem = new ToolStripMenuItem();
            btnModificarPerro = new ToolStripMenuItem();
            btnModificarGato = new ToolStripMenuItem();
            btnModificarConejo = new ToolStripMenuItem();
            pToolStripMenuItem = new ToolStripMenuItem();
            btnEliminarMenu = new ToolStripMenuItem();
            ordenarMenu = new ToolStripMenuItem();
            edadesToolStripMenuItem = new ToolStripMenuItem();
            btnEdadesMayoraMenor = new ToolStripMenuItem();
            btnEdadesMenoraMayor = new ToolStripMenuItem();
            animalesToolStripMenuItem = new ToolStripMenuItem();
            btnOrdenarPerro = new ToolStripMenuItem();
            btnOrdenarGato = new ToolStripMenuItem();
            btnOrdenarConejo = new ToolStripMenuItem();
            toolStrip = new ToolStrip();
            btnGuardar = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            stripDateTime = new ToolStripButton();
            stripUser = new ToolStripButton();
            statusStrip = new StatusStrip();
            toolTip = new ToolTip(components);
            listBoxMenu = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            menuStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.BackColor = Color.CornflowerBlue;
            menuStrip.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            menuStrip.Items.AddRange(new ToolStripItem[] { btnAbrirMenu, fileMenu, editMenu, ordenarMenu });
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
            btnAbrirMenu.Click += abrirToolStripMenuItem_Click;
            // 
            // fileMenu
            // 
            fileMenu.BackColor = Color.CornflowerBlue;
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { toolStripSeparator3, toolStripSeparator4, toolStripSeparator5, btnPerroMenuAgregar, btnGatoMenuAgregar, btnConejoMenuAgregar, productosToolStripMenuItem });
            fileMenu.ImageTransparentColor = SystemColors.ActiveBorder;
            fileMenu.Name = "fileMenu";
            fileMenu.Size = new Size(75, 24);
            fileMenu.Text = "&Agregar";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(141, 6);
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(141, 6);
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(141, 6);
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
            productosToolStripMenuItem.Click += productosToolStripMenuItem_Click;
            // 
            // editMenu
            // 
            editMenu.DropDownItems.AddRange(new ToolStripItem[] { toolStripSeparator6, toolStripSeparator7, modificarToolStripMenuItem, btnEliminarMenu });
            editMenu.Name = "editMenu";
            editMenu.Size = new Size(60, 24);
            editMenu.Text = "&Editar";
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(177, 6);
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(177, 6);
            // 
            // modificarToolStripMenuItem
            // 
            modificarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { btnModificarPerro, btnModificarGato, btnModificarConejo, pToolStripMenuItem });
            modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            modificarToolStripMenuItem.Size = new Size(180, 24);
            modificarToolStripMenuItem.Text = "Modificar";
            // 
            // btnModificarPerro
            // 
            btnModificarPerro.Name = "btnModificarPerro";
            btnModificarPerro.Size = new Size(180, 24);
            btnModificarPerro.Text = "Perros";
            btnModificarPerro.Click += modificarPerroForm;
            // 
            // btnModificarGato
            // 
            btnModificarGato.Name = "btnModificarGato";
            btnModificarGato.Size = new Size(180, 24);
            btnModificarGato.Text = "Gatos";
            btnModificarGato.Click += modificarGatoForm;
            // 
            // btnModificarConejo
            // 
            btnModificarConejo.Name = "btnModificarConejo";
            btnModificarConejo.Size = new Size(180, 24);
            btnModificarConejo.Text = "Conejos";
            btnModificarConejo.Click += modificarConejoForm;
            // 
            // pToolStripMenuItem
            // 
            pToolStripMenuItem.Name = "pToolStripMenuItem";
            pToolStripMenuItem.Size = new Size(180, 24);
            pToolStripMenuItem.Text = "Producto";
            pToolStripMenuItem.Click += modificarProducto;
            // 
            // btnEliminarMenu
            // 
            btnEliminarMenu.Name = "btnEliminarMenu";
            btnEliminarMenu.Size = new Size(180, 24);
            btnEliminarMenu.Text = "Eliminar";
            btnEliminarMenu.Click += eliminarToolStripMenuItem_Click;
            // 
            // ordenarMenu
            // 
            ordenarMenu.DropDownItems.AddRange(new ToolStripItem[] { edadesToolStripMenuItem, animalesToolStripMenuItem });
            ordenarMenu.Name = "ordenarMenu";
            ordenarMenu.Size = new Size(75, 24);
            ordenarMenu.Text = "&Ordenar";
            // 
            // edadesToolStripMenuItem
            // 
            edadesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { btnEdadesMayoraMenor, btnEdadesMenoraMayor });
            edadesToolStripMenuItem.Name = "edadesToolStripMenuItem";
            edadesToolStripMenuItem.Size = new Size(139, 24);
            edadesToolStripMenuItem.Text = "Edades";
            // 
            // btnEdadesMayoraMenor
            // 
            btnEdadesMayoraMenor.Name = "btnEdadesMayoraMenor";
            btnEdadesMayoraMenor.Size = new Size(179, 24);
            btnEdadesMayoraMenor.Text = "Mayor a menor";
            btnEdadesMayoraMenor.Click += mayorAMenorToolStripMenuItem_Click;
            // 
            // btnEdadesMenoraMayor
            // 
            btnEdadesMenoraMayor.Name = "btnEdadesMenoraMayor";
            btnEdadesMenoraMayor.Size = new Size(179, 24);
            btnEdadesMenoraMayor.Text = "Menor a mayor";
            btnEdadesMenoraMayor.Click += menorAMayorToolStripMenuItem_Click;
            // 
            // animalesToolStripMenuItem
            // 
            animalesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { btnOrdenarPerro, btnOrdenarGato, btnOrdenarConejo });
            animalesToolStripMenuItem.Name = "animalesToolStripMenuItem";
            animalesToolStripMenuItem.Size = new Size(139, 24);
            animalesToolStripMenuItem.Text = "Animales";
            // 
            // btnOrdenarPerro
            // 
            btnOrdenarPerro.Name = "btnOrdenarPerro";
            btnOrdenarPerro.Size = new Size(131, 24);
            btnOrdenarPerro.Text = "Perros";
            btnOrdenarPerro.Click += perrosToolStripMenuItem_Click;
            // 
            // btnOrdenarGato
            // 
            btnOrdenarGato.Name = "btnOrdenarGato";
            btnOrdenarGato.Size = new Size(131, 24);
            btnOrdenarGato.Text = "Gatos";
            btnOrdenarGato.Click += gatosToolStripMenuItem_Click;
            // 
            // btnOrdenarConejo
            // 
            btnOrdenarConejo.Name = "btnOrdenarConejo";
            btnOrdenarConejo.Size = new Size(131, 24);
            btnOrdenarConejo.Text = "Conejos";
            btnOrdenarConejo.Click += conejosToolStripMenuItem1_Click;
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { btnGuardar, toolStripSeparator1, stripDateTime, stripUser });
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
            // stripUser
            // 
            stripUser.Alignment = ToolStripItemAlignment.Right;
            stripUser.DisplayStyle = ToolStripItemDisplayStyle.Text;
            stripUser.Image = (Image)resources.GetObject("stripUser.Image");
            stripUser.ImageTransparentColor = Color.Magenta;
            stripUser.Name = "stripUser";
            stripUser.Size = new Size(98, 22);
            stripUser.Text = "toolStripButton2";
            // 
            // statusStrip
            // 
            statusStrip.Location = new Point(0, 566);
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
            listBoxMenu.Location = new Point(356, 103);
            listBoxMenu.Name = "listBoxMenu";
            listBoxMenu.Size = new Size(616, 424);
            listBoxMenu.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(358, 85);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 6;
            label1.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(414, 85);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 7;
            label2.Text = "Edad";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(453, 85);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 8;
            label3.Text = "Raza";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(490, 85);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 9;
            label4.Text = "Alimentacion";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(574, 85);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 11;
            label5.Text = "Atributo 1";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(640, 85);
            label6.Name = "label6";
            label6.Size = new Size(60, 15);
            label6.TabIndex = 12;
            label6.Text = "Atributo 2";
            // 
            // MDIformularioMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(984, 588);
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
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem editMenu;
        private ToolStripMenuItem ordenarMenu;
        private ToolTip toolTip;
        private ToolStripMenuItem btnPerroMenuAgregar;
        private ToolStripMenuItem btnGatoMenuAgregar;
        private ToolStripMenuItem btnConejoMenuAgregar;
        private ToolStripButton btnGuardar;
        private ListBox listBoxMenu;
        private ToolStripMenuItem modificarToolStripMenuItem;
        private ToolStripMenuItem btnEliminarMenu;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ToolStripMenuItem btnModificarPerro;
        private ToolStripMenuItem btnModificarGato;
        private ToolStripMenuItem btnModificarConejo;
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
        private ToolStripButton stripUser;
        private ToolStripMenuItem productosToolStripMenuItem;
        private ToolStripMenuItem pToolStripMenuItem;
    }
}



