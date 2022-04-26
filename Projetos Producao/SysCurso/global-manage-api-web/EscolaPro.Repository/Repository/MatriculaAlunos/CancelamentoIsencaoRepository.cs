using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Repository.MatriculaAlunos
{
    public class CancelamentoIsencaoRepository:DomainRepository<CancelamentoIsencao>, ICancelamentoIsencaoRepository
    {
        public CancelamentoIsencaoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
