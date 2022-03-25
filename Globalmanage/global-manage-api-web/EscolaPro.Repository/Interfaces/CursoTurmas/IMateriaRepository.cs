using EscolaPro.Core.Model.CursoTurma;
using EscolaPro.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IMateriaRepository : IDomainRepository<Materia>
    {
        Task<IEnumerable<Materia>> BuscarPorIdCurso(int idCurso);
    }
}
