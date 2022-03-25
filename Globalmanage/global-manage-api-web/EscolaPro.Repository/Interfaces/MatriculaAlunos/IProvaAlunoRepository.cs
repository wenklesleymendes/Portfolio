using EscolaPro.API.Dto;
using EscolaPro.Core.Model.PainelMatricula;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.MatriculaAlunos
{
    public interface IProvaAlunoRepository : IDomainRepository<ProvaAluno>
    {
        Task<ProvaAluno> BuscarPorId(int provaAlunoId);
        Task<IEnumerable<ProvaAluno>> BuscarPorMatriculaId(int matriculaId);
        ProvaAluno BuscarInscritoPorMatricula(int matriculaId);
        IEnumerable<ProvaAluno> BuscarProvasRealizadas(int matriculaId);
        Task<IEnumerable<ProvaAluno>> BuscarTodas();
        List<ProvaAluno> BuscarFiltro(FiltroHistoricoProvas filtro);
    }
}
