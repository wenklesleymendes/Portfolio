using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Atendimentos
{
    public class AtendimentoAgendamento : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int IdAtendimento { get; set; }
        public string Celular { get; set; }
        public string HoraAgendamento { get; set; }
        public string DataAgendamento { get; set; }
        public DateTime DataeHoradoUltimoContato { get; set; }
        public TipoAgendamentoEnum TipoAgendamento { get; set; }
    }
}
