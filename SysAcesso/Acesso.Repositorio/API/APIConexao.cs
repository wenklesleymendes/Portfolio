using System;
using System.Net.Http;

namespace Acesso.Repositorios.API
{
    public class EMAPIConexao
    {
        public Func<string> Url { private get; set; }

        private APIConexao _instancia;

        public APIConexao Instancie()
        {
            return _instancia ?? (_instancia = new APIConexao(Url()));
        }
    }

    public class APIConexao
    {
        private static readonly HttpClient _cliente = new HttpClient();
        //public readonly string _urlBase;
        public APIConexao(string urlBase)
        {
            //_urlBase = urlBase;
            _cliente.Timeout = TimeSpan.FromMinutes(5);
            _cliente.BaseAddress = new Uri(urlBase);
            _cliente.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        }

        private string MonteUrl(string controle, string acao)
        {
            return $"api/{controle}/{acao}";
        }

        public T Get<T>(string controle, string acao)
        {
            using (var responseMessage = _cliente.GetAsync(MonteUrl(controle, acao)))
            {
                responseMessage.Wait();
                var resultado = responseMessage.Result;
                if (!resultado.IsSuccessStatusCode)
                {
                    throw new InconsistenciaException($"Não foi possível Obter informações de {MonteUrl(controle, acao)}");
                }

                var content = resultado.Content.ReadAsStringAsync();
                content.Wait();
                return JsonConvert.DeserializeObject<T>(content.Result);
            }
        }

        public T Post<T>(string controle, string acao, object valor)
        {
            using (var responseMessage = _cliente.PostAsJsonAsync(MonteUrl(controle, acao), valor))
            {
                responseMessage.Wait();
                var resultado = responseMessage.Result;
                if (!resultado.IsSuccessStatusCode)
                {
                    throw new InconsistenciaException($"Não foi possível Obter informações de {MonteUrl(controle, acao)}");
                }

                var content = resultado.Content.ReadAsStringAsync();
                content.Wait();
                return JsonConvert.DeserializeObject<T>(content.Result);
            }
        }

        public void Post(string controle, string acao, object valor)
        {
            using (var responseMessage = _cliente.PostAsJsonAsync(MonteUrl(controle, acao), valor))
            {
                responseMessage.Wait();
                var resultado = responseMessage.Result;
                if (!resultado.IsSuccessStatusCode)
                {
                    throw new InconsistenciaException($"Não foi possível Obter informações de {MonteUrl(controle, acao)}");
                }
            }
        }

        public string Delete(string controle, string acao)
        {
            using (var responseMessage = _cliente.GetAsync(MonteUrl(controle, acao)))
            {
                var resultado = responseMessage.Result;
                if (!resultado.IsSuccessStatusCode)
                {
                    throw new InconsistenciaException($"Não foi possível Obter informações de {MonteUrl(controle, acao)}");
                }

                var content = resultado.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string>(content.Result);
            }
        }
    }
}
