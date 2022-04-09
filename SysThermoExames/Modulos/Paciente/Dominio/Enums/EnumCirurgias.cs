using System.ComponentModel;

namespace MdPaciente.Dominio.Enums
{
    public enum EnumCirurgias
    {
        [Description("Cirurgia Hepatobiliopancreática")]
        Hepatobiliopancreatica = 0,

        [Description("Cirurgia Histerectomia")]
        Histerectomia = 1,

        [Description("Cirurgia Fimose")]
        Fimose = 2,

        [Description("Cirurgia Otosclerose")]
        Otosclerose = 3,

        [Description("Cirurgia Mastoidectomia")]
        Mastoidectomia = 4,

        [Description("Cirurgia Adenóide")]
        Adenoide = 5,

        [Description("Cirurgia Amígdalas")]
        Amigdalas = 6,

        [Description("Cirurgia Colecistectomia")]
        Colecistectomia = 7,

        [Description("Cirurgia Hernioplastia")]
        Hernioplastia = 8,

        [Description("Cirurgia Apendicectomia")]
        Apendicectomia = 9,

        [Description("Cirurgia Gastrectomia")]
        Gastrectomia = 10,

        [Description("Cirurgia Angioplastia")]
        Angioplastia = 11,

        [Description("Cirurgia Mastectomia")]
        Mastectomia = 12,

        [Description("Cirurgia Bariátrica")]
        Bariatrica = 13,

        [Description("Sem Index")]
        SemIndex = 100
    }
}
