using System.ComponentModel;

namespace MdPaciente.Dominio.Enums
{
    public enum EnumSindromes
    {
        [Description("Diabetes")]
        Diabetes = 0,

        [Description("Artrite Reumatóide")]
        ArtriteReumatoide = 1,

        [Description("Raymond")]
        Raymond = 2,

        [Description("Gota")]
        Gota = 3,

        [Description("Lupus")]
        Lupus = 4,

        [Description("Litiase Biliar")]
        LitiaseBiliar = 5,

        [Description("Litiase Renal")]
        LitiaseRenal = 6,

        [Description("Dislipidemia")]
        Dislipidemia = 7,

        [Description("Sinusite")]
        Sinusite = 8,

        [Description("Sem Index")]
        SemIndex = 100
    }
}
