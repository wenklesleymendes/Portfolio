using BoletoNetCore;
using EscolaPro.Core.Model.ArquivoRemessa;
using EscolaPro.Core.Model.ArquivoRemessa.ArquivoSimples;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscolaPro.ControleBoleto
{
    public interface IBoletoManager
    {
        Task<string> RegistrarBoletoItau(string access_token, ItauSimplesCorpoCobranca layoutItau);
        List<Boleto> UploadArquivoRemessa(string path);
    }
}
