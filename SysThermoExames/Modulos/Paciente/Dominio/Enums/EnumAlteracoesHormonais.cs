using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdPaciente.Dominio.Enums
{
    public enum EnumAlteracoesHormonais
    {
        [Description("Hipotireoidismo")]
        Hipotireoidismo = 0,

        [Description("Amenorreia")]
        Amenorreia = 1,

        [Description("Hiperinsulismo")]
        Hiperinsulismo = 2,

        [Description("Hipercortisolismo")]
        Hipercortisolismo = 3,

        [Description("Hipertireoidismo")]
        Hipertireoidismo = 4,

        [Description("Doença de Cushing")]
        DoencaCushing = 5,

        [Description("Acromegalia")]
        Acromegalia = 6,

        [Description("Sem Index")]
        SemIndex = 100

    }
}
