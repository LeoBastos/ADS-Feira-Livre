using ads.feira.application.DTO.Cupons;
using FluentValidation;

namespace ads.feira.application.Validators.Cupons
{
    public class CuponValidator : AbstractValidator<CuponDTO>
    {
        public CuponValidator()
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
