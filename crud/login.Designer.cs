namespace WindFormCrud
{
    partial class login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            textUser = new TextBox();
            textPass = new TextBox();
            btnAceptar = new Button();
            btnReiniciar = new Button();
            btnCancelar = new Button();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(86, 121);
            label1.Name = "label1";
            label1.Size = new Size(58, 21);
            label1.TabIndex = 0;
            label1.Text = "Correo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(67, 155);
            label2.Name = "label2";
            label2.Size = new Size(89, 21);
            label2.TabIndex = 1;
            label2.Text = "Contraseña";
            // 
            // textUser
            // 
            textUser.Location = new Point(162, 119);
            textUser.Name = "textUser";
            textUser.Size = new Size(100, 23);
            textUser.TabIndex = 2;
            // 
            // textPass
            // 
            textPass.Location = new Point(162, 153);
            textPass.Name = "textPass";
            textPass.PasswordChar = '*';
            textPass.Size = new Size(100, 23);
            textPass.TabIndex = 3;
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.DarkGreen;
            btnAceptar.ForeColor = Color.MintCream;
            btnAceptar.Location = new Point(57, 199);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(102, 28);
            btnAceptar.TabIndex = 4;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnReiniciar
            // 
            btnReiniciar.ForeColor = SystemColors.ActiveCaptionText;
            btnReiniciar.Location = new Point(57, 233);
            btnReiniciar.Name = "btnReiniciar";
            btnReiniciar.Size = new Size(75, 23);
            btnReiniciar.TabIndex = 5;
            btnReiniciar.Text = "Reiniciar";
            btnReiniciar.UseVisualStyleBackColor = true;
            btnReiniciar.Click += btnReiniciar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.IndianRed;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(187, 199);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(102, 28);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += buttonCancelar_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(282, 278);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 7;
            label3.Text = "Intentos:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(348, 278);
            label4.Name = "label4";
            label4.Size = new Size(13, 15);
            label4.TabIndex = 8;
            label4.Text = "0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.CornflowerBlue;
            label5.Location = new Point(105, 52);
            label5.Name = "label5";
            label5.Size = new Size(184, 37);
            label5.TabIndex = 9;
            label5.Text = "BIENVENIDOS";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(95, 89);
            label6.Name = "label6";
            label6.Size = new Size(204, 21);
            label6.TabIndex = 10;
            label6.Text = "VETERINARIA PET ANIMALS";
            // 
            // login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(373, 302);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(btnCancelar);
            Controls.Add(btnReiniciar);
            Controls.Add(btnAceptar);
            Controls.Add(textPass);
            Controls.Add(textUser);
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlText;
            Name = "login";
            Text = "INGRESO USUARIO";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textUser;
        private TextBox textPass;
        private Button btnAceptar;
        private Button btnReiniciar;
        private Button btnCancelar;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}