using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conexao
{
    public delegate void ReceberMensagem(object sender, LeituraEventArgs e);
    public delegate void Erro(object sender, ErroEventArgs e);

    public class LeituraEventArgs
    {
        private string _mensagem;
        private int _remetente;
    
        public LeituraEventArgs(int Remetente, string Mensagem)
        {
            _remetente = Remetente;
            _mensagem = Mensagem;
        }

        public string Mensagem
        {
            get
            {
                return _mensagem;
            }
        }

        public int Remetente
        {
            get
            {
                return _remetente;
            }
        }
    }

    public class ErroEventArgs
    {
        string _erro;

        public string Erro
        {
            get
            {
                return _erro;
            }
        }
        public ErroEventArgs(String Erro)
        {
            _erro = Erro;
        }
    }
}
