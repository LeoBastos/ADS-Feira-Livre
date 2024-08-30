using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Entity.Stores;

namespace ads.feira.application.DTO.CognitoUsers
{
    public class CognitoUserDTO
    {
        public CognitoUserDTO()
        {

        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public bool TosAccept { get; set; }
        public bool PrivacyAccept { get; set; }
        public string Roles { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Cupon> RedeemedCoupons { get; set; } = new List<Cupon>();
        public ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}
