using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.MetasComissoes
{
    public class Meta : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Unidade Unidade { get; set; }
        public DateTime? InicioMeta { get; set; }
        public DateTime? TerminoMeta { get; set; }
        public int Quantidade { get; set; }
        public decimal BonusPeriodo { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
        public int UnidadeId { get; set; }
        public ICollection<DetalhamentoMeta> DetalhamentoMeta { get; set; }
    }
}
