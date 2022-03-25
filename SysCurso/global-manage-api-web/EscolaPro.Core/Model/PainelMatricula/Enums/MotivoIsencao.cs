using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EscolaPro.Core.Model.PainelMatricula.Enums
{
    public enum MotivoIsencao
    {
        [Description("Nova matrícula")]
        NovaMatricula = 1,
        [Description("Apresentou atestado médico")]
        AtestadoMedico = 2,
        [Description("Acordo Juridico")]
        AcordoJuridico = 3,
        [Description("Acordo administrativo")]
        AcordoAdministrativo = 4,
        [Description("Outros")]
        Outros = 5
    }
}
