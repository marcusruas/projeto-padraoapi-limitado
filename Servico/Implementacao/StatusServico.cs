using Repositorio.Interface;
using Servico.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servico.Implementacao {
    public class StatusServico : IStatusServico {
        private readonly IStatusAplicacaoRepositorio _repositorio;

        public StatusServico(IStatusAplicacaoRepositorio repositorio) {
            _repositorio = repositorio;
        }

        public string VerificarStatusBanco() {
            string status = string.Empty;
            try {
                status = _repositorio.StatusBanco();
                return status;
            } catch (Exception e) {
                return e.Message;
            }
        }

    }
}
