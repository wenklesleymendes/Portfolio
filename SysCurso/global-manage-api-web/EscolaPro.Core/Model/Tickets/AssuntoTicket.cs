using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Tickets
{
    public class AssuntoTicket : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int TempoEmDias { get; set; }

        public ICollection<FuncionarioAssuntoTicket> FuncionarioAssuntoTicket { get; set; }
        public Unidade Unidade { get; set; }
        public int? UnidadeId { get; set; }
        public CentroCusto CentroCusto { get; set; }
        public int? CentroCustoId { get; set; }
    }
}
