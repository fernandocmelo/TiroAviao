using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Conexao;

namespace ClienteTeste
{
    public partial class FormCliente : Form
    {
        Cliente cliente;
        public FormCliente()
        {
            InitializeComponent();
            textBoxIP.Text = "192.168.1.199";
            textBoxPorta.Text = "8001";
        }

        private void buttonEnviar_Click(object sender, EventArgs e)
        {
            cliente.EscreverMensagem(textBoxEnviar.Text);
            textBoxMsgs.Text += "Cliente "+cliente.CodigoCliente+": " + textBoxEnviar.Text + "\r\n";
        }

        void cliente_QuandoMensagemRecebida(object sender, LeituraEventArgs e)
        {
            String msgFormatada;
            msgFormatada = "Servidor: " + e.Mensagem + "\r\n";
            textBoxMsgs.Text += msgFormatada;
        }

        private void buttonConectar_Click(object sender, EventArgs e)
        {
            cliente = new Cliente();
            cliente.Conectar(textBoxIP.Text, int.Parse(textBoxPorta.Text));
            cliente.QuandoOcorrerErro += new Erro(cliente_QuandoOcorrerErro);
            cliente.QuandoMensagemRecebida += new ReceberMensagem(cliente_QuandoMensagemRecebida);
            textBoxMsgs.Text += "Conectado com o servidor\r\n";
        }

        void cliente_QuandoOcorrerErro(object sender, ErroEventArgs e)
        {
            MessageBox.Show(e.Erro,"Erro");
        }

        private void buttonDesconectar_Click(object sender, EventArgs e)
        {
            cliente.Desconectar();
            textBoxMsgs.Text += "Desconectado do servidor\r\n";
        }

        private void textBoxMsgs_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
