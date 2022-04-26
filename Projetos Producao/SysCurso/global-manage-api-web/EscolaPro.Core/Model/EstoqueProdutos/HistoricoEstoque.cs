using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.EstoqueProdutos
{
    public class HistoricoEstoque : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int IdEstoque { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public TipoHistoricoEnum TipoHistorico { get; set; }
    }
}
