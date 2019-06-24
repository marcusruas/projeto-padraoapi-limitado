﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Usuario {
    public interface IUsuario {
        Guid IdUsuario { get; set; }
        string Nome { get; set; }
        string UsuarioAplicacao { get; set; }
        string Senha { get; set; }
        string Grupo { get; set; }
        DateTime DataCriacao { get; set; }
        bool Ativo { get; set; }
        string Cpf { get; set;  }
    }
}
