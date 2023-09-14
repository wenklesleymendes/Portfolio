using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace EscolaPro.Core.Model.ReguaContato
{
    public class ReguaContatoRegra : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public TipoMensagem TipoMensagem { get; set; }
        public string Nome { get; set; }
        public string ContaOrigem { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public int Prioridade { get; set; }
        public DateTime DataEnvio { get; set; }
        public TipoRegra TipoRegra { get; set; }
        public List<ReguaContatoParametro> ReguaContatoParametro { get; set; }

    }
}
