using ads.feira.domain.Entity.Products;
using MediatR;

namespace ads.feira.application.CQRS.Products.Commands
{
    public class RemoveCuponFromProductCommand : IRequest<Product>
    {
        public int ProductId { get; set; }
        public int CuponId { get; set; }
    }
}
