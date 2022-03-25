using BoletoNetCore;
using EscolaPro.Core.Model.ArquivoRemessa;
using EscolaPro.Core.Model.ArquivoRemessa.ArquivoSimples;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.PagamentosVO;
using EscolaPro.Service.Dto.RegistroCobrancaVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IRegistroCobrancaService
    {
        Task<DtoCorpoCobranca> GerarBoleto(ItauSimplesCorpoCobranca dtoCorpoCobranca);
        Task<string> BoletoImpressoPdf(DtoCorpoCobranca dtoCorpoCobranca, DtoMatriculaAlunoResponse dtoMatriculaAluno, string descricaoParcela, Pagamento dtoPagamento);

        Task UploadArquivoRemessa(List<DtoBoleto> boletos);
    }
}
