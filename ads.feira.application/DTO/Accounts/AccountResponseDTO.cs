using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Entity.Stores;
using System.ComponentModel;

namespace ads.feira.application.DTO.Accounts
{
    public record AccountResponseDTO
    {
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Avatar")]
        public string? Assets { get; set; }

        [DisplayName("Role")]
        public string UserType { get; set; }

        public ICollection<Store> Stores { get; private set; } = new List<Store>();
        public ICollection<Review> Reviews { get; private set; } = new List<Review>();
        public ICollection<Store> FavoriteStores { get; private set; } = new List<Store>();
        public ICollection<Cupon> RedeemedCoupons { get; private set; } = new List<Cupon>();
    }
}
