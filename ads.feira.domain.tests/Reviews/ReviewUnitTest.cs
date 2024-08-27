﻿using ads.feira.domain.Entity.Reviews;
using FluentAssertions;

namespace ads.feira.domain.tests.Reviews
{
    public class ReviewUnitTest
    {
        [Fact(DisplayName = "Criar Review com valores Válidos")]
        public void CreateReview_WithValidParameters_ResultObjectValidState()
        {
            // Act
            Action action = () => new Review(1, "userId", "ReviewContent", "StoreId", 5);

            // Assert
            action.Should()
                 .NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Criar Review com Id Inválido")]
        public void CreateReview_NegativeIdValue_DomainExceptionInvalidId()
        {
            // Act
            Action action = () => new Review(-1, "userId", "ReviewContent", "StoreId", 5);

            // Assert
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                 .WithMessage("Id inválido.");
        }

        [Fact(DisplayName = "Review Content Menor que 3 Caracteres")]
        public void CreateReview_ShortReviewContentValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Review(1, "userId", "Re", "StoreId", 5);

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Minimo de 3 caracteres.");
        }

        [Fact(DisplayName = "Rate Inválida")]
        public void CreateReview_WithInvaldRateValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Review(1, "userId", "ReviewContent", "StoreId", -1);

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Rate deve ter um valor entre 0 e 5");
        }

        [Fact(DisplayName = "Rate Maior que 5")]
        public void CreateReview_WithdRateValueMore5_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Review(1, "userId", "ReviewContent", "StoreId", 6);

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Rate deve ter um valor entre 0 e 5");
        }
    }
}