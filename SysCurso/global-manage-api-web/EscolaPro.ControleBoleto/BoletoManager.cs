using AutoMapper;
using BoletoNetCore;
using EscolaPro.Core.Model.ArquivoRemessa;
using EscolaPro.Core.Model.ArquivoRemessa.ArquivoSimples;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EscolaPro.ControleBoleto
{
    public class BoletoManager : IBoletoManager
    {
        private readonly IBanco _banco;
        private readonly IMapper _mapper;

        public BoletoManager(IMapper mapper)
        {
            _banco = Banco.Instancia(Bancos.Itau);
            _mapper = mapper;
        }

        public async Task<string> RegistrarBoletoItau(string access_token, ItauSimplesCorpoCobranca layoutItau)
        {
            try
            {
                var layoutItauTemp = layoutItau;

                if (layoutItauTemp.pagador.logradouro_pagador.Length > 40)
                    layoutItauTemp.pagador.logradouro_pagador = layoutItauTemp.pagador.logradouro_pagador.Substring(0, 40);

                string json = JsonConvert.SerializeObject(layoutItauTemp);

                var client = new RestClient("https://gerador-boletos.itau.com.br/router-gateway-app/public/codigo_barras/registro");

                var request = new RestRequest(Method.POST);

                request.AddHeader("accept", "application/vnd.itau");
                request.AddHeader("itau-chave", "9a6a013b-54df-49a5-bf99-f674761f5775");
                request.AddHeader("identificador", "09435113000194");
                request.AddHeader("access_token", access_token);

                request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

                request.RequestFormat = DataFormat.Json;

                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {

                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(response.Content);
                }

                Console.WriteLine(response.Content);
                return response.Content;
            }
            catch (Exception ex)
            {
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
    }
}
