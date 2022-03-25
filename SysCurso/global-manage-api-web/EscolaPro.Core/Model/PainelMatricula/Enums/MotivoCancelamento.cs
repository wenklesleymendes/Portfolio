using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EscolaPro.Core.Model.PainelMatricula.Enums
{
    public enum MotivoCancelamento
    {
        [Description("Desempregado")]
        [Display(Name = "Desempregado")]
        Desempregado = 1,
        [Description("Dificuldade financeira")]
        [Display(Name = "Dificuldade financeira")]
        Dificuldadefinanceira = 2,
        [Description("Mudou de endereço")]
        [Display(Name = "Mudou de endereço")]
        MudoueEndereco = 3,
        [Description("Matriculou-se em outra escola")]
        [Display(Name = "Matriculou-se em outra escola")]
        MatriculaOutraEscola = 4,
        [Description("Saúde")]
        [Display(Name = "Saúde")]
        Saude = 5,
        [Description("Decidiu concluir em colégio publico")]
        [Display(Name = "Decidiu concluir em colégio publico")]
        ConluirColegioPublico = 6,
        [Description("Concluiu pelo Encceja")]
        [Display(Name = "Concluiu pelo Encceja")]
        ConcluiuEncceja = 7,
        [Description("Já concluiu o ensino médio")]
        [Display(Name = "Já concluiu o ensino médio")]
        ConcluiuEnsinoMedio = 8,
        [Description("Não gostou do curso")]
        [Display(Name = "Não gostou do curso")]
        NaoGostouCurso = 9,
        [Description("Não se adaptou as aulas")]
        [Display(Name = "Não se adaptou as aulas")]
        NaoAdaptouAula = 10,
        [Description("Motivo pessoal")]
        [Display(Name = "DesemprMotivo pessoalegado")]
        MotivoPessoal = 11,
        [Description("Outros")]
        [Display(Name = "Outros")]
        Outros = 12

    }
}
