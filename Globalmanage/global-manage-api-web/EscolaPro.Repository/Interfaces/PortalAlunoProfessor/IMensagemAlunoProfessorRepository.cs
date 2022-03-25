using EscolaPro.Core.Model.PortalAlunoProfessor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.PortalAlunoProfessor
{
    public interface IMensagemAlunoProfessorRepository : IDomainRepository<MensagemAlunoProfessor>
    {
        Task<IEnumerable<MensagemAlunoProfessor>> BuscarPorMatricula(int matriculaId);
        Task<IEnumerable<MensagemAlunoProfessor>> BuscarPorProfessor(int professorId);
    }
}
