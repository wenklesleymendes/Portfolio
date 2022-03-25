using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Anexos;
using EscolaPro.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IAnexoService
    {
        Task<DtoAnexo> Inserir(DtoAnexo curso);
        Task<bool> Deleter(int idAnexo);
        Task<DtoAnexo> Atualizar(DtoAnexo anexo);
        Task<string> Download(int idAnexo);
        Task<IEnumerable<DtoAnexo>> BuscarPorFiltro(AnexoFiltrar anexoFiltrar);
        Task<IEnumerable<DtoAnexo>> BuscarPorDespesa(int despesaId, bool documento);
        Task<string> DownloadPorTipoAnexo(int matriculaId, bool isComprovante = false);
        Task<DtoAnexo> InserirPendenciaDocumental(DtoAnexo dtoAnexo);
        Task<DtoAnexo> BuscarComprovante(int matriculaId);
        Task<DtoAnexo> RecusarDocumento(AnexoRecusar anexo);
        Task<int> ExisteAnexo(int solicitacaoId);
        Task<bool> DeleterDocumento(int? matriculaAlunoId, TipoAnexoEnum contratoProcuracaoEja);
        Task<DtoAnexo> DownloadDocumentoPorTipoEnum(int matriculaId, TipoAnexoEnum declaracaoPendenciaDocumental);
    }
}
