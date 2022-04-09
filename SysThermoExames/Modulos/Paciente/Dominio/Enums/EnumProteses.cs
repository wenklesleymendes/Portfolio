using System.ComponentModel;

namespace MdPaciente.Dominio.Enums
{
    public enum EnumProteses
    {
        [Description("Mamária")]
        Mamaria = 0,

        [Description("Dentária Fixa")]
        DentariaFixa = 1,

        [Description("Dentária Móvel")]
        DentariaMovel = 2,

        [Description("Ocular Esquerda")]
        OcularEsquerda = 3,

        [Description("Ocular Direita")]
        OcularDireita = 4,

        [Description("Auditiva")]
        Auditiva = 5,

        [Description("Ortopédica Inferior Direita")]
        OrtopedicaInferiorDireita = 6,

        [Description("Ortopédica Inferior Esquerda")]
        OrtopedicaInferiorEsquerda = 7,

        [Description("Ortopédica Superior Direita")]
        OrtopedicaSuperiorDireita = 8,

        [Description("Ortopédica Superior Esquerda")]
        OrtopedicaSuperiorEsquerda = 9,

        [Description("Sem Index")]
        SemIndex = 100
    }
}
