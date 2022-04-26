using EscolaPro.Core.Model.Tickets;
using EscolaPro.Service.Dto.AlunosVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.TicketVO
{
    public class DtoGridTicket
    {
        public int TicketId { get; set; }
        public string NumeroProtocolo { get; set; }
        public string Assunto { get; set; }
        public DateTime? DataAbertura { get; set; }
        public DateTime DataAtendimento { get; set; }
        public string SLA { get; set; }
        public StatusTicketEnum Status { get; set; }
        public string Atendente { get; set; }
        public string UsuarioResponsavel { get; set; }
        public DtoAlunoSimples Aluno { get; set; }
        public bool IsOcorrencia { get; set; }
    }
}
