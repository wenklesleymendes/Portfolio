using EscolaPro.Core.Model.PortalAlunoProfessor;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.PortalAlunoProfessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.PortalAlunoProfessor
{
    public class MensagemAlunoProfessorRepository : DomainRepository<MensagemAlunoProfessor>, IMensagemAlunoProfessorRepository
    {
        public MensagemAlunoProfessorRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<MensagemAlunoProfessor>> BuscarPorMatricula(int matriculaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MensagemAlunoProfessor>> BuscarPorProfessor(int professorId)
        {
            throw new NotImplementedException();
        }
    }
}
