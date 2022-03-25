using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Repository
{
    public class TurmaRepository : DomainRepository<Turma>, ITurmaRepository
    {
        public TurmaRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
    }
}
