using EscolaPro.Service.Dto.UnidadeVO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Dto.Unidades
{
    public class UnidadeValidator : AbstractValidator<DtoUnidadeRequest>
    {
        public UnidadeValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Informe o nome da unidade.");
            //.Must(AlunoMaiorIdade).WithMessage("O aluno é menor de idade");

            RuleFor(c => c.CNPJ)
              .MaximumLength(14).WithMessage("CNPJ contém no máximo 14 caracteres.")
              .NotEmpty().WithMessage("Informe o nome da unidade.");

            RuleFor(c => c.RazaoSocial).NotEmpty().WithMessage("Informe a razão social.");

            RuleFor(c => c.NomeFantasia).NotEmpty().WithMessage("Informe o nome fantasia.");

            RuleFor(c => c.Endereco.CEP).NotEmpty().WithMessage("Informe o cep.");
            RuleFor(c => c.Endereco.Rua).NotEmpty().WithMessage("Informe o nome da rua.");
            RuleFor(c => c.Endereco.Bairro).NotEmpty().WithMessage("Informe o bairro.");
            RuleFor(c => c.Endereco.Cidade).NotEmpty().WithMessage("Informe a cidade.");
            RuleFor(c => c.Endereco.Estado).NotEmpty().WithMessage("Informe o estado.");
            RuleFor(c => c.Endereco.Numero).NotEmpty().WithMessage("Informe o numero.");

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
