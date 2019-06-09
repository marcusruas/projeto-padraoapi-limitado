using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Comunicacao.ConexaoBanco {
    public class ConexaoBanco : IConexaoBanco{
        private readonly IConfiguration Configuracao;
        private SqlConnection Conexao;
        private SqlTransaction Transacao;

        public ConexaoBanco(IConfiguration configuracao) {
            Configuracao = configuracao;
        }

        private void IniciarConexao(string nomeBanco) {
            Conexao = new SqlConnection(Configuracao["Conexoes:" + nomeBanco]);
            Conexao.Open();
        }

        private void FecharConexao() {
            Conexao.Close();
        }

        public void IniciarTransacao(string nomeBanco) {
            try {
                IniciarConexao(nomeBanco);
                Transacao = Conexao.BeginTransaction();
            } catch (SqlException) {
                throw new Exception("Não foi possível criar conexão com o banco de dados.");
            }
        }

        public void Commit() {
            try {
                Transacao.Commit();
                Conexao.Close();
            }
            catch (SqlException) {
                throw new Exception("Ocorreu um erro ao salvar as alterações, tente novamente mais tarde");
            }
        }

        public void Rollback() {
            try {
                Transacao.Rollback();
                Conexao.Close();
            } catch (SqlException) {
                throw new Exception("Ocorreu um erro ao salvar as alterações, tente novamente mais tarde");
            }
        }

        private string CarregarArquivoSQL(string nomeArquivo) {
            string DiretorioRepositorioBuild = Directory.GetCurrentDirectory() + "\\Repositorio\\ConexaoBanco\\SQL\\";
            string arquivoLeitura = DiretorioRepositorioBuild + nomeArquivo + ".sql";
            string conteudoArquivo = string.Empty;
            string[] linhas;

            if (!File.Exists(arquivoLeitura))
                throw new Exception("Não foi possível localizar o arquivo de consulta ao banco com este nome.");
            try {
                linhas = File.ReadAllLines(arquivoLeitura);
                foreach (string linha in linhas)
                    conteudoArquivo += (linha + " ");
            } catch (ArgumentNullException) {
                throw new Exception("O arquivo de consulta ao banco de dados está vazio.");
            }
            return conteudoArquivo;
        }

        public IEnumerable<T> Select<T>(string nomeArquivo, string nomeBanco) {
            IEnumerable<T> resultado;
            string query;
            try {
                query = CarregarArquivoSQL(nomeArquivo);
                IniciarConexao(nomeBanco);
                resultado = Conexao.Query<T>(query).ToList();
                return resultado;
            } catch (SqlException) {
                throw new Exception("Não foi possível realizar a consulta, tente novamente mais tarde.");
            } catch (Exception e) {
                throw e;
            }
        }

        public bool Executar<T>(string nomeArquivo, string nomeBanco, T modelo) {
            string query;
            try {
                query = CarregarArquivoSQL(nomeArquivo);
                IniciarConexao(nomeBanco);
                return Conexao.Execute(query, modelo, Transacao) == 1;
            } catch (SqlException) {
                throw new Exception("Não foi possível realizar a consulta, tente novamente mais tarde.");
            } catch (Exception e) {
                throw e;
            }
        }
    }
}