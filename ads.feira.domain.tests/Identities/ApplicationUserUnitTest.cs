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
            var applicationUser = CognitoUser.Create(Guid.Parse("4B660458-AC10-48BA-8226-A8A84F302BC7"), "testuser@example.com", "testuser", "xxxx", "sadfsdf", true, true, "admin");

            // Act & Assert
            applicationUser.IsActive.Should().BeTrue("porque um novo usuário deve estar ativo por padrão.");
        }

        [Fact(DisplayName = "Método Remove deve desativar o usuário")]
        public void Remove_ShouldSetIsActiveToFalse()
        {
            // Arrange
            var user = CognitoUser.Create(Guid.Parse("4B660458-AC10-48BA-8226-A8A84F302BC7"), "testuser@example.com", "testuser", "xxxx", "sadfsdf", true, true, "admin");

            // Act
            user.Remove();

            // Assert
            user.IsActive.Should().BeFalse("porque o método Remove deve definir _isActive como false.");
        }

        [Fact(DisplayName = "Deve permitir adicionar uma revisão")]
        public void AddReview_ShouldAddReviewToCollection()
        {
            // Arrange
            var user = CognitoUser.Create(Guid.Parse("4B660458-AC10-48BA-8226-A8A84F302BC7"), "testuser@example.com", "testuser", "xxxx", "sadfsdf", true, true, "admin");
            var review = Review.Create(1, Guid.Parse("4B660458-AC10-48BA-8226-A8A84F302BC7"), "reviewContent",2, 5);

            // Act
            user.Reviews.Add(review);

            // Assert
            user.Reviews.Should().Contain(review, "porque a revisão deve ser adicionada à coleção de revisões.");
        }

        [Fact(DisplayName = "Deve permitir adicionar um cupom resgatado")]
        public void AddRedeemedCoupon_ShouldAddCouponToCollection()
        {
            // Arrange
            var user = CognitoUser.Create(Guid.Parse("4B660458-AC10-48BA-8226-A8A84F302BC7"), "testuser@example.com", "testuser", "xxxx", "sadfsdf", true, true, "admin");
            var coupon = Cupon.Create(1, "name", "code", "description", DateTime.UtcNow.AddDays(1), 10, Enums.Cupons.DiscountTypeEnum.Percentage);

            // Act
            user.RedeemedCoupons.Add(coupon);

            // Assert
            user.RedeemedCoupons.Should().Contain(coupon, "porque o cupom deve ser adicionado à coleção de cupons resgatados.");
        }

        [Fact(DisplayName = "Deve permitir adicionar uma loja")]
        public void AddStore_ShouldAddStoreToCollection()
        {
            // Arrange
            var user = CognitoUser.Create(Guid.Parse("4B660458-AC10-48BA-8226-A8A84F302BC7"), "testuser@example.com", "testuser", "xxxx", "sadfsdf", true, true, "admin");
            var store = Store.Create(1, Guid.Parse("4B660458-AC10-48BA-8226-A8A84F302BC7"), "name", 2, "description", "assets", "storeNumbere", false, "locations"); // Supondo que Store seja uma classe válida

            // Act
            user.Stores.Add(store);

            // Assert
            user.Stores.Should().Contain(store, "porque a loja deve ser adicionada à coleção de lojas.");
        }
    }
}
