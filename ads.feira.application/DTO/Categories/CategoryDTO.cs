using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Enums.Products;

namespace ads.feira.application.DTO.Categories
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TypeCategoryEnum Type { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }

        public int CreatedById { get; set; }
        public Account CreatedBy { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}
