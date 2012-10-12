namespace ClienteTeste
{
    partial class FormCliente
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
            this.buttonDesconectar = new System.Windows.Forms.Button();
            this.buttonConectar = new System.Windows.Forms.Button();
            this.textBoxPorta = new System.Windows.Forms.TextBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.buttonEnviar = new System.Windows.Forms.Button();
            this.textBoxEnviar = new System.Windows.Forms.TextBox();
            this.textBoxMsgs = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonDesconectar
            // 
            this.buttonDesconectar.Location = new System.Drawing.Point(442, 327);
            this.buttonDesconectar.Name = "buttonDesconectar";
            this.buttonDesconectar.Size = new System.Drawing.Size(80, 23);
            this.buttonDesconectar.TabIndex = 15;
            this.buttonDesconectar.Text = "Desconectar";
            this.buttonDesconectar.UseVisualStyleBackColor = true;
            this.buttonDesconectar.Click += new System.EventHandler(this.buttonDesconectar_Click);
            // 
            // buttonConectar
            // 
            this.buttonConectar.Location = new System.Drawing.Point(361, 327);
            this.buttonConectar.Name = "buttonConectar";
            this.buttonConectar.Size = new System.Drawing.Size(75, 23);
            this.buttonConectar.TabIndex = 14;
            this.buttonConectar.Text = "Conectar";
            this.buttonConectar.UseVisualStyleBackColor = true;
            this.buttonConectar.Click += new System.EventHandler(this.buttonConectar_Click);
            // 
            // textBoxPorta
            // 
            this.textBoxPorta.Location = new System.Drawing.Point(285, 327);
            this.textBoxPorta.Name = "textBoxPorta";
            this.textBoxPorta.Size = new System.Drawing.Size(73, 20);
            this.textBoxPorta.TabIndex = 13;
            this.textBoxPorta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(12, 327);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(270, 20);
            this.textBoxIP.TabIndex = 12;
            // 
            // buttonEnviar
            // 
            this.buttonEnviar.Location = new System.Drawing.Point(447, 283);
            this.buttonEnviar.Name = "buttonEnviar";
            this.buttonEnviar.Size = new System.Drawing.Size(75, 23);
            this.buttonEnviar.TabIndex = 11;
            this.buttonEnviar.Text = "Enviar";
            this.buttonEnviar.UseVisualStyleBackColor = true;
            this.buttonEnviar.Click += new System.EventHandler(this.buttonEnviar_Click);
            // 
            // textBoxEnviar
            // 
            this.textBoxEnviar.Location = new System.Drawing.Point(12, 283);
            this.textBoxEnviar.Name = "textBoxEnviar";
            this.textBoxEnviar.Size = new System.Drawing.Size(429, 20);
            this.textBoxEnviar.TabIndex = 10;
            // 
            // textBoxMsgs
            // 
            this.textBoxMsgs.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxMsgs.Location = new System.Drawing.Point(12, 12);
            this.textBoxMsgs.Multiline = true;
            this.textBoxMsgs.Name = "textBoxMsgs";
            this.textBoxMsgs.ReadOnly = true;
            this.textBoxMsgs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMsgs.Size = new System.Drawing.Size(510, 265);
            this.textBoxMsgs.TabIndex = 9;
            this.textBoxMsgs.TextChanged += new System.EventHandler(this.textBoxMsgs_TextChanged);
            // 
            // FormCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 362);
            this.Controls.Add(this.buttonDesconectar);
            this.Controls.Add(this.buttonConectar);
            this.Controls.Add(this.textBoxPorta);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.buttonEnviar);
            this.Controls.Add(this.textBoxEnviar);
            this.Controls.Add(this.textBoxMsgs);
            this.Name = "FormCliente";
            this.Text = "Cliente";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDesconectar;
        private System.Windows.Forms.Button buttonConectar;
        private System.Windows.Forms.TextBox textBoxPorta;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Button buttonEnviar;
        private System.Windows.Forms.TextBox textBoxEnviar;
        private System.Windows.Forms.TextBox textBoxMsgs;
    }
}

