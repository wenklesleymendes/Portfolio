using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.MatriculaAlunos
{
    public interface ISolicitacaoService
    {
        Task<IEnumerable<DtoSolicitacao>> BuscarTodos();
        Task<DtoSolicitacao> Inserir(DtoSolicitacao dtoSolicitacao);
        Task<DtoSolicitacao> BuscarPorId(int solicitacaoId);
        Task<bool> Excluir(int solicitacaoId);
        Task<IEnumerable<DtoSolicitacao>> BuscarPorCursoId(int cursoId, int matriculaId, int usuarioId);
        Task<DtoAlunoFoto> UploadFoto(byte[] file, int solicitacaoId, string extensao);
        Task<DtoAlunoFoto> SelecionarFoto(int solicitacaoId);
        Task<bool> ExcluirFoto(int solicitacaoId);
    }
}
