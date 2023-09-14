using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.EstoqueProdutos
{
    public class ItemProduto : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public DateTime DataEntrada { get; set; }
        public string NomeFornecedor { get; set; }
        public string CNPJ { get; set; }
        public string NumeroNotaFiscal { get; set; }
        public int QuantidadeEntrada { get; set; }
        public int QuantidadeSaida { get; set; }
        public int ProdutoId { get; set; }
    }
}
