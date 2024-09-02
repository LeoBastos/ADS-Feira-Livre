using ads.feira.application.DTO.Categories;
using FluentValidation;

namespace ads.feira.application.Validators.Categories
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(150)
                .WithMessage("Nome deve ser preenchido.");

            RuleFor(c => c.Type)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tipo deve ser preenchido.");

            RuleFor(c => c.Description)
               .NotNull()
               .NotEmpty()
               .MinimumLength(3)
               .MaximumLength(250)
               .WithMessage("Descrição deve ser preenchido");

            RuleFor(c => c.Assets)
               .NotNull()
               .NotEmpty()
               .WithMessage("Adicione a imagem");
        }
    }
}
