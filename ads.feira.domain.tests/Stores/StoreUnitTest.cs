using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Entity.Stores;
using FluentAssertions;

namespace ads.feira.domain.tests.Stores
{
    public class StoreUnitTest
    {
        [Fact(DisplayName = "Criar uma Loja com valores Válidos")]
        public void CreateStore_WithValidParameters_ResultObjectValidState()
        {
            // Act
            Action action = () => new Store(1, "storeOwner", "name", "categoryId", "description", "imagens", "storeNumber", false, "locations");

            // Assert
            action.Should()
                 .NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Criar Loja com Id Inválido")]
        public void CreateStore_NegativeIdValue_DomainExceptionInvalidId()
        {
            // Act
            Action action = () => new Store(-1, "storeOwner", "name", "categoryId", "description", "imagens", "storeNumber", false, "locations");

            // Assert
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                 .WithMessage("Id inválido.");
        }

        [Fact(DisplayName = "Loja com StoreOwner null")]
        public void CreateStoreOwner_WithNullValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, null, "name", "categoryId", "description", "imagens", "storeNumber", false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Lojista não pode ser nulo.");
        }

        [Fact(DisplayName = "Loja com Store Owner Menor que 3 Caracteres")]
        public void CreateStoreOwner_ShortReviewContentValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, "st", "name", "categoryId", "description", "imagens", "storeNumber", false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Minimo de 3 caracteres.");
        }

        [Fact(DisplayName = "Loja com Store Name null")]
        public void CreateStoreName_WithNullValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, "storeOwnere", null, "categoryId", "description", "imagens", "storeNumber", false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Nome não pode ser nulo.");
        }

        [Fact(DisplayName = "Loja com Store Name Menor que 3 Caracteres")]
        public void CreateStoreName_ShortReviewContentValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, "st", "name", "categoryId", "description", "imagens", "storeNumber", false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Minimo de 3 caracteres.");
        }

        [Fact(DisplayName = "Store com categoryId null")]
        public void CreateStore_WithCategoryIdNullValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, "storeOwnere", "name", null, "description", "imagens", "storeNumber", false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Categoria Id não pode ser nulo.");
        }

        [Fact(DisplayName = "Loja com Description null")]
        public void CreateDescription_WithNullValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, null, "name", "categoryId", null, "imagens", "storeNumber", false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Lojista não pode ser nulo.");
        }

        [Fact(DisplayName = "Loja com Description Menor que 3 Caracteres")]
        public void CreateDescription_ShorttValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, "st", "name", "categoryId", "de", "imagens", "storeNumber", false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Minimo de 3 caracteres.");
        }

        [Fact(DisplayName = "Loja com StoreNumber null")]
        public void CreateStoreNumber_WithNullValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, "storeOwner", "name", "categoryId", "description", "imagens", null, false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Número da Loja não pode ser nulo.");
        }
    }
}
