using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class HistoricoOcorrencias : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? UnidadeId { get; set; }
    }
}
