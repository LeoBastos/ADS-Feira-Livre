using ads.feira.application.DTO.Cupons;
using ads.feira.domain.Entity.Cupons;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ads.feira.application.Validators.Cupons
{
    public class CreateCuponValidator : AbstractValidator<CreateCuponDTO>
    {
        public CreateCuponValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(150)
                .WithMessage("Nome deve ser preenchido.");

            RuleFor(c => c.Code)
                .NotNull()
                .NotEmpty()
                .WithMessage("Insira o Código do cupon.");

            RuleFor(c => c.Description)
               .NotNull()
               .NotEmpty()
               .MinimumLength(3)
               .MaximumLength(250)
               .WithMessage("Descrição deve ser preenchido");

            RuleFor(c => c.Expiration)
               .NotNull()
               .NotEmpty()
               .GreaterThanOrEqualTo(DateTime.UtcNow)
               .WithMessage("Insira uma data maior ou igual a atual");

            RuleFor(c => c.Discount)
              .NotNull()
              .NotEmpty()
              .WithMessage("Insira o valor do desconto");

            RuleFor(c => c.DiscountType)
              .NotNull()
              .NotEmpty()
              .WithMessage("Insira o tipo de desconto");
        }
    }
}
