using EscolaPro.Core.Model.Tickets;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.TicketVO
{
    public class DtoMensagemTicket
    {
        public int Id { get; set; }
        public DateTime? DataAtendimento => DateTime.Now;
        public StatusTicketEnum StatusTicket { get; set; }
        public int TicketId { get; set; }
        public int UsuarioLogadoId { get; set; }
        public string Mensagem { get; set; }
        public ICollection<DtoUsuarioDestinarioTicket> UsuarioDestinarioTicket { get; set; }
        public IEnumerable<DtoAnexo> Anexo { get; set; }
        public int? DepartamentoId { get; set; }
        public int? UnidadeId { get; set; }
    }
}
