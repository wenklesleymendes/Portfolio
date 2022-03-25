using EscolaPro.Core.Model;
using EscolaPro.Core.Model.CursoTurma;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface ICursoRepository : IDomainRepository<Curso>
    {
        Task<IEnumerable<Curso>> BuscarCursosComMateria();
        Task<IEnumerable<Materia>> InserirMateria(Materia materia);
        Task<bool> RemoverMateria(int idMateria);
        Task<Curso> BuscarPorId(int idCurso);
    }
}
