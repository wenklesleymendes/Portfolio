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
using System.Net.Http.Headers;

namespace EscolaPro.Service.Concretes
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly IReguaContatoHistoricoService _reguaContatoHistoricoService;
        private readonly IAlunoService _alunoService;
        public WhatsAppService(IAlunoService alunoService,
                               IReguaContatoHistoricoService reguaContatoHistoricoService)
        {
            _alunoService = alunoService;
            _reguaContatoHistoricoService = reguaContatoHistoricoService;
        }
        private string APIUrl = "http://api2.megaapi.com.br";

        public async Task<string> SendMessage(string instanceUnidade, string tokenUnidade, string phone, string text, int alunoId)
        {
            var aluno = await _alunoService.BuscarPorId(alunoId);
            if (!aluno.Contato.ReceberWhatsApp)
                return "";

            ContetWhatsApp data = new ContetWhatsApp(phone, text);
            return await SendRequest(instanceUnidade, tokenUnidade, "sendmessage", data, alunoId);
        }

        private async Task<string> SendRequest(string instanceUnidade, string tokenUnidade, string method, ContetWhatsApp data, int alunoId)
        {
            var retorno = string.Empty;

            instanceUnidade = "15464";
            tokenUnidade = "M_Y3z9PShFX2Dte";
            try
            {

                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{APIUrl}:{instanceUnidade}/{method}?token={tokenUnidade}"))
                    {
                        request.Content = new StringContent(JsonConvert.SerializeObject(data));
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                        var response = await httpClient.SendAsync(request);
                        retorno = await response.Content.ReadAsStringAsync();
                        var reguaContatoHistorico = new ReguaContatoHistorico();
                        reguaContatoHistorico.AlunoId = alunoId;
                        reguaContatoHistorico.Titulo = JsonConvert.SerializeObject(data);
                        reguaContatoHistorico.Texto = retorno;
                        reguaContatoHistorico.DataEnvio = DateTime.Now;
                        reguaContatoHistorico.TipoMensagem = TipoMensagemEnum.WhatsApp;

                        var retornoSalvar = await _reguaContatoHistoricoService.Inserir(reguaContatoHistorico);
                        return retorno;
                    }
                }
            }
            catch (Exception ex)
            {
                return "Falha no envio";
            }
        }
    }

    public class ContetWhatsApp
    {
        public ContetWhatsApp(string celular, string mensagem)
        {
            jid = $"55{celular}@s.whatsapp.net";
            body = mensagem;
        }
        public string jid { get; set; }
        public string body { get; set; }
    }
}
