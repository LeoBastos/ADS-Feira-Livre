using ads.feira.domain.Entity.Products;
using MediatR;

namespace ads.feira.application.CQRS.Products.Commands
{
    namespace ads.feira.application.CQRS.Categories.Commands
    {
        public class AddCuponToProductCommand : IRequest<Product>
        {
            public int ProductId { get; set; }
            public int CuponId { get; set; }
        }
    }
}
