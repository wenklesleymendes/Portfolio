using EscolaPro.Core.Model.Provas;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AgendaProvaVO
{
    public class DtoAgendaProva
    {
        public int Id { get; set; }
        public DateTime? InicioInscricao { get; set; }
        public DateTime? TerminoInscricao { get; set; }
        public DateTime? DataProva { get; set; }
        public int QuantidadeVagas { get; set; }
        public ICollection<DtoUnidadeParticipanteProva> UnidadeParticipanteProva { get; set; }
        public TipoProvaEnum TipoProva { get; set; }
        public DtoColegioAutorizado ColegioAutorizado { get; set; }
        public int ColegioAutorizadoId { get; set; }
        public IEnumerable<DtoAgendaCurso> AgendaCurso { get; set; }
    }
}
