using ads.feira.application.DTO.Stores;
using FluentValidation;

namespace ads.feira.application.Validators.Stores
{
    public class UpdateStoreValidator : AbstractValidator<UpdateStoreDTO>
    {
        public UpdateStoreValidator()
        {
            RuleFor(s => s.StoreOwnerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Store de possuir um Id do usuário");

            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(150)
                .WithMessage("Nome deve ser preenchido.");

            RuleFor(s => s.CategoryId)
               .NotNull()
               .NotEmpty()
               .WithMessage("Store de possuir um Id de categoria");

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

            RuleFor(c => c.StoreNumber)
               .NotNull()
               .NotEmpty()
               .WithMessage("Store deve ter um número");

            RuleFor(c => c.Locations)
               .NotNull()
               .NotEmpty()
               .WithMessage("Store deve locação gps");

        }
    }
}
