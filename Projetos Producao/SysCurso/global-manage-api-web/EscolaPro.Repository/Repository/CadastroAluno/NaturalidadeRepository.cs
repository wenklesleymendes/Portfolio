using EscolaPro.Core.Model.CadastroAluno;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.CadastroAluno;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Repository.CadastroAluno
{
    public class NaturalidadeRepository : DomainRepository<Naturalidade>, INaturalidadeRepository
    {
        public NaturalidadeRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
