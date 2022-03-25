using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MetasComissoesVO
{
    public class DtoComissao
    {
        public int Id { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public bool TipoComissao { get; set; }
        public DateTime? DataInicioVirgencia { get; set; }
        public DateTime? DataFimVirgencia { get; set; }
        public bool PeriodoIndeterminado { get; set; }
        public int QuantidadeParcelas { get; set; }
        public bool TotalParcelas { get; set; }
        //public DtoUnidadeTurma Unidade { get; set; }
        public int UnidadeId { get; set; }
        public ICollection<DtoComissaoParcelas> ComissaoParcelas { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
    }
}
