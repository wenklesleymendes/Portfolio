using EscolaPro.OrquestradorReguaContato.Dto;
using Newtonsoft.Json;
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
    public class DiparadorFila
    {
        public readonly List<TipoMensagem> tipoMensagems;
        public Thread Worker = null;

        public DiparadorFila()
        {
            tipoMensagems = new List<TipoMensagem>() { TipoMensagem.Email, TipoMensagem.SMS, TipoMensagem.WhatsApp };
        }

        public async Task Working()
        {
            try
            {
                Log.Gravar($"{DateTime.Now} - Inicio API - Task Disparar Fila");

                await ExecutarDisparo();

                Log.Gravar($"{DateTime.Now} - Fim API - Task Disparar Fila");
            }
            catch (Exception ex)
            {
                Log.Gravar($"{DateTime.Now} - Task Disparar Fila: erro {ex.Message }");
                throw ex;
            }
        }

        private async Task<IEnumerable<ReguaContatoFila>> CarregarFila(TipoMensagem tipoMensagem)
        {
            try
            {
                IEnumerable<ReguaContatoFila> reguaContatoFilas = new List<ReguaContatoFila>();
                HttpClientHandler clientHandler = new HttpClientHandler();

                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["EscolaProApiBaseAddress"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonContent = JsonConvert.SerializeObject(tipoMensagem);
                var body = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                Log.Gravar($"{DateTime.Now} - CarregarFila: Chamando API api/ReguaContato/RetornaContatoFilas");
                var response = await client.PostAsync("api/ReguaContato/RetornaContatoFilas", body);

                if (!response.IsSuccessStatusCode)
                {
                    Log.Gravar($"{DateTime.Now} - CarregarFila:  Falha na requisição: api/ReguaContato/RetornaContatoFilas " + response.StatusCode + " - " + response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    reguaContatoFilas = JsonConvert.DeserializeObject<IEnumerable<ReguaContatoFila>>(responseBody);
                };

                return reguaContatoFilas;

            }
            catch (Exception ex)
            {
                Log.Gravar($"{DateTime.Now} - CarregarFila: Falha na execução: Status Code" + ex.Message);
                throw;
            }
        }

        public async Task ExecutarDisparo()
        {
            string timePausa = ConfigurationManager.AppSettings["TimePauseWhatsApp"];
            int.TryParse(timePausa, out int timePauseWhatsApp);
            foreach (var tipoMensagem in tipoMensagems)
            {
                IEnumerable<ReguaContatoFila> fila = await this.CarregarFila(tipoMensagem);
                foreach (var item in fila)
                {
                    try
                    {

                        HttpClientHandler clientHandler = new HttpClientHandler();

                        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                        HttpClient client = new HttpClient(clientHandler);

                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["EscolaProApiBaseAddress"]);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var jsonContent = JsonConvert.SerializeObject(item);
                        var body = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        Log.Gravar($"{ DateTime.Now} - ExecutarDisparo - Chamando API para /ReguaContato/DispararContato, Aluno: {item.AlunoId}");
                        var response = await client.PostAsync("api/ReguaContato/DispararContato", body);
                        if (!response.IsSuccessStatusCode)
                            Log.Gravar($"{ DateTime.Now} - ExecutarDisparo - Falha na requisição: {response.StatusCode} - {response.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");
                        else
                            Log.Gravar($"{ DateTime.Now} - ExecutarDisparo - Disparo realizado com sucesso.  Aluno: {item.AlunoId} - {response.Content} - {response.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");

                        if (tipoMensagem == TipoMensagem.WhatsApp)
                            Thread.Sleep(timePauseWhatsApp);

                    }
                    catch (Exception ex)
                    {
                        Log.Gravar($"{ DateTime.Now} - ExecutarDisparo - Falha na execução: Status Code {ex.Message}");
                        throw;
                    }


                }
            }
        }
    }
}
