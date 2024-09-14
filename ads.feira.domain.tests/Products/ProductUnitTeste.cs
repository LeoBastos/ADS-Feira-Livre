//using ads.feira.domain.Entity.Cupons;
//using ads.feira.domain.Entity.Products;
//using ads.feira.domain.Enums.Cupons;
//using ads.feira.domain.Validation;
//using FluentAssertions;

//namespace ads.feira.domain.tests.Products
//{
//    public class ProductUnitTeste
//    {
//        [Fact(DisplayName = "Criar Produto com valores Válidos")]
//        public void CreateProduct_WithValidParameters_ResultObjectValidState()
//        {
//            // Act
//            Action action = () => new Product(1, 2, 3, "name", "description", "imagens", 15, 0);

//            // Assert
//            action.Should()
//                 .NotThrow<Validation.DomainExceptionValidation>();
//        }

//        [Fact(DisplayName = "Criar Produto com Id Inválido")]
//        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
//        {
//            // Act
//            Action action = () => new Product(-1, 2, 2, "name", "description", "imagens", 15, 0);

//            // Assert
//            action.Should()
//                .Throw<Validation.DomainExceptionValidation>()
//                 .WithMessage("Id inválido.");
//        }

//        [Fact(DisplayName = "Nome Menor que 3 Caracteres")]
//        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
//        {
//            // Act
//            Action action = () => new Product(1, 2, 2, "n", "description", "imagens", 15, 0);

//            action.Should()
//                .Throw<Validation.DomainExceptionValidation>()
//                   .WithMessage("Minimo de 3 caracteres.");
//        }

//        [Fact(DisplayName = "Descrição Menor que 3 Caracteres")]
//        public void CreateProduct_ShortDescriptionValue_DomainExceptionShortName()
//        {
//            // Act
//            Action action = () => new Product(1, 2, 1, "name", "de", "imagens", 15, 0);

//            action.Should()
//                .Throw<Validation.DomainExceptionValidation>()
//                   .WithMessage("Minimo de 3 caracteres.");
//        }

//        [Fact(DisplayName = "Produto com Valor inválido")]
//        public void CreateProduct_WithInvalidPrice_DomainExceptionShortName()
//        {
//            // Act
//            Action action = () => new Product(1, 2, 2, "name", "description", "imagens", -15, 0);

//            action.Should()
//                .Throw<Validation.DomainExceptionValidation>()
//                   .WithMessage("Valor de preço inválido");
//        }

//        #region METHODS

//        [Fact(DisplayName = "UpdateDiscountedPrice com valores Válidos ")]
//        public void UpdateDiscountedPrice_WithValidCoupons_ShouldCalculateCorrectly()
//        {
//            // Arrange
//            var product = new Product(1, 2, 2, "Test Product", "Description", "asset.jpg", 100m, null);
//            var percentageCoupon = new Cupon(1, "10% Off", "PERCENT10", "10% discount", DateTime.Now.AddDays(30), 10m, DiscountTypeEnum.Percentage);
//            var fixedCoupon = new Cupon(2, "$5 Off", "FIXED5", "$5 discount", DateTime.Now.AddDays(30), 5m, DiscountTypeEnum.Fixed);

//            product.AddCoupon(percentageCoupon);
//            product.AddCoupon(fixedCoupon);

//            // Act
//            product.UpdateDiscountedPrice();

//            // Assert
//            Assert.Equal(85m, product.DiscountedPrice);
//        }

//        //[Fact(DisplayName = "UpdateDiscountedPrice com valores Inválidos ")]
//        //public void UpdateDiscountedPrice_WithExpiredCoupon_ShouldIgnoreExpiredCoupon()
//        //{
//        //    // Arrange
//        //    var product = new Product(1, "Store1", "Category1", "Test Product", "Description", "asset.jpg", 100m, null);
//        //    var validCoupon = new Cupon(1, "10% Off", "PERCENT10", "10% discount", DateTime.Now.AddDays(30), 10m, DiscountTypeEnum.Percentage);
//        //    var expiredCoupon = new Cupon(2, "$5 Off", "FIXED5", "$5 discount", DateTime.Now.AddDays(-1), 5m, DiscountTypeEnum.Fixed);

//        //    product.AvailableCoupons.Add(validCoupon);
//        //    product.AvailableCoupons.Add(expiredCoupon);

//        //    // Act
//        //    product.UpdateDiscountedPrice();

//        //    // Assert
//        //    Assert.Equal(90m, product.DiscountedPrice);
//        //}

//        [Fact(DisplayName = "Remover Cupon e Atualizando Preços")]
//        public void RemoveCoupon_ExistingCoupon_ShouldRemoveAndUpdatePrice()
//        {
//            // Arrange
//            var product = new Product(1, 2, 2, "Test Product", "Description", "asset.jpg", 100m, null);
//            var coupon = new Cupon(1, "10% Off", "PERCENT10", "10% discount", DateTime.Now.AddDays(30), 10m, DiscountTypeEnum.Percentage);
//            product.AddCoupon(coupon);

//            // Act
//            product.RemoveCoupon(coupon);

//            // Assert
//            Assert.Empty(product.AvailableCoupons);
//            Assert.Equal(100m, product.DiscountedPrice);
//        }

//        [Fact(DisplayName = "Remover Cupon não existente")]
//        public void RemoveCoupon_NonExistingCoupon_ShouldNotAffectPriceOrCollection()
//        {
//            // Arrange
//            var product = new Product(1, 2, 2, "Test Product", "Description", "asset.jpg", 100m, null);
//            var existingCoupon = new Cupon(1, "10% Off", "PERCENT10", "10% discount", DateTime.Now.AddDays(30), 10m, DiscountTypeEnum.Percentage);
//            var nonExistingCoupon = new Cupon(2, "$5 Off", "FIXED5", "$5 discount", DateTime.Now.AddDays(30), 5m, DiscountTypeEnum.Fixed);
//            product.AddCoupon(existingCoupon);

//            // Act
//            product.RemoveCoupon(nonExistingCoupon);

//            // Assert
//            Assert.Single(product.AvailableCoupons);
//            Assert.Equal(90m, product.DiscountedPrice);
//        }

//        [Fact(DisplayName = "Remover Cupon null ")]
//        public void RemoveCoupon_NullCoupon_ShouldThrowDomainExceptionValidation()
//        {
//            // Arrange
//            var product = new Product(1, 2, 2, "Test Product", "Description", "asset.jpg", 100m, null);

//            // Act & Assert
//            var exception = Assert.Throws<DomainExceptionValidation>(() => product.RemoveCoupon(null));
//            Assert.Equal("O cupom não pode ser nulo.", exception.Message);
//        }

//        [Fact(DisplayName = "Adicionando Cupon Válido")]
//        public void AddCoupon_ValidCoupon_ShouldAddAndUpdatePrice()
//        {
//            // Arrange
//            var product = new Product(1, 2, 2, "Test Product", "Description", "asset.jpg", 100m, null);
//            var coupon = new Cupon(1, "10% Off", "PERCENT10", "10% discount", DateTime.Now.AddDays(30), 10m, DiscountTypeEnum.Percentage);

//            // Act
//            product.AddCoupon(coupon);

//            // Assert
//            Assert.Single(product.AvailableCoupons);
//            Assert.Equal(90m, product.DiscountedPrice);
//        }

//        //[Fact(DisplayName = "Tentando Adicionar Cupon Expirado")]
//        //public void AddCoupon_ExpiredCoupon_ShouldThrowDomainExceptionValidation()
//        //{
//        //    // Arrange
//        //    var product = new Product(1, "Store1", "Category1", "Test Product", "Description", "asset.jpg", 100m, null);
//        //    var expiredCoupon = new Cupon(1, "10% Off", "PERCENT10", "10% discount", DateTime.Now.AddDays(-1), 10m, DiscountTypeEnum.Percentage);

//        //    // Act & Assert
//        //    var exception = Assert.Throws<DomainExceptionValidation>(() => product.AddCoupon(expiredCoupon));
//        //    Assert.Equal("O cupom não é válido ou está expirado.", exception.Message);
//        //}

//        [Fact(DisplayName = "Adicionar null Cupon ")]
//        public void AddCoupon_NullCoupon_ShouldThrowDomainExceptionValidation()
//        {
//            // Arrange
//            var product = new Product(1, 2, 2, "Test Product", "Description", "asset.jpg", 100m, null);

//            // Act & Assert
//            var exception = Assert.Throws<DomainExceptionValidation>(() => product.AddCoupon(null));
//            Assert.Equal("O cupom não pode ser nulo.", exception.Message);
//        }
//        #endregion


//        #region COLLECTIONS

//        [Fact(DisplayName = "Produto Inicializa com Coleções Vázias")]
//        public void CreateProduct_InitializesWithEmptyCollections()
//        {
//            // Act
//            var product = new Product(1,2, 2, "name", "description", "imagens", 15, 0);

//            // Assert
//            product.AvailableCoupons.Should().BeEmpty("porque um novo produto deve ter uma coleção de lojas vazia.");
//        }
//        #endregion
//    }
//}
