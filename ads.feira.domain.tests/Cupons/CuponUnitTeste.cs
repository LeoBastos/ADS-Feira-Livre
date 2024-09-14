//using ads.feira.domain.Entity.Cupons;
//using ads.feira.domain.Entity.Products;
//using ads.feira.domain.Entity.Stores;
//using ads.feira.domain.Enums.Cupons;
//using ads.feira.domain.Validation;
//using FluentAssertions;

//namespace ads.feira.domain.tests.Cupons
//{
//    public class CuponUnitTeste
//    {
//        [Fact(DisplayName = "Criar Cupon com valores Válidos")]
//        public void CreateCupon_WithValidParameters_ResultObjectValidState()
//        {
//            // Act
//            Action action = () => new Cupon(1, "Cupom 10", "Barraca10", "cupom da barraca 10", DateTime.UtcNow.AddDays(5), 10, Enums.Cupons.DiscountTypeEnum.Fixed);

//            // Assert
//            action.Should()
//                 .NotThrow<Validation.DomainExceptionValidation>();
//        }

//        [Fact(DisplayName = "Criar Cupon com Id Negativo")]
//        public void CreateCupon_NegativeIdValue_DomainExceptionInvalidId()
//        {
//            // Act
//            Action action = () => new Cupon(-1, "cupon", "xyz", "lorem ipsun", DateTime.UtcNow.AddDays(1), 10, DiscountTypeEnum.Fixed);

//            // Assert
//            action.Should()
//                .Throw<Validation.DomainExceptionValidation>()
//                 .WithMessage("Id inválido.");
//        }

//        [Fact(DisplayName = "Nome Menor que 3 Caracteres")]
//        public void CreateCupon_ShortNameValue_DomainExceptionShortName()
//        {
//            // Act
//            Action action = () => new Cupon(1, "n", "code", "description", DateTime.UtcNow.AddDays(1), 10, DiscountTypeEnum.Percentage);

//            action.Should()
//                .Throw<Validation.DomainExceptionValidation>()
//                   .WithMessage("Minimo de 3 caracteres.");
//        }

//        [Fact(DisplayName = "Descrição Menor que 3 Caracteres")]
//        public void CreateCupon_ShortDescriptionValue_DomainExceptionShortName()
//        {
//            // Act
//            Action action = () => new Cupon(1, "Nome", "Code", "d", DateTime.UtcNow.AddDays(1), 1, DiscountTypeEnum.Percentage);

//            action.Should()
//                .Throw<Validation.DomainExceptionValidation>()
//                   .WithMessage("Minimo de 3 caracteres.");
//        }

//        [Fact(DisplayName = "Code Menor que 2 Caracteres")]
//        public void CreateCupon_ShortCodeValue_DomainExceptionShortName()
//        {
//            // Act
//            Action action = () => new Cupon(1, "Nome", "C", "description", DateTime.UtcNow.AddDays(1), 1, DiscountTypeEnum.Percentage);

//            action.Should()
//                .Throw<Validation.DomainExceptionValidation>()
//                   .WithMessage("Minimo de 2 caracteres.");
//        }

//        [Fact(DisplayName = "Data Com valor Retroativo")]
//        public void CreateCupon_WithInvalidDateTimeValue_DomainExceptionShortName()
//        {
//            // Act
//            Action action = () => new Cupon(1, "Nome", "Code", "description", DateTime.UtcNow.AddDays(-1), 1, DiscountTypeEnum.Percentage);

//            action.Should()
//                .Throw<Validation.DomainExceptionValidation>()
//                   .WithMessage("O cupom não é válido ou está expirado.");
//        }

//        [Fact(DisplayName = "Desconto com valor negativo")]
//        public void CreateCupon_WithInvalidDiscountValue_DomainExceptionShortName()
//        {
//            // Act
//            Action action = () => new Cupon(1, "Nome", "Code", "description", DateTime.UtcNow.AddDays(1), -1, DiscountTypeEnum.Percentage);

//            action.Should()
//                .Throw<Validation.DomainExceptionValidation>()
//                   .WithMessage("Desconto não pode ser menor igual a zero");
//        }

//        [Fact(DisplayName = "Desconto com 0 value")]
//        public void CreateCupon_WithNullDiscountValue_DomainExceptionShortName()
//        {
//            // Act
//            Action action = () => new Cupon(1, "Nome", "Code", "description", DateTime.UtcNow.AddDays(1), 0, DiscountTypeEnum.Percentage);

//            action.Should()
//                .Throw<Validation.DomainExceptionValidation>()
//                   .WithMessage("Desconto não pode ser menor igual a zero");
//        }

//        #region METHODS

//        [Fact(DisplayName = "Adicionar Produto e Stores")]
//        public void AddProductAndStores_ValidProduct_ShouldAddToCollection()
//        {
//            // Arrange
//            var cupon = new Cupon(1, "name", "code", "Description", DateTime.UtcNow.AddDays(1), 10, DiscountTypeEnum.Fixed);
//            var product = new Product(1, 2, 1, "nome", "Description", "asset.jpg", 10.99m, 0);
//            var store = new Store(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "name", 1, "description", "imagens", "2", true, "locations");

//            // Act
//            cupon.AddProduct(product);
//            cupon.AddStore(store);

//            // Assert
//            Assert.Single(cupon.Products);
//            Assert.Single(cupon.Stores);
//            Assert.Contains(product, cupon.Products);
//            Assert.Contains(store, cupon.Stores);
//        }

//        [Fact(DisplayName = "Tentar Adicionar Produto com objeto Null")]
//        public void AddProduct_NullProduct_ShouldThrowDomainExceptionValidation()
//        {
//            // Arrange
//            var cupon = new Cupon(1, "name", "code", "Description", DateTime.UtcNow.AddDays(1), 10, DiscountTypeEnum.Fixed);

//            // Act & Assert
//            var exception = Assert.Throws<DomainExceptionValidation>(() => cupon.AddProduct(null));
//            Assert.Equal("Produto não pode ser nulo.", exception.Message);
//        }

//        [Fact(DisplayName = "Remover produto")]
//        public void RemoveProduct_ExistingProduct_ShouldRemoveFromCollection()
//        {
//            // Arrange
//            var cupon = new Cupon(1, "name", "code", "Description", DateTime.UtcNow.AddDays(1), 10, DiscountTypeEnum.Fixed);
//            var product = new Product(1, 2, 1, "Test Product", "Description", "asset.jpg", 10.99m, 0);
//            var store = new Store(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "name", 1, "description", "imagens", "2", true, "locations");

//            cupon.AddProduct(product);
//            cupon.AddStore(store);

//            // Act
//            cupon.RemoveProduct(product);
//            cupon.RemoveStore(store);

//            // Assert
//            Assert.Empty(cupon.Products);
//            Assert.Empty(cupon.Stores);
//        }

//        #endregion

//        #region COLLECTIONS

//        [Fact(DisplayName = "Cupon Inicializa com Coleções Vázias")]
//        public void CreateCupon_InitializesWithEmptyCollections()
//        {
//            // Act
//            var cupon = new Cupon(1, "name", "code", "description", DateTime.UtcNow.AddDays(1), 25, DiscountTypeEnum.Fixed);

//            // Assert
//            cupon.Stores.Should().BeEmpty("porque um novo cupon deve ter uma coleção de lojas vazia.");
//            cupon.Products.Should().BeEmpty("porque um novo cupon deve ter uma coleção de produtos vazia.");
//        }

//        [Fact(DisplayName = "Adicionar Cupon a Loja")]
//        public void AddStore_ToCupon_StoreAddedSuccessfully()
//        {
//            // Arrange           
//            var cupon = new Cupon(1, "name", "code", "description", DateTime.UtcNow.AddDays(1), 20, DiscountTypeEnum.Percentage);
//            var store = Store.Create(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "name", 2, "description", "imagens", "1", false, "locations");

//            // Act
//            cupon.Stores.Add(store);

//            // Assert
//            cupon.Stores.Should().Contain(store, "porque a loja foi adicionada ao Cupon.");
//        }

//        [Fact(DisplayName = "Adicionar Cupon ao Produto")]
//        public void AddProduct_ToCupon_ProductAddedSuccessfully()
//        {
//            // Arrange

//            var cupon = new Cupon(1, "name", "code", "description", DateTime.UtcNow.AddDays(1), 20, DiscountTypeEnum.Percentage);
//            var product = Product.Create(1, 2, 1, "Product 1", "Description", "assets", 100.0m, 0);

//            // Act
//            cupon.Products.Add(product);

//            // Assert
//            cupon.Products.Should().Contain(product, "porque o produto foi adicionado ao cupon.");
//        }

//        #endregion
//    }
//}
