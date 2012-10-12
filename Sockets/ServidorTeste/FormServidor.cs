using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Conexao;

namespace ServidorTeste
{
    public partial class FormServidor : Form
    {
        Servidor servidor;
        public FormServidor()
        {
            InitializeComponent();
            textBoxIP.Text = "192.168.1.199";
            textBoxPorta.Text = "8001";
        }

        private void buttonConectar_Click(object sender, EventArgs e)
        {
            servidor = new Servidor();
            servidor.AbrirConexao(textBoxIP.Text, int.Parse(textBoxPorta.Text));
            servidor.QuandoMensagemRecebida += new ReceberMensagem(servidor_QuandoMensagemRecebida);
            textBoxMsgs.Text += "Conexão aberta.\r\n";
        }

        void servidor_QuandoMensagemRecebida(object sender, LeituraEventArgs e)
        {
            String msgFormatada;
            msgFormatada = "Cliente " + e.Remetente + ": " + e.Mensagem + "\r\n";
            textBoxMsgs.Text += msgFormatada;
        }

        private void buttonEnviar_Click(object sender, EventArgs e)
        {
            servidor.EscreverParaTodos(textBoxEnviar.Text);
            textBoxMsgs.Text += "Servidor: " + textBoxEnviar.Text + "\r\n";
        }

        private void buttonDesconectar_Click(object sender, EventArgs e)
        {
            servidor.FecharConexao();
            textBoxMsgs.Text += "Conexão fechada.\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxMsgs.Text += "Clientes conectados: "+servidor.NroClientesConectados+"\r\n";
            foreach (int cliente in servidor.ClientesConectados)
            {
                textBoxMsgs.Text += cliente+"\r\n";
            }
        }
    }
}
