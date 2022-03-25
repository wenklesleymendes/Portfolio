using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EscolaPro.Core.Model.Enums
{
    public enum ScoreEnum
    {
        [Description("Lead Inicial")]
        LeadInicial = 1000,

        [Description("Lead cadastrada no dia anterior não matriculado.")]
        LeadCadastroDiaAnteior = 990,

        [Description("Lead agendado apos 1 dia sem matriculado.")]
        LeadAgendamentoSemMatriculaPrimeiorDia = 980,

        [Description("Lead agendado apos 1 dia sem matriculado.")]
        LeadAgendamentoSemMatriculaSegundoDia = 970,

        [Description("Lead agendado apos 1 dia sem matriculado.")]
        LeadAgendamentoSemMatriculaTerceiroDia = 960,

        [Description("Lead agendado apos 1 dia sem matriculado.")]
        LeadAgendamentoSemMatriculaQuartoDia = 950,

        [Description("Lead agendado apos 1 dia sem matriculado.")]
        LeadAgendamentoSemMatriculaQuintoDia = 940,

        [Description("Lead primeria tentativa.")]
        LeadPrimeiraTentativaContato = 930,

        [Description("Lead segunda tentativa")]
        LeadSegundaTentativaContato = 920,


        //980 Score - Lead agendada para 1 dia atrás > não matriculado 1 dia atrás.
        //970 Score – Lead agendada para 2 dias atrás > não matriculado 2 dias atrás.
        //960 Score – Lead agendada para 3 dias atrás > não matriculado 3 dias atrás.
        //950 Score – Lead agendada para 4 dias atrás > não matriculado 4 dias atrás.
        //940 Score – Lead agendada para 5 dias atrás > não matriculado 5 dias atrás.

        //930 Score – Tentativa de contato nº 1> Sem agendamento 1 dia


        // Dimunir o restante do dias retirando 10 pontos
        //920 Score - Tentativa de contato nº 2 > Sem agendamento 2 dia
        //910 Score - Tentativa de contato nº 3 > Sem agendamento 3 dia
        //900 Score - Tentativa de contato nº 4 > Sem agendamento 4 dia
        //890 Score - Tentativa de contato nº 5 > Sem agendamento 5 dia
        //880 Score - Tentativa de contato nº 6 > Sem agendamento 6 dia
        //870 Score - Tentativa de contato nº 7 > Sem agendamento 7 dia
        //860 Score - Tentativa de contato nº 8 > Sem agendamento 8 dia
        //850 Score - Tentativa de contato nº 9 > Sem agendamento 9 dia
        //840 Score - Tentativa de contato nº 10 > Sem agendamento 10 dia
        //830 Score - Tentativa de contato nº 11 > Sem agendamento 11 dia
        //820 Score - Tentativa de contato nº 12 > Sem agendamento 12 dia
        //810 Score - Tentativa de contato nº 13 > Sem agendamento 13 dia
        //800 Score - Tentativa de contato nº 14 > Sem agendamento 14 dia
        //790 Score - Tentativa de contato nº 15 > Sem agendamento 15 dia
    }
}
