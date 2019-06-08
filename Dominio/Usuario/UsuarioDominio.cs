using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Usuario {
    public class UsuarioDominio : IUsuarioDominio {
        public UsuarioDominio() {  
        }

        public UsuarioDominio(Guid idUsuario, string nome, string usuario, string senha, string grupo, DateTime dataCriacao, bool ativo, string cpf) {
            IdUsuario = idUsuario;
            Nome = nome;
            Usuario = usuario;
            Senha = senha;
            Grupo = grupo;
            DataCriacao = dataCriacao;
            Ativo = ativo;
            Cpf = cpf;
        }

        [Description("IDUSUARIO")]
        public Guid IdUsuario { get; set; }
        [Description("NOME")]
        public string Nome { get; set; }
        [Description("USUARIO")]
        public string Usuario { get; set; }
        [Description("SENHA")]
        public string Senha { get; set; }
        [Description("GRUPO")]
        public string Grupo { get; set; }
        [Description("DATACRIACAO")]
        public DateTime DataCriacao { get; set; }
        [Description("ATIVO")]
        public bool Ativo { get; set; }
        [Description("CPF")]
        public string Cpf { get; set; }
    }
}
