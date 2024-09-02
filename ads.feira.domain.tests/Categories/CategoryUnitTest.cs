using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Enums.Products;
using ads.feira.domain.Validation;
using FluentAssertions;

namespace ads.feira.domain.tests.Categories
{
    public class CategoryUnitTest
    {
        [Fact(DisplayName = "Criar Categoria com valores Válidos")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            // Arrange          
            

            // Act
            Action action = () => new Category(1, "Category Name", "Categoria Teste", "imagem", TypeCategoryEnum.ComidaSalgada);

            // Assert
            action.Should()
                 .NotThrow<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Criar Categoria com Id Negativo")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            // Arrange
            

            // Act
            Action action = () => new Category(-1, "Category Name", "Categoria Teste", "imagem", TypeCategoryEnum.ComidaSalgada);

            // Assert
            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                 .WithMessage("Id inválido.");
        }

        [Fact(DisplayName = "Nome Menor que 3 Caracteres")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            // Arrange
            

            // Act
            Action action = () => new Category(1, "Ca",  "Categoria Teste", "imagem", TypeCategoryEnum.ComidaSalgada);

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Minimo de 3 caracteres.");
        }

        [Fact(DisplayName = "Descrição Menor que 3 Caracteres")]
        public void CreateCategory_ShortDescriptionValue_DomainExceptionShortName()
        {
            // Arrange
            

            // Act
            Action action = () => new Category(1, "Cadastro teste", "C", "imagem", TypeCategoryEnum.ComidaSalgada);

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                   .WithMessage("Minimo de 3 caracteres.");
        }

        [Fact(DisplayName = "Sem Nome")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            // Arrange
           

            // Act
            Action action = () => new Category(1, "", "C", "imagem", TypeCategoryEnum.ComidaSalgada);

            action.Should()
                .Throw<Validation.DomainExceptionValidation>()
                .WithMessage("Nome não pode ser nulo.");
        }

        [Fact(DisplayName = "Nome com Null Value")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            // Arrange
           

            // Act
            Action action = () => new Category(1, null, "C", "imagem", TypeCategoryEnum.ComidaSalgada);

            action.Should()
                .Throw<Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Descrição com Null Value")]
        public void CreateCategory_WithNullDescriptionValue_DomainExceptionInvalidName()
        {
            // Arrange
            

            // Act
            Action action = () => new Category(1, "Teste", null, "imagem", TypeCategoryEnum.ComidaSalgada);

            action.Should()
                .Throw<Validation.DomainExceptionValidation>();
        }


        #region METHODS

        [Fact(DisplayName = "Adicionar Produto Valido")]
        public void AddProduct_ValidProduct_ShouldAddToCollection()
        {
            // Arrange
            var category = new Category(1, "Test Category", "Description", "assets.jpg", TypeCategoryEnum.ComidaSalgada);
            var product = new Product(1, 2, category.Id, "Test Product", "Description", "asset.jpg", 10.99m, 0);

            // Act
            category.AddProduct(product);

            // Assert
            Assert.Single(category.Products);
            Assert.Contains(product, category.Products);
        }

        [Fact(DisplayName = "Tentar Adicionar Produto com objeto Null")]
        public void AddProduct_NullProduct_ShouldThrowDomainExceptionValidation()
        {
            // Arrange
            var category = new Category(1, "Test Category", "Description", "assets.jpg", TypeCategoryEnum.ComidaSalgada);

            // Act & Assert
            var exception = Assert.Throws<DomainExceptionValidation>(() => category.AddProduct(null));
            Assert.Equal("Produto não pode ser nulo.", exception.Message);
        }

        [Fact(DisplayName = "Remover produto")]
        public void RemoveProduct_ExistingProduct_ShouldRemoveFromCollection()
        {
            // Arrange
            var category = new Category(1, "Test Category", "Description", "assets.jpg", TypeCategoryEnum.ComidaSalgada);
            var product = new Product(1, 2, category.Id, "Test Product", "Description", "asset.jpg", 10.99m, 0);
            category.AddProduct(product);

            // Act
            category.RemoveProduct(product);

            // Assert
            Assert.Empty(category.Products);
        }

        [Fact(DisplayName = "Tentar Remover produto inexistente")]
        public void RemoveProduct_NonExistingProduct_ShouldNotAffectCollection()
        {
            // Arrange
            var category = new Category(1, "Test Category", "Description", "assets.jpg", TypeCategoryEnum.ComidaSalgada);
            var product1 = new Product(1, 2, category.Id, "Test Product 1", "Description", "asset1.jpg", 10.99m, 0);
            var product2 = new Product(2, 2, category.Id, "Test Product 2", "Description", "asset2.jpg", 15.99m, 0);
            category.AddProduct(product1);

            // Act
            category.RemoveProduct(product2);

            // Assert
            Assert.Single(category.Products);
            Assert.Contains(product1, category.Products);
        }

        [Fact(DisplayName = "Tentar Remover produtos ")]
        public void RemoveProduct_NullProduct_ShouldThrowDomainExceptionValidation()
        {
            // Arrange
            var category = new Category(1, "Test Category", "Description", "assets.jpg", TypeCategoryEnum.Consertos);

            // Act & Assert
            var exception = Assert.Throws<DomainExceptionValidation>(() => category.RemoveProduct(null));
            Assert.Equal("Produto não pode ser nulo.", exception.Message);
        }

        #endregion

        #region COLLECTIONS

        [Fact(DisplayName = "Categoria Inicializa com Coleções Vázias")]
        public void CreateCategory_InitializesWithEmptyCollections()
        {
            // Arrange
            

            // Act
            var category = new Category(1, "Category Name", "Categoria Teste", "imagem", TypeCategoryEnum.ComidaSalgada);

            // Assert
            category.Stores.Should().BeEmpty("porque uma nova categoria deve ter uma coleção de lojas vazia.");
            category.Products.Should().BeEmpty("porque uma nova categoria deve ter uma coleção de produtos vazia.");
        }

        [Fact(DisplayName = "Adicionar Loja à Categoria")]
        public void AddStore_ToCategory_StoreAddedSuccessfully()
        {
            // Arrange
            var applicationUser = new Account("testuser", "xxxx", true, true, true,UserType.StoreOwner);
            var category = new Category(1, "Category Name","Categoria Teste", "imagem", TypeCategoryEnum.ComidaSalgada);
            var store = Store.Create(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "name", 2, "description", "imagens", "1", false, "locations");

            // Act
            category.Stores.Add(store);

            // Assert
            category.Stores.Should().Contain(store, "porque a loja foi adicionada à categoria.");
        }

        [Fact(DisplayName = "Adicionar Produto à Categoria")]
        public void AddProduct_ToCategory_ProductAddedSuccessfully()
        {
            // Arrange
            var applicationUser = new Account("testuser", "xxxx", true, true, true,  UserType.StoreOwner);
            var category = new Category(1, "Category Name","Categoria Teste", "imagem", TypeCategoryEnum.ComidaSalgada);
            var product = Product.Create(1, 2, 1, "Product 1", "Description", "assets", 100.0m, 0);

            // Act
            category.Products.Add(product);

            // Assert
            category.Products.Should().Contain(product, "porque o produto foi adicionado à categoria.");
        }
        #endregion
    }

}
