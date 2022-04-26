using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.PainelMatricula;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Tickets
{
    public class Ticket : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public AssuntoTicket AssuntoTicket { get; set; }
        public int UsuarioLogadoId { get; set; }
        public int? IdFuncionarioAtendente { get; set; }
        public DateTime? DataAtendimento { get; set; }
        public DateTime? DataAbertura { get; set; }
        public string NumeroProtocolo { get; set; }
        public StatusTicketEnum Status { get; set; }
        public ICollection<DestinatarioTicket> DestinatarioTicket { get; set; }
        public int AssuntoTicketId { get; set; }
        public int? MatriculaId { get; set; }
        public MatriculaAluno Matricula { get; set; }
    }
}
