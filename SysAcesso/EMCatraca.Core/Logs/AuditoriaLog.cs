using EMCatraca.Core.Interfaces;
using System;
using System.IO;

namespace EMCatraca.Core.Logs
{
    public static class AuditoriaLog
    {
        public static void Escreva(string nomeLog, string message)
        {
            Console.WriteLine($"[{nomeLog}] - {FormatadaHoraMinutoSegundoMilesegundo()} - {message}");

            try
            {
                string caminhoArquivo = ObtenhaEnderecoLog(nomeLog);

                if (!File.Exists(caminhoArquivo))
                {
                    FileStream arquivo = File.Create(caminhoArquivo);
                    arquivo.Close();
                }

                //using (StreamWriter w = File.AppendText(caminhoArquivo))
                //{
                //    File.AppendAllText(ObtenhaEnderecoLog(nomeLog), 
                //        $"{FormatadaHoraMinutoSegundoMilesegundo()} - {message}\n");
                //}

                File.AppendAllText(ObtenhaEnderecoLog(nomeLog),
                        $"{FormatadaHoraMinutoSegundoMilesegundo()} - {message}\n");


            }
            catch { }
        }

        public static void EscrevaErro(string nomeLog, Exception ex)
        {
            Console.WriteLine($"\n[{nomeLog}] - {ex}\n");

            try
            {
                File.AppendAllText($"{ObtenhaEnderecoLog(nomeLog)}", 
                    $"{FormatadaHoraMinutoSegundoMilesegundo()} - === ERRO === - Message: {ex.Message} \n");
            }
            catch { }
        }

        public static void EscrevaErro(string nomeLog, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"\n[{nomeLog}] - {e}\n");

            try
            {
                File.AppendAllText($"{ObtenhaEnderecoLog(nomeLog)}", 
                    $"{FormatadaHoraMinutoSegundoMilesegundo()} - === ERRO === - Message: {e} \n");
            }
            catch { }
        }

        private static string FormatadaHoraMinutoSegundoMilesegundo()
        {
            return DateTime.Now.ToString(@"HH\:mm\:ss\.fff");
        }
            
        private static string ObtenhaEnderecoLog(string nomeLog)
        {
            var aquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Catraca", "LOG");

            if (!Directory.Exists(aquivo))
            {
                Directory.CreateDirectory(aquivo);
            }

            return Path.Combine(aquivo, $"{DateTime.Now:yyyyMMdd}.{nomeLog}.log");
        }
    }
}
