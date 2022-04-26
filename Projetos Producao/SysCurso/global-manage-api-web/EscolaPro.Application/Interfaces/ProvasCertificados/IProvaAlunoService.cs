using EscolaPro.Core.Model.Provas;
using EscolaPro.Service.Dto.AgendaProvaVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.ProvasCertificados
{
    public interface IProvaAlunoService
    {
        Task<DtoProvaAluno> Inserir(DtoProvaAlunoRequest dtoProvaAluno);
        Task<DtoProvaAluno> AtualizarStatusProva(DtoProvaAluno dtoProvaAluno);
        DtoProvaAluno BuscarPorMatriculaId(int matriculaId);
        Task<DtoProvaAluno> BuscarPorId(int provaAlunoId);
        Task<DtoProvaAlunoInformacoes> InformacoesCadastro(int matriculaId);
        Task<DtoProvaAluno> CadastrarCredenciais(DtoProvaAlunoRequest credenciais);
        IEnumerable<DtoAgendaProva> BuscarProvasDisponiveis(int colegioId, int cursoId, int unidadeId);
        Task<bool> CancelarInscricao(int provaAlunoId);
        Task<DtoProvaAluno> ImprimirFormulario(int provaAlunoId);
        Task TicketEnviar(int matriculaId, int UsuarioLogadoId);
        IEnumerable<DtoProvaAluno> BuscarProvasRealizadas(int matriculaId);

    }
}
