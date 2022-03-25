using EscolaPro.Core.Model.MetasComissoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MetasComissoesVO.Dashboard
{
    public class DtoMinhasComissoes
    {
        public int UnidadeId { get; set; }
        public string Data { get; set; }
        public bool ComissaoEquipe { get; set; }
        public decimal ValorComissao { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
        public int QuantidadePrimeiraParcelaPaga { get; set; }
    }
}
