using EscolaPro.Core.Model.DadosFuncionario;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IMateriaCursoProfessorRepository : IDomainRepository<MateriaCursoProfessor>
    {
        Task<IEnumerable<MateriaCursoProfessor>> BuscarPorCurso(int idCursoProfessor);
    }
}
