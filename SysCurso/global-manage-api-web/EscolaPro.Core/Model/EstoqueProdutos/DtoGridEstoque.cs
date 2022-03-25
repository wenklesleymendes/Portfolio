using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.EstoqueProdutos
{
    public class DtoGridEstoque
    {
        public int Id { get; set; }
        public string Unidade { get; set; }
        public string NomeProduto { get; set; }
        public string CodigoInterno { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int QuantidadeSaida { get; set; }
    }
}
