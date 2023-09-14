using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.DadosFuncionario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Tickets
{
    public class MensagemTicket : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataCriacao { get; set; }
        public ICollection<Anexo> Anexo { get; set; }
        public Model.DadosFuncionario.Funcionario Funcionario { get; set; }
        public int TicketId { get; set; }
    }
}
