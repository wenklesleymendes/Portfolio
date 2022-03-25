using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.AtendimentoVO
{
    public class DtoAtendimentoAgendamento
    {
        public int Id { get; set; }
        public int IdAtendimento { get; set; }
        public string NomedoCliente { get; set; }
        public string Celular { get; set; }
        public string HoraAgendamento { get; set; }
        public string DataAgendamento { get; set; }
        public string DataeHoradoUltimoContato { get; set; }
        public TipoAgendamentoEnum TipoAgendamento { get; set; }
        public SituacaAgendamento Situacao { get; set; }
        public string Observacoes { get; set; }
        public int UsuarioCadastro { get; set; }
    }
}
