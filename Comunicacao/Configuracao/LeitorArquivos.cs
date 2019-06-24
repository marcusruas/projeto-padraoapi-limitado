using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Comunicacao.Configuracao {
    public class LeitorArquivos {
        private static string DiretorioArquivosBuild = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;

        public static string CarregarArquivoSQL(string nomeArquivo) {
            string pathArquivo = Path.Combine(DiretorioArquivosBuild, "ConexaoBanco", "SQL", $"{nomeArquivo}.sql");
            string conteudoArquivo = string.Empty;
            string[] linhas;

            if (!File.Exists(pathArquivo))
                throw new Exception("Não foi possível localizar o arquivo de consulta ao banco com este nome.");
            try {
                linhas = File.ReadAllLines(pathArquivo);
                foreach (string linha in linhas)
                    conteudoArquivo += (linha + " ");
            } catch (ArgumentNullException) {
                throw new Exception("O arquivo de consulta ao banco de dados está vazio.");
            }
            return conteudoArquivo;
        }

        public static string ObterStringBanco(string nomeBanco) {
            string arquivoConexao = Path.Combine(DiretorioArquivosBuild, "conexoes.json");
            List<Conexao> conexoes;
            try {
                using (StreamReader r = new StreamReader(arquivoConexao)) {
                    var json = r.ReadToEnd();
                    conexoes = JsonConvert.DeserializeObject<Conexao[]>(json).ToList();
                }
                foreach(var con in conexoes)
                    if (con.Nome == nomeBanco)
                        return con.StringConexao;
                return null;
            }catch(Exception) {
                throw new Exception("Não foi possível realizar a conexão ao banco de dados.");
            }
        }
    }
}