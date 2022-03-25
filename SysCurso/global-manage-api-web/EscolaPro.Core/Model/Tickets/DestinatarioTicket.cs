using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.DadosFuncionario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Tickets
{
    public class DestinatarioTicket : IIdentityEntity
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int UsuarioLogadoId { get; set; }
        public string Mensagem { get; set; }
        public CentroCusto Departamento { get; set; }
        public Unidade Unidade { get; set; }
        public ICollection<UsuarioDestinarioTicket> UsuarioDestinarioTicket { get; set; }
        public IEnumerable<Anexo> Anexo { get; set; }
        public DateTime DataAtendimento { get; set; }
        public StatusTicketEnum StatusTicket { get; set; }
        public int? DepartamentoId { get; set; }
        public int? UnidadeId { get; set; }
    }
}
