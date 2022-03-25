using EscolaPro.Core.Model.ContasPagar;
using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Service.Dto.FornecedorVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscolaPro.Service.Dto.ContasAPagarVO
{
    public class DtoDespesa
    {
        public int Id { get; set; }
        public string NomeDespesa { get; set; }
        public DtoUnidadeTurma Unidade { get; set; }
        public DtoCentroCusto CentroCusto { get; set; }
        public DtoCategoria Categoria { get; set; }
        public DtoFornecedorResponse Fornecedor { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public string CodigoBarras { get; set; }
        public DateTime? DataVencimento { get; set; }
        public decimal? ValorTotalDespesa { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string NumeroDocumento { get; set; }
        public TipoParcelaEnum TipoParcela { get; set; }
        public int QuantidadeParcela { get; set; }
        public string Observacao { get; set; }
        public IEnumerable<DtoDespesaParcela> DespesaParcela { get; set; }
        public IEnumerable<DtoAnexo> Documentos { get; set; }
        public int? CentroCustoId { get; set; }
        public int? CategoriaId { get; set; }
        public int? UnidadeId { get; set; }
        public int? FornecedorId { get; set; }
        public bool Quitado => DespesaParcela.Where(x => x.StatusPagamento == StatusPagamentoEnum.AReceber).Count() > 0 ? false : true;
        public TipoDespesaEnum TipoDespesa { get; set; }
        //public DtoImpostoDespesa DespesaImposto { get; set; }
        //public int? DespesaImpostoId { get; set; }

        public string CodigoBanco { get; set; }
        public string NomeBanco { get; set; }
        public string NumeroAgencia { get; set; }
        public string NumeroConta { get; set; }
        public TipoContaBancariaEnum? TipoContaBancaria { get; set; }

        public DtoDespesaGPS? DespesaGPS { get; set; }
        public DtoDespesaDARF? DespesaDARF { get; set; }
        public int? DespesaGPSId { get; set; }
        public int? DespesaDARFId { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
