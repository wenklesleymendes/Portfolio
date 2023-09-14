using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Repository.Interfaces.Fornecedores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Repository.Fornecedores
{
    public class CategoriaRepository : DomainRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
