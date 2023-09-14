using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Repository.MatriculaAlunos
{
    public class CancelamentoIsencaoPagamentoRepository : DomainRepository<CancelamentoIsencaoPagamento>, ICancelamentoIsencaoPagamentoRepository
    {
        public CancelamentoIsencaoPagamentoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
    }
}
