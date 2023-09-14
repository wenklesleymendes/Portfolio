using EscolaPro.Core.Model.CadastroAluno;
using EscolaPro.Repository.Interfaces.CadastroAluno;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Repository.CadastroAluno
{
    public class NacionalidadeRepository : DomainRepository<Nacionalidade>, INacionalidadeRepository
    {
        public NacionalidadeRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
