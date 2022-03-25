using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EscolaPro.OrquestradorReguaContato
{
    public class AtualizarFila
    {


        public Thread Worker = null;
        public AtualizarFila()
        {

        }
        public async Task Working()
        {
            try
            {
                Log.Gravar($"{DateTime.Now} - Inicio API - Task Atualizar Fila");

                await ExecutaAtualizacao();


                Log.Gravar($"{DateTime.Now} - Fim API - Task Atualizar Fila");
            }
            catch (Exception ex)
            {
                Log.Gravar($"{DateTime.Now} - Task Atualizar Fila Working erro {ex.Message }");
                throw ex;
            }
        }

        private async Task ExecutaAtualizacao()
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();

                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["EscolaProApiBaseAddress"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                Log.Gravar("Chamando API para ");
                var response = await client.GetAsync("api/ReguaContato/CarregaReguaContatoCobranca");

                if (!response.IsSuccessStatusCode)
                {
                    Log.Gravar($"Falha na requisição:  " + response.StatusCode + " - " + response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }
            }
            catch (Exception ex)
            {
                Log.Gravar($"Falha na execução: Status Code" + ex.Message);
                throw;
            }
        }
    }
}
