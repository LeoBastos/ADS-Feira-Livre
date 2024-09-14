using ads.feira.domain.Entity.Products;
using MediatR;

namespace ads.feira.application.CQRS.Products.Commands
{
    public class RemoveCuponFromProductCommand : IRequest<Product>
    {
        public string ProductId { get; set; }
        public string CuponId { get; set; }
    }
}
