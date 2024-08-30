using ads.feira.domain.Entity.Products;
using MediatR;

namespace ads.feira.application.CQRS.Products.Queries
{
    public class GetAllProductQuery : IRequest<IEnumerable<Product>>
    {

    }
}
