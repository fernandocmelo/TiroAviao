namespace ServidorTeste
{
    partial class FormServidor
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
            this.textBoxMsgs = new System.Windows.Forms.TextBox();
            this.textBoxEnviar = new System.Windows.Forms.TextBox();
            this.buttonEnviar = new System.Windows.Forms.Button();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPorta = new System.Windows.Forms.TextBox();
            this.buttonConectar = new System.Windows.Forms.Button();
            this.labelIp = new System.Windows.Forms.Label();
            this.labelPorta = new System.Windows.Forms.Label();
            this.buttonDesconectar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            this.textBoxMsgs.TabIndex = 0;
            // 
            // textBoxEnviar
            // 
            this.textBoxEnviar.Location = new System.Drawing.Point(12, 283);
            this.textBoxEnviar.Name = "textBoxEnviar";
            this.textBoxEnviar.Size = new System.Drawing.Size(429, 20);
            this.textBoxEnviar.TabIndex = 1;
            // 
            // buttonEnviar
            // 
            this.buttonEnviar.Location = new System.Drawing.Point(447, 283);
            this.buttonEnviar.Name = "buttonEnviar";
            this.buttonEnviar.Size = new System.Drawing.Size(75, 23);
            this.buttonEnviar.TabIndex = 2;
            this.buttonEnviar.Text = "Enviar";
            this.buttonEnviar.UseVisualStyleBackColor = true;
            this.buttonEnviar.Click += new System.EventHandler(this.buttonEnviar_Click);
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(12, 327);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(270, 20);
            this.textBoxIP.TabIndex = 3;
            // 
            // textBoxPorta
            // 
            this.textBoxPorta.Location = new System.Drawing.Point(285, 327);
            this.textBoxPorta.Name = "textBoxPorta";
            this.textBoxPorta.Size = new System.Drawing.Size(73, 20);
            this.textBoxPorta.TabIndex = 4;
            this.textBoxPorta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonConectar
            // 
            this.buttonConectar.Location = new System.Drawing.Point(361, 327);
            this.buttonConectar.Name = "buttonConectar";
            this.buttonConectar.Size = new System.Drawing.Size(75, 23);
            this.buttonConectar.TabIndex = 5;
            this.buttonConectar.Text = "Abrir";
            this.buttonConectar.UseVisualStyleBackColor = true;
            this.buttonConectar.Click += new System.EventHandler(this.buttonConectar_Click);
            // 
            // labelIp
            // 
            this.labelIp.AutoSize = true;
            this.labelIp.Location = new System.Drawing.Point(12, 309);
            this.labelIp.Name = "labelIp";
            this.labelIp.Size = new System.Drawing.Size(69, 13);
            this.labelIp.TabIndex = 6;
            this.labelIp.Text = "Endereço IP:";
            // 
            // labelPorta
            // 
            this.labelPorta.AutoSize = true;
            this.labelPorta.Location = new System.Drawing.Point(289, 309);
            this.labelPorta.Name = "labelPorta";
            this.labelPorta.Size = new System.Drawing.Size(35, 13);
            this.labelPorta.TabIndex = 7;
            this.labelPorta.Text = "Porta:";
            // 
            // buttonDesconectar
            // 
            this.buttonDesconectar.Location = new System.Drawing.Point(442, 327);
            this.buttonDesconectar.Name = "buttonDesconectar";
            this.buttonDesconectar.Size = new System.Drawing.Size(80, 23);
            this.buttonDesconectar.TabIndex = 8;
            this.buttonDesconectar.Text = "Fechar";
            this.buttonDesconectar.UseVisualStyleBackColor = true;
            this.buttonDesconectar.Click += new System.EventHandler(this.buttonDesconectar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(528, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Cl.conect.";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormServidor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 362);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonDesconectar);
            this.Controls.Add(this.labelPorta);
            this.Controls.Add(this.labelIp);
            this.Controls.Add(this.buttonConectar);
            this.Controls.Add(this.textBoxPorta);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.buttonEnviar);
            this.Controls.Add(this.textBoxEnviar);
            this.Controls.Add(this.textBoxMsgs);
            this.Name = "FormServidor";
            this.Text = "Servidor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMsgs;
        private System.Windows.Forms.TextBox textBoxEnviar;
        private System.Windows.Forms.Button buttonEnviar;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPorta;
        private System.Windows.Forms.Button buttonConectar;
        private System.Windows.Forms.Label labelIp;
        private System.Windows.Forms.Label labelPorta;
        private System.Windows.Forms.Button buttonDesconectar;
        private System.Windows.Forms.Button button1;
    }
}

