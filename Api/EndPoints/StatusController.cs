using Microsoft.AspNetCore.Mvc;
using Servico.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EndPoints {
    [ApiController]
    [Route("/")]
    public class StatusController : Controller {
        private IStatusServico _servico;
        public StatusController(IStatusServico servico) {
            _servico = servico;
        }

        [HttpGet]
        [Route("/")]
        public Dictionary<string, string> StatusBanco() {
            Dictionary<string, string> retorno = new Dictionary<string, string>();
            retorno.Add("Banco:", _servico.VerificarStatusBanco());
            return retorno;
        }
    }
}
