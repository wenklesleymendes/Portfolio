using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.EstoqueVO
{
    public class DtoProduto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public DateTime DataEntrada { get; set; }
        public int AlertaQuantidadeMinima { get; set; }
        public DtoUnidadeTurma Unidade { get; set; }
        public string CodigoNCM { get; set; }
        public string CodigoInterno { get; set; }
        public int? UnidadeId { get; set; }
        public ICollection<DtoItemProduto> ItemProduto { get; set; }

        
    }
}
