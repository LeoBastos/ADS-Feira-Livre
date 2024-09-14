using ads.feira.domain.Enums.Cupons;

namespace ads.feira.application.DTO.Cupons
{
    public class UpdateCuponDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime Expiration { get; set; }
        public decimal Discount { get; set; }
        public DiscountTypeEnum DiscountType { get; set; }
    }
}
