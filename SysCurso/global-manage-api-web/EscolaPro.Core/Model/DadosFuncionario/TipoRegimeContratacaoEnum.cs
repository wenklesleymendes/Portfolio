using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EscolaPro.Core.Model.Funcionario
{
    public enum RegimeContratacaoEnum
    {

        [Description("CLT Seg-Sex")]
        CLT_SEG_SEX = 1,

        [Description("Estágio Seg-Sex")]
        ESTAGIO_SEG_SEX = 2,

        [Description("Professor Autonômo")]
        PROFESSOR_AUTONOMO = 3,

        [Description("Professor CLT")]
        PROFESSOR_CLT = 4,

        [Description("Profissional Autonômo")]
        PROFISSIONAL_AUTONOMO = 5,

        [Description("CLT Seg-Sab")]
        CLT_SEG_SAB = 6,

        [Description("Estágio Seg-Sab")]
        ESTAGIO_SEG_SAB = 7,

        [Description("Autônomo - pré CLT de segunda a Sexta")]
        AUTONOMO_PRE_CLT_SEG_SEX = 8,

        [Description("Autônomo - pré CLT de segunda a sábado")]
        AUTONOMO_PRE_CLT_SEG_SAB = 9,

        [Description("Autônomo - pré estágio de segunda a Sexta")]
        AUTONOMO_PRE_ESTAGIO_SEG_SEX = 10,

        [Description("Autônomo - pré estágio de segunda a sábado")]
        AUTONOMO_PRE_ESTAGIO_SEG_SAB = 11
    }
}
