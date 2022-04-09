using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace MdPaciente.Dominio.Enums
{
    public enum EnumTipoExame
    {
        
        [Description("MMSS")]
        MMSS = 0,

        [Description("MMII")]
        MMII = 1,

        [Description("TOTAL")]
        TOTAL = 2,

        [Description("FACE")]
        FACE = 3,

        [Description("TRONCO")]
        TRONCO = 4,

        [Description("Sem Index")]
        SemIndex = 100

    }

}
