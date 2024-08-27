using ads.feira.domain.Entity.Products;
using FluentAssertions;

namespace ads.feira.domain.tests.Products
{
    public class ProductUnitTeste
    {
        [Fact(DisplayName = "Criar Produto com valores Válidos")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            // Act
            Action action = () => new Product(1, "storeId", "categoryId", "name", "description", "imagens", 15);

            // Assert
            action.Should()
                 .NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Criar Produto com Id Inválido")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            // Act
            Action action = () => new Product(-1, "storeId", "categoryId", "name", "description", "imagens", 15);

            // Assert
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                 .WithMessage("Id inválido.");
        }

        [Fact(DisplayName = "Nome Menor que 3 Caracteres")]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Product(1, "storeId", "categoryId", "n", "description", "imagens", 15);

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Minimo de 3 caracteres.");
        }

        [Fact(DisplayName = "Descrição Menor que 3 Caracteres")]
        public void CreateProduct_ShortDescriptionValue_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Product(1, "storeId", "categoryId", "name", "de", "imagens", 15);

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Minimo de 3 caracteres.");
        }

        [Fact(DisplayName = "Produto com Valor inválido")]
        public void CreateProduct_WithInvalidPrice_DomainExceptionShortName()
        {
            // Act
            Action action = () => new Product(1, "storeId", "categoryId", "name", "description", "imagens", -15);

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Valor de preço inválido");
        }


        //Collections
        [Fact(DisplayName = "Produto Inicializa com Coleções Vázias")]
        public void CreateProduct_InitializesWithEmptyCollections()
        {
            // Act
            var product = new Product(1, "storeId", "categoryId", "name", "description", "imagens", 15);

            // Assert
            product.AvailableCoupons.Should().BeEmpty("porque um novo produto deve ter uma coleção de lojas vazia.");
        }
    }
}
