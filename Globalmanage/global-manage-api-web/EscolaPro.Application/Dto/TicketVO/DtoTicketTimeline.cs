using EscolaPro.Core.Model.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.TicketVO
{
    public class DtoTicketTimeline
    {
        public int TicketId { get; set; }
        public StatusTicketEnum StatusTicket { get; set; }
        public List<DtoMensagensTimeLine> Mensagens { get; set; }
        public DtoDestinatarioTicket UsuarioResponsavel { get; set; }
    }

    public class DtoMensagensTimeLine
    {
        public string Unidade { get; set; }
        public string Atendente { get; set; }
        public DateTime Data { get; set; }
        public string Mensagem { get; set; }
        public int AnexoId { get; set; }
        public StatusTicketEnum StatusTicket { get; set; }
        public string ArquivoString { get; set; }
        public string Extensao { get; set; }
    }
}
