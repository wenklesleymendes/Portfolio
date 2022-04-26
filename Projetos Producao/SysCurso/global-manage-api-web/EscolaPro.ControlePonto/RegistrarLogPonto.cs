using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EscolaPro.ControlePonto
{
    public class RegistraLogPonto
    {
        private static string caminhoExe = string.Empty;
        public static bool AmbienteHomolog { get; set; } = false;
        public static bool Log(string strMensagem, TipoResquisicao tipoResquisicao, string nomeApi, string metodo)
        {
            try
            {
                string caminhoArquivo = string.Empty;

                if (AmbienteHomolog)
                {
                    caminhoExe = @"c:\Logs\Homolog\";
                    caminhoArquivo = @"c:\Logs\Homolog\";
                }
                else
                {
                    caminhoExe = @"c:\Logs\Dev\";
                    caminhoArquivo = @"c:\Logs\Dev\";
                }

                switch (tipoResquisicao)
                {
                    case TipoResquisicao.Error:
                        caminhoArquivo = Path.Combine(caminhoExe, "Erro.txt");
                        break;
                    case TipoResquisicao.Json:
                        caminhoArquivo = Path.Combine(caminhoExe, "Json.txt");
                        break;
                    case TipoResquisicao.Sucesso:
                        caminhoArquivo = Path.Combine(caminhoExe, "Sucesso.txt");
                        break;
                    default:
                        break;
                }

                if (!File.Exists(caminhoArquivo))
                {
                    FileStream arquivo = File.Create(caminhoArquivo);
                    arquivo.Close();
                }
                using (StreamWriter w = File.AppendText(caminhoArquivo))
                {
                    AppendLog(strMensagem, w);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static void AppendLog(string logMensagem, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entrada : ");
                txtWriter.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine($"  :{logMensagem}");
                txtWriter.WriteLine("------------------------------------");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


public enum TipoResquisicao
{
    Error,
    Json,
    Sucesso
}