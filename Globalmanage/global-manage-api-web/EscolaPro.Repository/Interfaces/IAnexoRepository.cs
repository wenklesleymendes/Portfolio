using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Anexos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IAnexoRepository : IDomainRepository<Anexo>
    {
        Task<IEnumerable<Anexo>> BuscarAnexo(AnexoFiltrar anexoFiltrar);
        Task<Anexo> DownloadArquivo(int idAnexo);
        Task<Anexo> DownloadComprovanteBancario(int idFolhaPagamento);
        Task<IEnumerable<Anexo>> BuscarPorIdDespesa(int despesaId, bool documento);
        Task<Anexo> DownloadPorTipoAnexo(int matriculaId, bool isComprovante);
        Task<Anexo> BuscarPorId(int anexoId);
        Task<int> ExisteAnexo(int solicitacaoAlunoId);
        Task<bool> DeleterDocumento(int? matriculaAlunoId, TipoAnexoEnum tipoAnexo);
        Task<Anexo> DownloadDocumentoPorTipoEnum(int matriculaId, TipoAnexoEnum tipoAnexoEnum);
        bool ExisteContrato(int matriculaId);
    }
}
