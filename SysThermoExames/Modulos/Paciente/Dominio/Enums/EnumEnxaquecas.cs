using System.ComponentModel;

namespace MdPaciente.Dominio.Enums
{
    public enum EnumEnxaquecas
    {
        [Description("Frontal")]
        Frontal = 0,

        [Description("Temporal Direita")]
        TemporalDireita = 1,

        [Description("Temporal Esquerda")]
        TemporalEsquerda = 2,

        [Description("Alternante")]
        Alternante = 3,

        [Description("Nuca")]
        Nuca = 4,

        [Description("Topo")]
        Topo = 5,

        [Description("Toda cabeça")]
        TodaCabeca = 6,

        [Description("Tiara")]
        Tiara = 7,

        [Description("Pulsátil")]
        Pulsatil = 8,

        [Description("Sem Index")]
        SemIndex = 100
    }
}
