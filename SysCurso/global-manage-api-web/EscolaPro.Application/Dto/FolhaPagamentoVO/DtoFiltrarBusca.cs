using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Service.Dto.FornecedorVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FolhaPagamentoVO
{
    public class DtoFiltrarBusca
    {
        public DateTime? InicioPeriodo { get; set; }
        public DateTime? FimPeriodo { get; set; }
        public int? UnidadeId { get; set; }
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? Categoria { get; set; }
        public StatusPagamentoEnum? StatusPagamento { get; set; }
        public TipoPagamentoEnum? TipoPagamento { get; set; }
        public TipoPessoaEnum? TipoPessoa { get; set; }
    }
}
