using EMCatraca.Core.Dominio;
using System;
using System.IO;

namespace EMCatraca.Core
{
    public class LogCatraca
    {
        private readonly Dispositivo _catraca;
        private string _nome;

        public LogCatraca(Dispositivo catraca)
        {
            _catraca = catraca;
            _nome = catraca.Descricao;
        }

        public void WriteLog(string message)
        {
            var nomeLog = $"{_nome}";
            Console.WriteLine($"[{nomeLog}] - {ObtenhaHoraFormatada()} - {message}");

            try
            {
                File.AppendAllText(ObtenhaNomeArquivoLog(), $"{ObtenhaHoraFormatada()} - {message}\n");
            }
            catch { }
        }

        private static string ObtenhaHoraFormatada()
        {
            return DateTime.Now.ToString(@"HH\:mm\:ss\.fff");
        }

        public void WriteLogError(string message, Exception ex)
        {
            Console.WriteLine($"\n[{_nome}] - {message} - {ex}\n");

            try
            {
                File.AppendAllText(ObtenhaNomeArquivoLog(), $"{ObtenhaHoraFormatada()} - === ERRO === {message} - Message: {ex.Message} \n");
            }
            catch { }
        }

        private string ObtenhaNomeArquivoLog()
        {
            var diretorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Catraca", "Log");

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            return Path.Combine(diretorio, $"{DateTime.Now:yyyyMMdd}.{_nome}.log");
        }
    }
}
