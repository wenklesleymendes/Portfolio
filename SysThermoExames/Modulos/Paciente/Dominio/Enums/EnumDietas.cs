using System.ComponentModel;

namespace MdPaciente.Dominio.Enums
{
    public enum EnumDietas
    {
        [Description("Hiperproteica")]
        Hiperproteica = 0,

        [Description("Hipocalórica")]
        Hipocalorica = 1,

        [Description("Herbalife")]
        Herbalife = 2,

        [Description("Sem Index")]
        SemIndex = 100

    }
}
