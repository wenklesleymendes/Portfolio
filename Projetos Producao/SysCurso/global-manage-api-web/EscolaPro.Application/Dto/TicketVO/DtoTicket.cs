using EscolaPro.Core.Model.Tickets;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.TicketVO
{
    public class DtoTicket
    {
        public int Id { get; set; }
        //public DtoAssuntoTicket AssuntoTicket { get; set; }
        public int UsuarioLogadoId { get; set; }
        public DateTime? DataAtendimento { get; set; }
        public DateTime? DataAbertura { get; set; }
        public string NumeroProtocolo { get; set; }
        public StatusTicketEnum Status { get; set; }
        public DtoDestinatarioTicket DestinatarioTicket { get; set; }
        public int AssuntoTicketId { get; set; }
        public int? IdFuncionarioAtendente { get; set; }
        public int? MatriculaId { get; set; }
        public bool? BaixaBoleto { get; set; }
    }
}
