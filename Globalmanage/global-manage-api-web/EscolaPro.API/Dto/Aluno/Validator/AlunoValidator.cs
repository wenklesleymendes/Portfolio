using EscolaPro.Core.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Dto.Aluno.Validator
{
    public class AlunoValidator : AbstractValidator<EscolaPro.Core.Model.Aluno>
    {
        public AlunoValidator()
        {
            RuleFor(c => c.DataNascimento)
                .NotEmpty().WithMessage("Informe a data de nascimento do Aluno")
                .Must(AlunoMaiorIdade).WithMessage("O aluno é menor de idade");

            RuleFor(c => c.Contato.Email)
            .NotEmpty().WithMessage("Informe o e-mail.")
            .EmailAddress().WithMessage("E-mail inválido");


        }

        public static bool AlunoMaiorIdade(DateTime dataNascimento)
        {
            return dataNascimento <= DateTime.Now.AddYears(-18);
        }
    }
}
