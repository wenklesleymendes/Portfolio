using EM.Infra;
using EM.Infra.Excecoes;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMCatraca.WebApi.Controllers
{
    public class ApiControllerAbstract : ApiController
    {
        protected HttpResponseMessage MonteRespostaErro(string mensagem)
        {
            return MonteResposta(mensagem, HttpStatusCode.BadRequest);
        }

        protected HttpResponseMessage MonteRespostaSucesso(object resposta)
        {
            return MonteResposta(resposta, HttpStatusCode.OK);
        }

        private static HttpResponseMessage MonteResposta(object objeto, HttpStatusCode httpStatusCode)
        {
            var json = JsonConvert.SerializeObject(objeto);
            var resposta = new HttpResponseMessage
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"),
                StatusCode = httpStatusCode
            };
            return resposta;
        }

        protected async Task<HttpResponseMessage> AcaoDeProcessoAsync<T>(Func<T> consulta)
        {
            return await ContextoAssincrono.ExecuteTarefa(() =>
            {
                try
                {
                    var respota = consulta();
                    return MonteRespostaSucesso(respota);
                }
                catch (InconsistenciaException ex)
                {
                    return MonteRespostaSucesso(ex.Message);
                }
                catch (Exception ex)
                {
                    return MonteRespostaErro(ex.Message);
                }
            });
        }

        protected async Task<HttpResponseMessage> AcaoDeProcessoAsync(Action consulta)
        {
            return await ContextoAssincrono.ExecuteTarefa(() =>
            {
                try
                {
                    consulta?.Invoke();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch (InconsistenciaException ex)
                {
                    return MonteRespostaSucesso(ex.Message);
                }
                catch (Exception ex)
                {
                    return MonteRespostaErro(ex.Message);
                }
            });
        }
    }
}