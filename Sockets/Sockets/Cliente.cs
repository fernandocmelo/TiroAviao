using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Conexao
{
    public class Cliente
    {
        #region Campos

        /// <summary>
        /// Conexão tcp do cliente
        /// </summary>
        private TcpClient _tcpCliente;
        /// <summary>
        /// Thread que controla as mensagens recebidas do servidor
        /// </summary>
        private Thread _threadLeitura;
        /// <summary>
        /// Identificador único do cliente
        /// </summary>
        private int _codigoCliente;
        /// <summary>
        /// Fluxo de dados recebido pelo tcp
        /// </summary>
        private Stream _stream;

        #endregion

        #region Propriedades

        /// <summary>
        /// Retorna o código do cliente atribuído pelo servidor
        /// </summary>
        public int CodigoCliente
        {
            get
            {
                return _codigoCliente;
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Dispara este evento quando receber uma mensagem do servidor
        /// </summary>
        public event ReceberMensagem QuandoMensagemRecebida;
        /// <summary>
        /// Dispara este evento quando ocorrer algum erro tratado na classe
        /// </summary>
        public event Erro QuandoOcorrerErro;

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Conecta-se ao servidor
        /// </summary>
        /// <param name="Ip">IP do servidor</param>
        /// <param name="Porta">Porta do servidor</param>
        public void Conectar(String Ip, int Porta)
        {
            // Inicializa cliente
            _tcpCliente = new TcpClient();
            // Estabelece conexão com o servidor
            _tcpCliente.Connect(Ip, Porta);
            // Inicializa stream
            _stream = _tcpCliente.GetStream();
            // Aguarda servidor enviar código do cliente
            _codigoCliente = int.Parse(aguardaMensagem());
            // Chama thread para receber mensagens do servidor
            _threadLeitura = new Thread(receberDados);
            // Inicia a thread para receber os dados
            _threadLeitura.Start();
        }

        /// <summary>
        /// Desconecta-se do servidor
        /// </summary>
        public void Desconectar()
        {
            // Mata a thread
            if (_threadLeitura != null && _threadLeitura.IsAlive)
            {
                _threadLeitura.Abort();
            }
            // encerra conexão com o servidor
            if (_tcpCliente != null && _tcpCliente.Connected)
            {
                _stream.Close();
                _tcpCliente.Close();
            }
        }

        /// <summary>
        /// Envia mensagem para o servidor
        /// </summary>
        /// <param name="Mensagem">Mensagem enviada para o servidor</param>
        public void EscreverMensagem(String Mensagem)
        {
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] listaBytes = asen.GetBytes(Mensagem);
            _stream.Write(listaBytes, 0, listaBytes.Length);
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Aguarda receber dados do servidor
        /// </summary>
        /// <returns></returns>
        private string aguardaMensagem()
        {
            // fluxo de dados do cliente
            byte[] listaBytes = null;
            int nroBytesTransferidos = 0;
            StringBuilder str = new StringBuilder();
            listaBytes = new byte[500];
            try
            {
                nroBytesTransferidos = _stream.Read(listaBytes, 0, 500);
            }
            catch (Exception)
            {

                throw new ErroConexaoException();
            }

            for (int i = 0; i < nroBytesTransferidos; i++)
            {
                str.Append(Convert.ToChar(listaBytes[i]));
            }
            // retorna resultado
            return str.ToString();
        }

        /// <summary>
        /// Método para ficar esperando dados do servidor
        /// </summary>
        private void receberDados()
        {
            string mensagem = "";
            while (true)
            {
                try
                {
                    mensagem = aguardaMensagem();
                }
                catch (ErroConexaoException erro)
                {
                    _stream.Close();
                    _tcpCliente.Close();
                    QuandoOcorrerErro(this, new ErroEventArgs(erro.Message));
                    break;
                }
                // Envia evento alertando q uma mensagem foi recebida.
                QuandoMensagemRecebida(this, new LeituraEventArgs(0, mensagem));
            }
        }

        #endregion

    }
}
