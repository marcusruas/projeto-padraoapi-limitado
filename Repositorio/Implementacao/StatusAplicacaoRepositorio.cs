using Repositorio.ConexaoBanco;
using Repositorio.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Implementacao {
    public class StatusAplicacaoRepositorio : IStatusAplicacaoRepositorio {
        private IConexaoBanco Conexao;
        public StatusAplicacaoRepositorio(IConexaoBanco conexao) {
            Conexao = conexao;
        }

        public string StatusBanco() {
            string resultado = string.Empty;
            try {
                resultado = Conexao.Select<string>("selectStatusBanco", "SHAREDB").First().ToString();
            } catch(Exception e) {
                return e.Message;
            }
            return resultado;
        }
    }
}
