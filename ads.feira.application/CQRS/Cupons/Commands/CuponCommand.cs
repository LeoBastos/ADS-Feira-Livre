using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Enums.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Commands
{
    public abstract class CuponCommand : IRequest<Cupon>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime Expiration { get; set; }
        public decimal Discount { get; set; }
        public DiscountTypeEnum DiscountType { get; set; }       
    }
}
