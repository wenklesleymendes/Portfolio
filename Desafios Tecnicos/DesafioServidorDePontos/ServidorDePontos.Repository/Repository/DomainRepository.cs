using Microsoft.EntityFrameworkCore;
using ServidorDePontos.Core.Interfaces;
using ServidorDePontos.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorDePontos.Repository.Repository
{
    public class DomainRepository<TEntidade> : RepositoryBase<TEntidade>, IDomainRepository<TEntidade> where TEntidade : class, IIdentityEntity
    {
        protected DomainRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
