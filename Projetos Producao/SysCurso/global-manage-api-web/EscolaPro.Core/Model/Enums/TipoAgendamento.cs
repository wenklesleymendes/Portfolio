using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EscolaPro.Core.Model.Enums
{
    public enum TipoAgendamentoEnum
    {
        [Description("Sim, agendar um dia para matrícula.")]
        Secretaria = 1,

        [Description("Sim, agendar um dia para matrícula Delivery.")]
        Delivery = 2,

        [Description("Não, agendar um dia para matrícula.")]
        NaoAgendar = 3,
    }
}