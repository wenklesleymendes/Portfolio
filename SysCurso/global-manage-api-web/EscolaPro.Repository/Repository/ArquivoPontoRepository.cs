using EscolaPro.Core.Model.ControlePontoEletronico;
using EscolaPro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Repository
{
    public class ArquivoPontoRepository : DomainRepository<ArquivoPonto>, IArquivoPontoRepository
    {
        public ArquivoPontoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
