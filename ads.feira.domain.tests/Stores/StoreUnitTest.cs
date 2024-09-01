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
            Action action = () => new Store(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "name", 1, "description", "imagens", "storeNumber", false, "locations");

            // Assert
            action.Should()
                 .NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Criar Loja com Id Inválido")]
        public void CreateStore_NegativeIdValue_DomainExceptionInvalidId()
        {
            // Act
            Action action = () => new Store(-1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "name", 1, "description", "imagens", "storeNumber", false, "locations");

            // Assert
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                 .WithMessage("Id inválido.");
        }

        //[Fact(DisplayName = "Loja com StoreOwner null")]
        //public void CreateStoreOwner_WithNullValue_DomainExceptionShortName()
        //{           
        //    // Act
        //    Action action = () => new Store(1, Guid.Parse(null), "name", 1, "description", "imagens", "storeNumber", false, "locations");

        //    action.Should()
        //        .Throw<Validation.DomainExceptionValidation>()
        //           .WithMessage("Lojista não pode ser nulo.");
        //}

        [Fact(DisplayName = "Loja com Store Name null")]
        public void CreateStoreName_WithNullValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", null, 1, "description", "imagens", "storeNumber", false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Nome não pode ser nulo.");
        }

        [Fact(DisplayName = "Loja com Store Name Menor que 3 Caracteres")]
        public void CreateStoreName_ShortReviewContentValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "n", 1, "description", "imagens", "storeNumber", false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Minimo de 3 caracteres.");
        }

        [Fact(DisplayName = "Store com categoryId null")]
        public void CreateStore_WithCategoryIdNullValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "name", -1, "description", "imagens", "storeNumber", false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Id inválido.");
        }

        [Fact(DisplayName = "Loja com Description null")]
        public void CreateDescription_WithNullValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "name", 1, null, "imagens", "storeNumber", false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Descrição não pode ser nulo.");
        }

        [Fact(DisplayName = "Loja com Description Menor que 3 Caracteres")]
        public void CreateDescription_ShorttValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "name", 1, "de", "imagens", "storeNumber", false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Minimo de 3 caracteres.");
        }

        [Fact(DisplayName = "Loja com StoreNumber null")]
        public void CreateStoreNumber_WithNullValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Store(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "name", 1, "description", "imagens", null, false, "locations");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Número da Loja não pode ser nulo.");
        }
    }
}
