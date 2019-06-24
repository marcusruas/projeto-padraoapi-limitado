using Comunicacao.Configuracao;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Comunicacao.ConexaoBanco {
    public class ConexaoBanco : IConexaoBanco{
        private SqlConnection Conexao;
        private SqlTransaction Transacao;

        public void IniciarConexao(string nomeBanco) {
            Conexao = new SqlConnection(LeitorArquivos.ObterStringBanco(nomeBanco));
            Conexao.Open();
        }

        public void FecharConexao() {
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
        
        public IEnumerable<T> Select<T>(string nomeArquivo, string nomeBanco) {
            IEnumerable<T> resultado;
            string query;
            try {
                query = LeitorArquivos.CarregarArquivoSQL(nomeArquivo);
                IniciarConexao(nomeBanco);
                resultado = Conexao.Query<T>(query).ToList();
                return resultado;
            } catch (SqlException) {
                throw new Exception("Não foi possível realizar a consulta, tente novamente mais tarde.");
            } catch (Exception e) {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }

        public T SelectUmaLinha<T>(string nomeArquivo, string nomeBanco)
        {
            T resultado;
            string query;
            try
            {
                query = LeitorArquivos.CarregarArquivoSQL(nomeArquivo);
                IniciarConexao(nomeBanco);
                resultado = Conexao.Query<T>(query).FirstOrDefault();
                return resultado;
            }
            catch (SqlException)
            {
                throw new Exception("Não foi possível realizar a consulta, tente novamente mais tarde.");
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }

        public int Executar<T>(string nomeArquivo, string nomeBanco, T modelo) {
            string query;
            try {
                query = LeitorArquivos.CarregarArquivoSQL(nomeArquivo);
                IniciarConexao(nomeBanco);
                return Conexao.Execute(query, modelo, Transacao);
            } catch (SqlException) {
                throw new Exception("Não foi possível realizar a consulta, tente novamente mais tarde.");
            } catch (Exception e) {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }
    }
}