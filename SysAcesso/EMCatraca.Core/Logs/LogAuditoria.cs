using System;
using System.IO;
using System.Reflection;

namespace EMCatraca.Core.Logs
{
    public class LogAuditoria
    {
        private static string caminhoExe = string.Empty;
        private static readonly string _pastaCatraca = "EmCatraca";
        private static readonly string _pastaLog = "LOG";
        private static readonly string _segundaLinha = "2.l";

        public static bool Escreva(string nomeLog, string mensagem)
        {
            try
            {
                caminhoExe = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{_pastaCatraca}\\{_pastaLog}";
                string caminhoArquivo = Path.Combine(caminhoExe, $"{DateTime.Now:yyyyMMdd}.{nomeLog}.log");

                if (!Directory.Exists(caminhoArquivo))
                {
                    Directory.CreateDirectory(caminhoExe);
                }

                if (!File.Exists(caminhoArquivo))
                {
                    FileStream arquivo = File.Create(caminhoArquivo);
                    arquivo.Close();
                }

                using (StreamWriter w = File.AppendText(caminhoArquivo))
                {
                    AppendLog(mensagem, w);
                }

                return true;
            }
            catch (Exception)
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

                if (logMensagem.Contains(_segundaLinha))
                {
                    txtWriter.WriteLine($"  :{ logMensagem.Replace(_segundaLinha, "\n  :")}");
                }
                else
                {
                    txtWriter.WriteLine($"  :{logMensagem}");
                }

                txtWriter.WriteLine("-----------------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
