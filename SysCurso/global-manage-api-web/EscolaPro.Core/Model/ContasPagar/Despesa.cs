using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.Fornecedores;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ContasPagar
{
    public class Despesa : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeDespesa { get; set; }
        public Unidade Unidade { get; set; }
        public CentroCusto CentroCusto { get; set; }
        public Categoria Categoria { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public DateTime? DataVencimento { get; set; }
        public decimal ValorTotalDespesa { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string NumeroDocumento { get; set; }
        public TipoParcelaEnum TipoParcela { get; set; }
        public int QuantidadeParcela { get; set; }
        public string Observacao { get; set; }
        public ICollection<DespesaParcela> DespesaParcela { get; set; }
        public ICollection<Anexo> Documentos { get; set; }
        public int? CentroCustoId { get; set; }
        public int? CategoriaId { get; set; }
        public int? UnidadeId { get; set; }
        public int? FornecedorId { get; set; }
        public TipoDespesaEnum TipoDespesa { get; set; }
       
        //public ImpostoDespesa DespesaImposto { get; set; }
        //public int? DespesaImpostoId { get; set; }

        public string CodigoBanco { get; set; }
        public string NomeBanco { get; set; }
        public string NumeroAgencia { get; set; }
        public string NumeroConta { get; set; }
        public TipoContaBancariaEnum? TipoContaBancaria { get; set; }
        public DespesaGPS? DespesaGPS { get; set; }
        public DespesaDARF? DespesaDARF { get; set; }
        public int? DespesaGPSId { get; set; }
        public int? DespesaDARFId { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
