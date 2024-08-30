using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Reviews;

namespace ads.feira.application.DTO.Stores
{
    public class StoreDTO
    {
        public StoreDTO()
        {
            
        }

        public int Id { get; set; }
        public string StoreOwner { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public string StoreNumber { get; set; }
        public bool HasDebt { get; set; }
        public string Locations { get; set; }

        public Category Category { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Cupon> AvailableCupons { get; set; } = new List<Cupon>();
        public ICollection<CognitoUser> Users { get; set; } = new List<CognitoUser>();
    }
}
