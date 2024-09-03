using ads.feira.application.DTO.Reviews;
using FluentValidation;

namespace ads.feira.application.Validators.Reviews
{
    public class ReviewValidator : AbstractValidator<ReviewDTO>
    {
        public ReviewValidator()
        {
            RuleFor(c => c.UserId)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.ReviewContent)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(250)
                .WithMessage("minimo de 3 e maximo de 250 caracteres.");

            RuleFor(c => c.StoreId)
               .NotNull()
               .NotEmpty();

            RuleFor(c => c.Rate)
               .NotNull()
               .NotEmpty();
        }
    }
}
