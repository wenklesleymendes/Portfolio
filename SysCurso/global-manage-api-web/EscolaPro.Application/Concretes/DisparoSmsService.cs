using EscolaPro.Core.Model;
using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Service.Dto.DisparoSmsVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.ReguaContato;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class DisparoSmsService : IDisparoSmsService
    {
        private readonly IReguaContatoHistoricoService _reguaContatoHistoricoService;

        public DisparoSmsService(IReguaContatoHistoricoService reguaContatoHistoricoService)
        {
            _reguaContatoHistoricoService = reguaContatoHistoricoService;
        }

        public async Task<bool> Enviar(SmsBody smsBody)
        {
            try
            {

                HttpClient httpClient = new HttpClient();

                var token = "1fd96ed99f23ece342f5a795dfe7f3d1d1021436";

                List<SmsBody> smsContatos = new List<SmsBody>();

                SmsBody body = new SmsBody
                {
                    numero = $"+55{smsBody.numero}",
                    servico = "short",
                    mensagem = smsBody.mensagem,
                    parceiro_id = "5034e65a0c",
                    codificacao = "0"
                };

                smsContatos.Add(body);

                var jsonContent = JsonConvert.SerializeObject(smsContatos);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                httpClient.BaseAddress = new Uri("https://api.disparopro.com.br/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                 //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

                var retorno = await httpClient.PostAsync("mt", contentString);

                if (retorno.IsSuccessStatusCode)
                {

                }

                var reguaContatoHistorico = new ReguaContatoHistorico();
                reguaContatoHistorico.AlunoId = smsBody.alunoId;
                reguaContatoHistorico.Titulo = smsBody.mensagem;
                reguaContatoHistorico.Texto = retorno.ToString();
                reguaContatoHistorico.DataEnvio = DateTime.Now;
                reguaContatoHistorico.TipoMensagem = TipoMensagemEnum.Sms;

                var retornoSalvar = await _reguaContatoHistoricoService.Inserir(reguaContatoHistorico);

                return retorno.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
