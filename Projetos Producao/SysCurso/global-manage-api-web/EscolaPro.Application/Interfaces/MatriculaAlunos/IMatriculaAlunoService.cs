using EscolaPro.Core.Model;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.PainelMatricula.PlanoAluno;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.DocumentosAlunoVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.ProfessorVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.MatriculaAlunos
{
    public interface IMatriculaAlunoService
    {
        Task<DtoMatriculaAlunoResponse> MatricularAluno(DtoMatriculaAluno dtoMatriculaAluno);
        Task<IEnumerable<DtoListaMatriculaGrid>> BuscarMinhasMatriculas(int alunoId, int usuarioLogadoId);
        Task<DtoMatriculaAlunoResponse> BuscarPorId(int matriculaId);
        Task<DtoDocumentosPendentes> ConsultarDocumentosPendentes(MatriculaAluno matriculaAluno);
        Task<IEnumerable<DtoGridProfessorTurma>> ConsultarMeusProfessores(int matriculaId);
        Task<DtoDocumentoAluno> GerarDocumentosPendencia(int matriculaId);
        Task<DtoTurma> BuscarMinhaTurma(int matriculaId);
        Task<int> QuantidadeMatriculasCadastradas(int turmaId);
        Task<bool> Excluir(int matriculaId);
        Task<DtoMatriculaAlunoResponse> GerarNumeroMatricula(DtoMatriculaAlunoResponse matricula, PlanoPagamentoAluno planoPagamentoAluno, bool rollback = false);
        Task<bool> JaExistenteMatricula(int alunoId, int unidadeId);
        Task Update(MatriculaAluno matriculaAluno);
        Task<DtoMatriculaAlunoResponse> BuscarPorIdSimples(int id);
        Task SalvarPlanoPagamento(MatriculaAluno matriculaAluno, PlanoPagamentoAluno planoPagamentoAluno);
        bool VerificarEnsinoMedio(int matriculaId);
        Task AtivarAluno(int matriculaAlunoId);
    }
}
