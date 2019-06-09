using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Comunicacao.ConexaoBanco {
    public class LeitorArquivos {

        public static string CarregarArquivoSQL(string nomeArquivo) {
            string DiretorioArquivosBuild = Directory.GetCurrentDirectory() + "\\ConexaoBanco\\SQL\\";
            string arquivoLeitura = DiretorioArquivosBuild + nomeArquivo + ".sql";
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

        public static string ObterStringBanco(string nomeBanco) {
            try {
                string DiretorioConexoesBuild = Directory.GetCurrentDirectory() + "\\ConexaoBanco\\conexoes.json";
                using (StreamReader r = new StreamReader(DiretorioConexoesBuild)) {
                    var json = r.ReadToEnd();
                    var conexoes = JsonConvert.DeserializeObject<string>(json);
                    return json;
                }
            }catch(Exception e) {

            }

            return string.Empty;
        }
    }
}