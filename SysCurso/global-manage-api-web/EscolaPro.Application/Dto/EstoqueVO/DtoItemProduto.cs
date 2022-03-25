using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.EstoqueVO
{
    public class DtoItemProduto
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
