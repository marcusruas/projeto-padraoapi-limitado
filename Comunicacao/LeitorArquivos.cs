using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Comunicacao.ConexaoBanco {
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
            string conexoes = Path.Combine(DiretorioArquivosBuild, "conexoes.json");
            try {
                using (StreamReader r = new StreamReader(conexoes)) {
                    var json = r.ReadToEnd();
                    var conexao = JsonConvert.DeserializeObject<object>(json);
                    return json;
                }
            }catch(Exception) {
                throw new Exception("Não foi possível realizar a conexão ao banco de dados.");
            }
        }
    }
}