using BoletoNetCore;
using EscolaPro.Service.Dto.PagamentosVO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace EscolaPro.WindowsService
{
    public class ItauArquivoRemessaService
    {
        private readonly IBanco _banco;

        public Thread Worker = null;

        public ItauArquivoRemessaService()
        {
            _banco = Banco.Instancia(Bancos.Itau);
        }

        public async Task Working()
        {
            try
            {
                GravarLog("Chamada API");

                await ExecutaArquivoRemessa();

                GravarLog("Fim Remessa");
            }
            catch (Exception ex)
            {
                GravarLog($"Working erro {ex.Message }");
                throw ex;
            }
        }


        public async Task ExecutaArquivoRemessa()
        {
            try
            {
                string path = ConfigurationManager.AppSettings["PathArquivoRetornoItau"];
                var carteiraAtiva = ConfigurationManager.AppSettings["CarteiraAtiva"].Split(',').ToList(); ;

                DirectoryInfo pastaItauFTP = new DirectoryInfo(path);

                FileInfo[] files = pastaItauFTP.GetFiles("*.RET");

                foreach (var file in files)
                {
                    string arquivoPath = $"{path}\\{file.Name}";

                    GravarLog("Processando arquivo " + arquivoPath);

                    var boletos = UploadArquivoRemessa(arquivoPath);

                    HttpClientHandler clientHandler = new HttpClientHandler();

                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                    HttpClient client = new HttpClient(clientHandler);

                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["EscolaProApiBaseAddress"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonContent = JsonConvert.SerializeObject(boletos.Where(x => carteiraAtiva.Contains(x.Carteira)).Select(x => new DtoBoleto
                    {
                        DataCredito = x.DataCredito,
                        NossoNumero = x.NossoNumero,
                        ValorDesconto = x.ValorDesconto,
                        ValorPagoCredito = x.ValorPagoCredito,
                        ValorTarifas = x.ValorTarifas,
                        DescricaoMovimentoRetorno = x.DescricaoMovimentoRetorno
                    }));
                    var boletosString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    GravarLog("Chamando API para " + jsonContent);
                    var response = await client.PostAsync("api/RegistroCobrancaPagamentos/UploadArquivoRemessa", boletosString);

                    if (!response.IsSuccessStatusCode)
                    {
                        GravarLog($"Falha na requisição: Status Code " + response.StatusCode + " - " + response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    }

                    if (!Directory.Exists($"{path}\\backup\\"))
                    {
                        Directory.CreateDirectory($"{path}\\BACKUP\\");
                    }

                    GravarLog("Movendo arquivo " + arquivoPath + " para pasta BACKUP");
                    File.Move(arquivoPath, $"{path}\\BACKUP\\{file.Name}");
                }
            }
            catch (Exception ex)
            {
                GravarLog($"ExecutaArquivoRemessa: Erro");
                throw ex;
            }
        }

        public List<Boleto> UploadArquivoRemessa(string path)
        {
            try
            {
                List<Boleto> boletos = new List<Boleto>();

                var arquivoRetorno = new ArquivoRetorno(_banco, TipoArquivo.CNAB400, true);

                using (var fileStream = new FileStream(path, FileMode.Open))
                {
                    boletos = arquivoRetorno.LerArquivoRetorno(fileStream);
                }

                return boletos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void GravarLog(string txt)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{txt}\n");

            // flush every 20 seconds as you do it
            File.AppendAllText(ConfigurationManager.AppSettings["PathLogFile"] + "LogItau.txt", sb.ToString());
            sb.Clear();
        }
    }
}
