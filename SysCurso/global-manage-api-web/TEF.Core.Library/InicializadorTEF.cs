using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace TEF.Core.Library
{
    public static class InicializadorTEF
    {
        public static async Task<int> EfetuarTransacao(DtoTransacaoTEF dtoTransacao)
        {
            try
            {
                IntegracaoDTEF integracaoDTEF = new IntegracaoDTEF();

                string sNormal = "";
                string sEstendido = "";

                int iRes = 11;

                if (dtoTransacao.Credito)
                {
                    iRes = integracaoDTEF.EfetuarPagamentoCredito(dtoTransacao.ValorTotal);
                }
                else
                {
                    iRes = integracaoDTEF.EfetuarPagamentoDebito(dtoTransacao.ValorTotal);
                }

                if (iRes == 0)
                {
                    integracaoDTEF.ObtemLog(ref sNormal, ref sEstendido);

                    string numeroCartao = sNormal.Trim().Substring(61, 16);
                    string numeroControle = sEstendido.Substring(32, 6);

                    if (dtoTransacao.PagamentoIds != null)
                    {
                        await AtualizarPagamento(new DtoTransacaoTEF 
                        { 
                            Credito = dtoTransacao.Credito, 
                            PagamentoIds = dtoTransacao.PagamentoIds, 
                            ValorTotal = dtoTransacao.ValorTotal, 
                            TEF = true,
                            NumeroCartao = numeroCartao,
                            NumeroControle = numeroControle
                        });
                    }
                    else
                    {
                        await ContratarPlano(new DtoTransacaoTEF
                        {
                            Credito = dtoTransacao.Credito,
                            PagamentoIds = dtoTransacao.PagamentoIds,
                            ValorTotal = dtoTransacao.ValorTotal,
                            TEF = true,
                            CampanhaId = dtoTransacao.CampanhaId,
                            MatriculaId = dtoTransacao.MatriculaId,
                            PlanoPagamentoId = dtoTransacao.PlanoPagamentoId,
                            TemApostila = dtoTransacao.TemApostila,
                            NumeroCartao = numeroCartao,
                            NumeroControle = numeroControle
                        });
                    }
                }

                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task AtualizarPagamento(DtoTransacaoTEF dtoTransacaoTEF)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();

                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                string json = JsonConvert.SerializeObject(dtoTransacaoTEF);

                var strinContent = new StringContent(json, Encoding.UTF8, "application/json");

                //client.BaseAddress = new Uri("https://localhost:8080/");
                client.BaseAddress = new Uri("https://18.230.199.40:8081/");
                client.BaseAddress = new Uri("https://18.230.199.40:8080/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync("api/AlunoFinanceiroContrato/EfetuarPagamentoTEF", strinContent);

                if (!response.IsSuccessStatusCode)
                {
                    //GravarLog($"Falha na requisição: " + response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                //GravarLog($"ExecutaArquivoRemessa: Erro");
                throw ex;
            }
        }

        public static async Task ContratarPlano(DtoTransacaoTEF dtoTransacaoTEF)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();

                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                string json = JsonConvert.SerializeObject(dtoTransacaoTEF);

                var strinContent = new StringContent(json, Encoding.UTF8, "application/json");

                //client.BaseAddress = new Uri("https://localhost:8080/");
                client.BaseAddress = new Uri("https://18.230.199.40:8080/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync("api/AlunoFinanceiroContrato/ContratarPlano", strinContent);

                if (!response.IsSuccessStatusCode)
                {

                }
            }
            catch (Exception ex)
            {
                //GravarLog($"ExecutaArquivoRemessa: Erro");
                throw ex;
            }
        }

        public class DtoTransacaoTEF
        {
            [DataMember(Name = "Amount")]
            public decimal ValorTotal { get; set; }
            public bool Credito { get; set; }
            public int[] PagamentoIds { get; set; }
            public bool TEF { get; set; }

            // ContratarPlano
            public int MatriculaId { get; set; }
            public int PlanoPagamentoId { get; set; }
            public bool TemApostila { get; set; }
            public int? CampanhaId { get; set; }
            public string NumeroCartao { get; set; }
            public string NumeroControle { get; set; }
        }
    }
}
