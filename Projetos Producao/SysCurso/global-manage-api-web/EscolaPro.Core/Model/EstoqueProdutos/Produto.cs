using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.EstoqueProdutos
{
    public class Produto : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public DateTime DataEntrada { get; set; }
        public int AlertaQuantidadeMinima { get; set; }
        public Unidade Unidade { get; set; }
        public string CodigoNCM { get; set; }
        public string CodigoInterno { get; set; }
        public int? UnidadeId { get; set; }
        public ICollection<ItemProduto> ItemProduto { get; set; }
    }
}
