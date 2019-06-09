using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Comunicacao.ConexaoBanco {
    public interface IConexaoBanco {
        void IniciarTransacao(string nomeBanco);
        void Commit();
        void Rollback();
        IEnumerable<T> Select<T>(string nomeArquivo, string nomeBanco);
    }
}
