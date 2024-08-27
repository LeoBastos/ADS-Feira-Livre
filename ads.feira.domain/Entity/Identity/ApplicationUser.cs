using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Entity.Stores;
using Microsoft.AspNetCore.Identity;

namespace ads.feira.domain.Entity.Identity
{
    public sealed class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        public string FullName { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public bool TosAccept { get; set; }
        public bool PrivacyAccept { get; set; }

        private bool _isActive = true;
        public bool IsActive => _isActive;

        public ICollection<Review> Reviews { get; private set; } = new List<Review>();
        public ICollection<Cupon> RedeemedCoupons { get; private set; } = new List<Cupon>();
        public ICollection<Store> Stores { get; private set; } = new List<Store>();        


        public void Remove()
        {
            _isActive = false;
        }
    }
}
