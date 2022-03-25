using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Pagamentos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.PainelMatricula
{
    public class CancelamentoIsencaoPagamento : BaseEntity,IIdentityEntity
    {
        public int Id { get; set; }
        public int PagamentoId { get; set; }
        public int CancelamentoIsencaoId { get; set; }
        public Pagamento Pagamento { get; set; }
        public CancelamentoIsencao CancelamentoIsencao { get; set; }
    }
}
