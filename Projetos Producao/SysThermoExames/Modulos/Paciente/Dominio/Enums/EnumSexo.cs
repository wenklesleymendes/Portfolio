using System.ComponentModel;

namespace MdPaciente.Dominio.Enums
{
    public enum EnumSexo
    {
        [Description("Homem")]
        Homem = 0,

        [Description("Homem Trans")]
        HomemTrans = 1,

        [Description("Mulher")]
        Mulher = 2,

        [Description("Mulher Trans")]
        MulherTrans = 3,

        [Description("Sem Index")]
        SemIndex = 100

    }
}
