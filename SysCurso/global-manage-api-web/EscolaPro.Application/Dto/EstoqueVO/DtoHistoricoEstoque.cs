using EscolaPro.Core.Model.EstoqueProdutos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.EstoqueVO
{
    public class DtoHistoricoEstoque
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public TipoHistoricoEnum TipoHistorico { get; set; }
    }
}
