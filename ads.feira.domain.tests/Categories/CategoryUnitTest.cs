using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Identity;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Enums.Products;
using FluentAssertions;

namespace ads.feira.domain.tests.Categories
{
    public class CategoryUnitTest
    {
        [Fact(DisplayName = "Criar Categoria com valores Válidos")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Id = "user123", UserName = "testuser", Email = "testuser@example.com" };

            // Act
            Action action = () => new Category(1, "Category Name", TypeCategoryEnum.ComidaSalgada, applicationUser, "Categoria Teste", "imagem");

            // Assert
            action.Should()
                 .NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Criar Categoria com Id Negativo")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Id = "user123", UserName = "testuser", Email = "testuser@example.com" };

            // Act
            Action action = () => new Category(-1, "Category Name", TypeCategoryEnum.ComidaSalgada, applicationUser, "Categoria Teste", "imagem");

            // Assert
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                 .WithMessage("Id inválido.");
        }

        [Fact(DisplayName = "Nome Menor que 3 Caracteres")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Id = "user123", UserName = "testuser", Email = "testuser@example.com" };

            // Act
            Action action = () => new Category(1, "Ca", TypeCategoryEnum.ComidaSalgada, applicationUser, "Categoria Teste", "imagem");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Minimo de 3 caracteres.");
        }

        [Fact(DisplayName = "Descrição Menor que 3 Caracteres")]
        public void CreateCategory_ShortDescriptionValue_DomainExceptionShortName()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Id = "user123", UserName = "testuser", Email = "testuser@example.com" };

            // Act
            Action action = () => new Category(1, "Cadastro teste", TypeCategoryEnum.ComidaSalgada, applicationUser, "C", "imagem");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Minimo de 3 caracteres.");
        }


        [Fact(DisplayName = "Sem Nome")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Id = "user123", UserName = "testuser", Email = "testuser@example.com" };

            // Act
            Action action = () => new Category(1, "", TypeCategoryEnum.ComidaSalgada, applicationUser, "C", "imagem");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Nome não pode ser nulo.");
        }

        [Fact(DisplayName = "Nome com Null Value")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Id = "user123", UserName = "testuser", Email = "testuser@example.com" };

            // Act
            Action action = () => new Category(1, null, TypeCategoryEnum.ComidaSalgada, applicationUser, "C", "imagem");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Descrição com Null Value")]
        public void CreateCategory_WithNullDescriptionValue_DomainExceptionInvalidName()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Id = "user123", UserName = "testuser", Email = "testuser@example.com" };

            // Act
            Action action = () => new Category(1, "Teste", TypeCategoryEnum.ComidaSalgada, applicationUser, null, "imagem");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Sem CreatedBy")]
        public void CreateCategory_WithNullCreatedByValue_DomainExceptionInvalidName()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Id = "user123", UserName = "testuser", Email = "testuser@example.com" };

            // Act
            Action action = () => new Category(1, "Teste", TypeCategoryEnum.ComidaSalgada, null, "Categoria A", "imagem");

            action.Should()
                .Throw<Validation.DomainExceptionValidation>();
        }

        //Collections

        [Fact(DisplayName = "Categoria Inicializa com Coleções Vázias")]
        public void CreateCategory_InitializesWithEmptyCollections()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Id = "user123", UserName = "testuser", Email = "testuser@example.com" };

            // Act
            var category = new Category(1, "Category Name", TypeCategoryEnum.ComidaSalgada, applicationUser, "Categoria Teste", "imagem");

            // Assert
            category.Stores.Should().BeEmpty("porque uma nova categoria deve ter uma coleção de lojas vazia.");
            category.Products.Should().BeEmpty("porque uma nova categoria deve ter uma coleção de produtos vazia.");
        }

        [Fact(DisplayName = "Adicionar Loja à Categoria")]
        public void AddStore_ToCategory_StoreAddedSuccessfully()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Id = "user123", UserName = "testuser", Email = "testuser@example.com" };
            var category = new Category(1, "Category Name", TypeCategoryEnum.ComidaSalgada, applicationUser, "Categoria Teste", "imagem");
            var store = Store.Create(1, "storage owner", "name", "2", "description", "imagens", "1", false, "locations");

            // Act
            category.Stores.Add(store);

            // Assert
            category.Stores.Should().Contain(store, "porque a loja foi adicionada à categoria.");
        }

        [Fact(DisplayName = "Adicionar Produto à Categoria")]
        public void AddProduct_ToCategory_ProductAddedSuccessfully()
        {
            // Arrange
            var applicationUser = new ApplicationUser { Id = "user123", UserName = "testuser", Email = "testuser@example.com" };
            var category = new Category(1, "Category Name", TypeCategoryEnum.ComidaSalgada, applicationUser, "Categoria Teste", "imagem");
            var product = Product.Create(1, "store123", "category123", "Product 1", "Description", "assets", 100.0m);

            // Act
            category.Products.Add(product);

            // Assert
            category.Products.Should().Contain(product, "porque o produto foi adicionado à categoria.");
        }
    }
}
