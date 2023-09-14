using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class ChamadaOnibus : BaseEntity
    {
        public string numeroonibus { get; set; }
        public string localsaida { get; set; }
        public string destino { get; set; }
        public string dataprova { get; set; }
        public string horariosaidaonibus { get; set; }
        public List<HistoricoProvas> HistoricoProvas { get; set; }
    }
}
