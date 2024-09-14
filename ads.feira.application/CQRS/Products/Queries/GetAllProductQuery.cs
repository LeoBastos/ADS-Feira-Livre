using ads.feira.domain.Entity.Products;
using ads.feira.domain.Paginated;
using MediatR;

namespace ads.feira.application.CQRS.Products.Queries
{
    public class GetAllProductQuery : IRequest<PagedResult<Product>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
