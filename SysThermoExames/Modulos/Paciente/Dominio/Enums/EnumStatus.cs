using System.ComponentModel;

namespace MdPaciente.Dominio
{
    public enum EnumStatus
    {
        [Description("Imagens Coletadas")]
        ImagensColetadas = 0,

        [Description("Realizando Laudo")]
        RealizandoLaudo = 1,

        [Description("Laudo Pronto")]
        LaudoPronto = 2,

        [Description("Entregue")]
        Entregue = 3,

        [Description("Sem Index")]
        SemIndex = 100

    }
    
}
