using ads.feira.application.DTO.Products;
using FluentValidation;

namespace ads.feira.application.Validators.Products
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator()
        {
            RuleFor(c => c.StoreId)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.CategoryId)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.Name)
               .NotNull()
               .NotEmpty()
               .MinimumLength(3)
               .MaximumLength(250)
               .WithMessage("Nome deve ter minino 3 e maximo 250 caracteres");

            RuleFor(c => c.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(250)
                .WithMessage("Descrição deve ter minino 3 e maximo 250 caracteres");

            RuleFor(c => c.Assets)
                .NotNull()
                .NotEmpty()
                .WithMessage("Insira uma imagem do produto");

            RuleFor(c => c.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage("Informe o preço");
        }
    }
}
