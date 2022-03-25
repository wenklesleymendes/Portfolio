using EscolaPro.Core.Model.Tickets;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.TicketVO
{
    public class DtoDestinatarioTicket
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int UsuarioLogadoId { get; set; }
        public string Mensagem { get; set; }
        //public DtoCentroCusto Departamento { get; set; }
        //public DtoUnidadeTicket Unidade { get; set; }
        public DateTime DataAtendimento => DateTime.Now;
        public List<DtoUsuarioDestinarioTicket> UsuarioDestinarioTicket { get; set; }
        public IEnumerable<DtoAnexo> Anexo { get; set; }
        public int? DepartamentoId { get; set; }
        public int? UnidadeId { get; set; }
        public StatusTicketEnum StatusTicket { get; set; }
    }
}
