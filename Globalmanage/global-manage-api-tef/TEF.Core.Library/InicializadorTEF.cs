using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TEF.Core.Library
{
    public static class InicializadorTEF
    {
        //public static string URL = "https://www.portaldoalunocurso.com.br:8082/"; // PROD
        public static string URL = "https://api.portaldoalunocurso.com.br/"; // DEV
        //public static string URL = "http://localhost:8081/"; // DEV

        public static async Task<int> EfetuarTransacao(DtoTransacaoTEF dtoTransacao)
        {
            IntegracaoDTEF integracaoDTEF = new IntegracaoDTEF();

            string numeroControle = "";
            string numeroCartao = "";

            try
            {
                string sNormal = "";
                string sEstendido = "";

                int iRes = 11;

                integracaoDTEF.ObtemLog(ref sNormal, ref sEstendido);

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

                    numeroCartao = sNormal.Trim().Substring(61, 16);
                    numeroControle = sEstendido.Substring(32, 6);
                    string quantidadeParcelas = sNormal.Trim().Substring(104, 2);
                    string numeroAutorizacao = $"{sNormal.Substring(154, 6)} / {sEstendido.Substring(32, 6)}";

                    if (dtoTransacao.Credito)
                    {
                        integracaoDTEF.ConfirmaCartaoCredito(int.Parse(numeroControle));
                    }
                    else
                    {
                        integracaoDTEF.ConfirmaCartaoDebito(int.Parse(numeroControle));
                    }

                    string comprovanteImpressao = integracaoDTEF.ObtemComprovanteTransacao(int.Parse(numeroControle));

                    var posicaoFinalCupom = comprovanteImpressao.LastIndexOf("(NSU D-TEF   : ", comprovanteImpressao.LastIndexOf(")") + 1);

                    comprovanteImpressao = comprovanteImpressao.Substring(0, posicaoFinalCupom + 22);

                    comprovanteImpressao = $"{comprovanteImpressao}\n\n";

                    integracaoDTEF.FinalizaTransacao();

                    quantidadeParcelas = quantidadeParcelas == "00" ? "1" : quantidadeParcelas;

                    if (dtoTransacao.PagamentoIds != null)
                    {
                        await AtualizarPagamento(new DtoTransacaoTEF
                        {
                            Credito = dtoTransacao.Credito,
                            PagamentoIds = dtoTransacao.PagamentoIds,
                            ValorTotal = dtoTransacao.ValorTotal,
                            TEF = true,
                            NumeroCartao = numeroCartao,
                            NumeroControle = numeroAutorizacao,
                            MatriculaId = dtoTransacao.MatriculaId,
                            UsuarioLogadoId = dtoTransacao.UsuarioLogadoId,
                            QuantidadeParcela = int.Parse(quantidadeParcelas),
                            ComprovanteCartao = comprovanteImpressao
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
                            NumeroControle = numeroAutorizacao,
                            QuantidadeParcela = int.Parse(quantidadeParcelas),
                            SolicitacaoId = dtoTransacao.SolicitacaoId.HasValue ? dtoTransacao.SolicitacaoId : null,
                            UsuarioLogadoId = dtoTransacao.UsuarioLogadoId,
                            ComprovanteCartao = comprovanteImpressao
                        });
                    }
                }

                return iRes;
            }
            catch
            {
                if (!string.IsNullOrEmpty(numeroCartao))
                    integracaoDTEF.DesfazCartao(int.Parse(numeroControle));
                throw;
            }
        }

        public static async Task<int> DesfazimentoUltimaTransacao(string numeroControle)
        {
            IntegracaoDTEF integracaoDTEF = new IntegracaoDTEF();

            return integracaoDTEF.DesfazCartao(int.Parse(numeroControle));
        }

        public static void InicializacaoDesfazimento()
        {
            var integracaoDTEF = new IntegracaoDTEF();

            string sNormal = "";
            string sEstendido = "";

            integracaoDTEF.ObtemLog(ref sNormal, ref sEstendido);

            var ultimoNumeroControle = sNormal.Substring(160, 6);

            if (!string.IsNullOrWhiteSpace(ultimoNumeroControle))
            {
                integracaoDTEF.DesfazCartao(int.Parse(ultimoNumeroControle));
                integracaoDTEF.FinalizaTransacao();
            }
        }

        public static async Task AtualizarPagamento(DtoTransacaoTEF dtoTransacaoTEF)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();

            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            string json = JsonConvert.SerializeObject(dtoTransacaoTEF);

            var strinContent = new StringContent(json, Encoding.UTF8, "application/json");

            //client.BaseAddress = new Uri("https://localhost:8080/"); //LocalHost
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync("api/AlunoFinanceiroContrato/EfetuarPagamentoTEF", strinContent);

            if (!response.IsSuccessStatusCode)
            {
                //GravarLog($"Falha na requisição: " + response.Content.ReadAsStringAsync());
            }
        }

        public static async Task ContratarPlano(DtoTransacaoTEF dtoTransacaoTEF)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();

            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            string json = JsonConvert.SerializeObject(dtoTransacaoTEF);

            var strinContent = new StringContent(json, Encoding.UTF8, "application/json");

            //client.BaseAddress = new Uri("https://localhost:8080/"); //LocalHost
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (dtoTransacaoTEF.SolicitacaoId.HasValue)
            {
                await client.PostAsync("api/SolicitacaoAluno/EfetuarSolicitacao", strinContent);
            }
            else
            {
                await client.PostAsync("api/AlunoFinanceiroContrato/ContratarPlano", strinContent);
            }
            //if (!response.IsSuccessStatusCode)
            //{

            //}
            
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
            public int QuantidadeParcela { get; set; }
            public int? SolicitacaoId { get; set; }
            public int UsuarioLogadoId { get; set; }
            public string ComprovanteCartao { get; set; }
        }

        public class DesfazerTransacao 
        {
            public string NumeroControle { get; set; }
        }

    }
}
