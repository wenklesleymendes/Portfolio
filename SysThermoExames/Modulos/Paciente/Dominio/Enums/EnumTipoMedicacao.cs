using System.ComponentModel;

namespace MdPaciente.Dominio.Enums
{
    public enum EnumTipoMedicacao
    {
        [Description("Omeprazol")]
        Omeprazol = 0,

        [Description("Corticosteroide")]
        Corticosteroide = 1,

        [Description("Antiarritmico")]
        Antiarritmico = 2,

        [Description("Hormônio tireoidiano")]
        HormonioTireoidiano = 3,

        [Description("Antidepressivo")]
        Antidepressivo = 4,

        [Description("Antihipertensivo")]
        Antihipertensivo = 5,

        [Description("Sem Index")]
        SemIndex = 100
    }
}
