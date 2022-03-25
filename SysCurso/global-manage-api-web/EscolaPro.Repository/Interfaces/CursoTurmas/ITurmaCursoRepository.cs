using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface ITurmaCursoRepository : IDomainRepository<TurmaCurso>
    {
        Task<IEnumerable<TurmaCurso>> BuscarPorIdTurma(int id);
        Task Deletar(TurmaCurso turmaCurso);
        Task<IEnumerable<Turma>> BuscarPorCursoId(int cursoId, int unidadeId, int? usuarioLogadoId);
    }
}
