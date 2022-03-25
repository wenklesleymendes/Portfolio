using EscolaPro.Core.Model;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IUnidadeService
    {
        Task<DtoUnidadeResponse> Inserir(DtoUnidadeRequest model);
        Task<IEnumerable<DtoUnidadeResponse>> BuscarTodos(int usuarioLogadoId);
        Task<IEnumerable<DtoUnidadeResponse>> BuscarUnidadesTicket(int usuarioLogadoId);
        Task<DtoUnidadeResponse> BuscarPorId(int idUnidade, bool editar = true);
        Task<bool> Deletar(int idUnidade);

        Task<DtoAlunoFoto> UploadFoto(byte[] file, int unidadeId, string extensao);
        Task<DtoAlunoFoto> SelecionarFoto(int unidadeId);
        Task<bool> ExcluirFoto(int unidadeId);
    }
}
