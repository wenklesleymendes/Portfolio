using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Provas
{
    public class AgendaProva : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public DateTime? InicioInscricao { get; set; }
        public DateTime? TerminoInscricao { get; set; }
        public DateTime? DataProva { get; set; }
        public int QuantidadeVagas { get; set; }
        public ICollection<UnidadeParticipanteProva> UnidadeParticipanteProva { get; set; }
        public TipoProvaEnum TipoProva { get; set; }
        public ColegioAutorizado ColegioAutorizado { get; set; }
        public int? ColegioAutorizadoId { get; set; }
        public ICollection<AgendaCurso> AgendaCurso { get; set; }
        public ICollection<UnidadeTransporteProva> UnidadeTransporteProvas { get; set; }
    }
}
