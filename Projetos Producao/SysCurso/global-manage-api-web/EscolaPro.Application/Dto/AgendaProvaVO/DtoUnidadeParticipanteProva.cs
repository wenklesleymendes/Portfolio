using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AgendaProvaVO
{
    public class DtoUnidadeParticipanteProva
    {
        public int Id { get; set; }
        public string HoraSaida { get; set; }
        public string LocalSaida { get; set; }
        public int UnidadeId { get; set; }
    }
}
