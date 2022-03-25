using EscolaPro.Core.Model.DadosFuncionario;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface ICursoProfessorRepository : IDomainRepository<CursoProfessor>
    {
        Task<IEnumerable<CursoProfessor>> BuscarPorFuncionarioId(int idFuncionario);
    }
}
