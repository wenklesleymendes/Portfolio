﻿using EscolaPro.Core.Model.Tickets;
using EscolaPro.Service.Dto.ControleUsuarioVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.TicketVO
{
    public class DtoFiltrarTicket
    {
        public int? UnidadeId { get; set; }
        public DateTime? PeriodoAberturaInicio { get; set; }
        public DateTime? PeriodoAberturaFim { get; set; }
        public StatusTicketEnum? StatusTickets { get; set; }
        public string NumeroProtocolo { get; set; }
        public string NumeroMatricula { get; set; }
        public string NomeResponsavel { get; set; }
        public string NomeAluno { get; set; }
        public int? AssuntoTicketId { get; set; }
        public int UsuarioLogadoId { get; set; }
        public int? DepartamentoId { get; set; }
        public int? MatriculaId { get; set; }
        public bool? UsuarioFinanceiro { get; set; }
        public bool VerTudo { get; set; }
        public bool FiltroAvancado { get; set; }
        public DtoUsuario FiltroUsuario { get; set; }
    }
}