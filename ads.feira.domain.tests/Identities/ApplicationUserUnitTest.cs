using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Entity.Stores;
using FluentAssertions;


namespace ads.feira.domain.tests.Identities
{
    public class ApplicationUserUnitTests
    {
        [Fact(DisplayName = "Deve inicializar como ativo")]
        public void Constructor_ShouldInitializeAsActive()
        {
            // Arrange
            var applicationUser = new Account("testuser", "xxxx", true, true, true, UserType.StoreOwner);

            // Act & Assert
            applicationUser.IsActive.Should().BeTrue("porque um novo usuário deve estar ativo por padrão.");
        }

        [Fact(DisplayName = "Método Remove deve desativar o usuário")]
        public void Remove_ShouldSetIsActiveToFalse()
        {
            // Arrange
            var applicationUser = new Account("testuser", "xxxx", true, true, true, UserType.StoreOwner);

            // Act
            applicationUser.Remove();

            // Assert
            applicationUser.IsActive.Should().BeFalse("porque o método Remove deve definir _isActive como false.");
        }

        [Fact(DisplayName = "Deve permitir adicionar uma revisão")]
        public void AddReview_ShouldAddReviewToCollection()
        {
            // Arrange
            var applicationUser = new Account("testuser", "xxxx", true, true, true, UserType.StoreOwner); 
            var review = Review.Create(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "reviewContent",2, 5);

            // Act
            applicationUser.Reviews.Add(review);

            // Assert
            applicationUser.Reviews.Should().Contain(review, "porque a revisão deve ser adicionada à coleção de revisões.");
        }

        [Fact(DisplayName = "Deve permitir adicionar um cupom resgatado")]
        public void AddRedeemedCoupon_ShouldAddCouponToCollection()
        {
            // Arrange
            var applicationUser = new Account("testuser", "xxxx", true, true, true, UserType.StoreOwner);
            var coupon = Cupon.Create(1, "name", "code", "description", DateTime.UtcNow.AddDays(1), 10, Enums.Cupons.DiscountTypeEnum.Percentage);

            // Act
            applicationUser.RedeemedCoupons.Add(coupon);

            // Assert
            applicationUser.RedeemedCoupons.Should().Contain(coupon, "porque o cupom deve ser adicionado à coleção de cupons resgatados.");
        }

        [Fact(DisplayName = "Deve permitir adicionar uma loja")]
        public void AddStore_ShouldAddStoreToCollection()
        {
            // Arrange
            var applicationUser = new Account("testuser", "xxxx", true, true, true,  UserType.StoreOwner);
            var store = Store.Create(1, "4B660458-AC10-48BA-8226-A8A84F302BC7", "name", 2, "description", "assets", "storeNumbere", false, "locations");

            // Act
            applicationUser.Stores.Add(store);

            // Assert
            applicationUser.Stores.Should().Contain(store, "porque a loja deve ser adicionada à coleção de lojas.");
        }
    }
}
