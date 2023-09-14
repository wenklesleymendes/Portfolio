using EscolaPro.API.Dto.Contato;
using EscolaPro.Core.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Dto.Validator
{
    public class ContatoValidator : AbstractValidator<DtoContatoResponse>
    {
        public ContatoValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Informe o e-mail.")
                .EmailAddress().WithMessage("E-mail inválido");

        }
    }
}
