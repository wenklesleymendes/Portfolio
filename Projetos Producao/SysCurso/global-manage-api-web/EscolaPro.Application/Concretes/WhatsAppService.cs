using EscolaPro.Core.Model;
using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Repository.Interfaces.ReguaContato;
using EscolaPro.Repository.Repository.ReguaContato;
using EscolaPro.Service.Concretes.ReguaContato;
using EscolaPro.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EscolaPro.Service.Interfaces.ReguaContato;

namespace EscolaPro.Service.Concretes
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly IReguaContatoHistoricoService _reguaContatoHistoricoService;

        public WhatsAppService(IReguaContatoHistoricoService reguaContatoHistoricoService)
        {
            _reguaContatoHistoricoService = reguaContatoHistoricoService;
        }

        /// <summary>
        /// Link. You can get it in your personal office.
        /// </summary>
        private string APIUrl = "https://api.chat-api.com/instance206567/";
        /// <summary>
        /// Token. You can get it in your personal office.
        /// </summary>
        //private string token = "owjyrbtwbyj5eo2i";
        /// <summary>
        /// Sends a message to this ID
        /// </summary>
        /// <param name="phone">Number phone</param>
        /// <param name="text">Message Text</param>
        /// <returns></returns>
        public async Task<string> SendMessage(string tokenUnidade, string phone,  string text, int alunoId)
        {
            //if (phone.Length < 10)
            phone = "55" + phone;

            var data = new Dictionary<string, string>()
            {
                {"phone",phone },
                { "body", text }
            };
            return await SendRequest(tokenUnidade, "message", JsonConvert.SerializeObject(data), alunoId);
        }
        /// <summary>
        /// The method makes a request to the server chat-api.com.
        /// </summary>
        /// <param name="method">API method as per documentation.</param>
        /// <param name="data">Json data</param>
        /// <returns></returns>
        private async Task<string> SendRequest(string tokenUnidade, string method, string data, int alunoId)
        {
            string url = $"{APIUrl}{method}?token={tokenUnidade}";
            var retorno = string.Empty;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("", content);
                retorno = await result.Content.ReadAsStringAsync();
                //return retorno;
            }

            try
            {
                var reguaContatoHistorico = new ReguaContatoHistorico();
                reguaContatoHistorico.AlunoId = alunoId;
                reguaContatoHistorico.Titulo = data;
                reguaContatoHistorico.Texto = retorno;
                reguaContatoHistorico.DataEnvio = DateTime.Now;
                reguaContatoHistorico.TipoMensagem = TipoMensagemEnum.WhatsApp;

                var retornoSalvar = await _reguaContatoHistoricoService.Inserir(reguaContatoHistorico);

                return retorno;
            }
            catch (Exception ex)
            {
                return "Falha no envio";
            }
        }
    }
}
