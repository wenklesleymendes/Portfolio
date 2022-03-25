using EscolaPro.Core.Model.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.TicketVO
{
    public class DtoTicketRetorno
    {
        public int Id { get; set; }
        public DtoAssuntoTicket AssuntoTicket { get; set; }
        public int? IdFuncionarioResponsavel { get; set; }
        public DateTime? DataAtendimento { get; set; }
        public DateTime? DataAbertura { get; set; }
        public string NumeroProtocolo { get; set; }
        public StatusTicketEnum Status { get; set; }
        public ICollection<DtoDestinatarioTicket> DestinatarioTicket { get; set; }
        public int AssuntoTicketId { get; set; }
    }
}
