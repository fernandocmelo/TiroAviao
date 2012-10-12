using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Conexao
{
    public class Servidor
    {
        #region Campos

        /// <summary>
        /// Conexão do servidor
        /// </summary>
        private TcpListener _listenerServidor;
        /// <summary>
        /// Thread Usada para o servidor ouvir entradas dos clientes de forma não bloqueante
        /// </summary>
        private Thread _threadConexao;
        /// <summary>
        /// Dicionario contendo a chave (identificador do cliente) e valor o socket cliente
        /// </summary>
        private Dictionary<int, Socket> _listaClientes;
        /// <summary>
        /// Threads usadas para a conexão de cada cliente
        /// </summary>
        private List<Thread> _threadsLeitura;
        /// <summary>
        /// propriedade usada para contrar a chave unica atribuida a cada cliente
        /// </summary>
        private int _chave;
        
        #endregion        

        #region Propriedades

        /// <summary>
        /// Retorna lista com o código dos clientes conectados.
        /// </summary>
        public int[] ClientesConectados
        {
            get
            {
                return _listaClientes.Keys.ToArray<int>();
            }
        }

        public int NroClientesConectados
        {
            get
            {
                return _listaClientes.Count;
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento disparado quando o servidor receber mensagem dos clientes
        /// </summary>
        public event ReceberMensagem QuandoMensagemRecebida;

        #endregion

        #region Construtor e métodos públicos

        public Servidor()
        {
            _chave = 0;
        }
        /// <summary>
        /// Servidor abre uma nova conexão na porta e ip informados
        /// </summary>
        /// <param name="Ip">IP utilizado para abrir conexão</param>
        /// <param name="Porta">Porta utilizada para abrir conexão</param>
        public void AbrirConexao(String Ip, int Porta)
        {
            // Converte o Ip
            IPAddress enderecoIp = IPAddress.Parse(Ip);
            // Inicia conexão
            _listenerServidor = new TcpListener(enderecoIp, Porta);
            // inicia sevidor
            _listenerServidor.Start();
            // Inicia lista de clientes
            _listaClientes = new Dictionary<int, Socket>();
            // zera a chave quando abre uma nova conexão
            _chave = 0;
            // Inicializa lista de threads dos clientes
            _threadsLeitura = new List<Thread>();
            // Inicia metodo para aguardar conexão do cliente
            aguardarConexoes();
        }

        /// <summary>
        ///  Fecha todas as conexões dos clientes com o servidor e encerra a conexão do servidor
        /// </summary>
        public void FecharConexao()
        {
            // Encerra thread de leitura de dados
            foreach (Thread threadLeitura in _threadsLeitura)
            {
                if (threadLeitura != null && threadLeitura.IsAlive)
                {
                    threadLeitura.Abort();
                }
            }
            // Encerra thread de que aguarda conexões
            // Mata a thread se estiver executando
            if (_threadConexao != null && _threadConexao.IsAlive)
            {
                _threadConexao.Abort();
            }
            // Para cada cliente conectado no servidor.
            foreach (Socket cliente in _listaClientes.Values)
            {
                // Encerra o socket se estiver aberto
                if (cliente.Connected)
                {
                    cliente.Close();
                }
            }
            // Para o listener
            if (_listenerServidor != null)
            {
                _listenerServidor.Stop();
            }
            _listaClientes.Clear();
            _chave = 0;
        }

        /// <summary>
        /// Propaga a mensagem para todos os clientes conectados no servidor.
        /// </summary>
        /// <param name="Mensagem">Mensagem enviada</param>
        public void EscreverParaTodos(String Mensagem)
        {
            foreach (Socket cliente in _listaClientes.Values)
            {
                escreverMensagem(cliente, Mensagem);
            }
        }

        /// <summary>
        /// Escreve mensagem para o cliente
        /// </summary>
        /// <param name="CodigoCliente">Utiliza o identificado para localizar o socket</param>
        /// <param name="Mensagem">Mensagem Enviada</param>
        public void EscreverMensagem(int CodigoCliente, String Mensagem)
        {
            Socket cliente = _listaClientes[CodigoCliente];
            escreverMensagem(cliente, Mensagem);
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Servidor inicia serviço para aguardar conexões de clientes. Após conexão do cliente o servidor já estará recebendo dados do cliente.
        /// </summary>
        private void aguardarConexoes()
        {
            // Passa método que aceita conexões por parâmetro
            _threadConexao = new Thread(distribuirConexao);
            // Inicia Thread
            _threadConexao.Start();
        }
        
        /// <summary>
        /// Escreve mensagem para o cliente
        /// </summary>
        /// <param name="Cliente">Socket para qual será enviada a mensagem</param>
        /// <param name="Mensagem">Mensagem enviada</param>
        private void escreverMensagem(Socket Cliente, String Mensagem)
        {
            ASCIIEncoding asen = new ASCIIEncoding();
            Cliente.Send(asen.GetBytes(Mensagem));
        }

        /// <summary>
        /// Servidor fica aguarando conexões de clientes e cria uma nova thread de leitura de dados para cada cliente conectado.
        /// </summary>
        private void distribuirConexao()
        {
            Socket cliente = null;
            Thread threadLeitura = null;
            ParameterizedThreadStart operacao = null;
            List<object> parametros = null;
            // fica sempre aguardando a conexão de algum cliente
            while (true)
            {
                // Aguarda cliente se conectar
                cliente = _listenerServidor.AcceptSocket();
                // Adiciona cliente na lista de clientes
                _chave++;
                _listaClientes.Add(_chave, cliente);
                // Envia para o cliente o código que a ele foi atribuido no servidor.
                escreverMensagem(cliente, _chave.ToString());
                // Chama thread para controlar leitur do cliente
                operacao = new ParameterizedThreadStart(lerDadosCliente);
                threadLeitura = new Thread(operacao);
                // Adiciona na lista de threads de leitura do servidor
                _threadsLeitura.Add(threadLeitura);
                // Inicia leitura do cliente
                parametros = new List<object>();
                parametros.Add(_chave);
                parametros.Add(cliente);
                threadLeitura.Start(parametros);
            }

        }

        /// <summary>
        /// Recebe um socket por parâmetro e monitora a porta até receber uma mensagem.
        /// </summary>
        /// <param name="cliente">cliente que será monitorado</param>
        /// <returns>Retorna mensagem recebida pelo cliente</returns>
        private string aguardaMensagem(Socket cliente)
        {
            // Inicializa a string q vai receber os dados
            StringBuilder str = new StringBuilder();
            byte[] listaBytes = null;
            int nroBytesTransferidos = 0;
            listaBytes = new byte[500];
            nroBytesTransferidos = cliente.Receive(listaBytes);
            for (int i = 0; i < nroBytesTransferidos; i++)
            {
                str.Append(Convert.ToChar(listaBytes[i]));
            }
            // retorna dados recebidos
            return str.ToString();
        }

        /// <summary>
        /// Responsavel por fazer a leitura do socket do cliente.
        /// </summary>
        /// <param name="o"></param>
        private void lerDadosCliente(Object o)
        {
            // Converte o parametro para cliente
            List<Object> parametros = o as List<Object>;
            // Primeiro: codigoCliente: int
            int codigoCliente = (int)parametros[0];
            // Segundo: cliente: Socket
            Socket cliente = parametros[1] as Socket;
            String mensagem = "";

            while (true)
            {
                
                mensagem = aguardaMensagem(cliente);
                // Quando o servidor não receber nada do cliente encerra a conexão.
                if (mensagem == "")
                {
                    cliente.Close();
                    removeClienteLista(codigoCliente);
                    break;
                }
                else
                {
                    // remove o código do cliente.
                    QuandoMensagemRecebida(this, new LeituraEventArgs(codigoCliente, mensagem));
                }
            }
        }

        private void removeClienteLista(int CodigoCliente)
        {
            _listaClientes.Remove(CodigoCliente);
            // verifica se existe algum cliente conectado. Se não tiver zera a chave
            if (_listaClientes.Count == 0)
            {
                _chave = 0;
            }
        }

        #endregion

    }
}
