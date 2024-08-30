using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Enums.Cupons;

namespace ads.feira.api.Models.Cupons
{
    public class CuponViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime Expiration { get; set; }
        public decimal Discount { get; set; }
        public DiscountTypeEnum DiscountType { get; set; }


        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}
