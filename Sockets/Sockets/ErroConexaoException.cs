using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Conexao
{
    public class ErroConexaoException:Exception
    {
        public ErroConexaoException()
            : base("A conexão com o servidor foi perdida.")
        { 
        }
    }
}
