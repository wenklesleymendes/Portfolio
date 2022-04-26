using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Provas
{
    public class UnidadeParticipanteProva : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string HoraSaida { get; set; }
        public string LocalSaida { get; set; }
        public int UnidadeId { get; set; }
        public int AgendaProvaId { get; set; }
        public AgendaProva AgendaProva { get; set; }
        public ICollection<UnidadeTransporteProva> UnidadeTransporteProvas { get; set; }
    }
}
