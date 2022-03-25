using EscolaPro.Core.Model.Anexos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.PainelMatricula.PlanoAluno;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.MatriculaAlunos
{
    public interface IMatriculaAlunoRepository : IDomainRepository<MatriculaAluno>
    {
        Task<IEnumerable<MatriculaAluno>> BuscarMinhasMatriculas(int alunoId, int usuarioLogadoId, bool consultaDocumentos = false);
        Task<MatriculaAluno> BuscarPorId(int matriculaId);
        Task<int> QuantidadeMatriculasCadastradas(int turmaId);
        Task<MatriculaAluno> SalvarPlanoPagamento(MatriculaAluno matricula, PlanoPagamentoAluno planoPagamentoAluno);
        Task<bool> VerificarMatriculaExistente(string numeroMatricula, int unidadeId);
        string BuscarUltimaMatricula(int unidadeId);
        Task<DocumentoPendente> ConsultarDocumentosPendentes(MatriculaAluno matriculaAluno);
        bool VerificarEnsinoMedio(int matriculaId);
        MatriculaAluno GetMatriculaProva(int matriculaId);
        MatriculaAluno GetInformacoesEmail(int matriculaId);
        string VerificarMatriculaExistente2(MatriculaAluno numeroMatricula);
        Task<bool> AlterarStatus(int matriculaId, bool status);
        Task<IList<MatriculaAluno>> GetRange(IEnumerable<int> ids);

    }
}
