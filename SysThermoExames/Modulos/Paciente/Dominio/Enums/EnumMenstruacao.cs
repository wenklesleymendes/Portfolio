

using System.ComponentModel;

namespace MdPaciente.Dominio.Enums
{
    public enum EnumMenstruacao
    {
        [Description("Ausente")]
        Ausente = 0,

        [Description("Irregular")]
        Irregular = 1,

        [Description("Dolorosa")]
        Dolorosa = 2,
                
        [Description("Sem Index")]
        SemIndex = 100

    }
}
