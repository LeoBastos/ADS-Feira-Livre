using ads.feira.application.DTO.Products;
using ads.feira.domain.Entity.Products;
using MediatR;
using System.Linq.Expressions;

namespace ads.feira.application.CQRS.Products.Queries
{
    public class FindProductQuery : IRequest<IEnumerable<Product>>
    {
        public Expression<Func<ProductDTO, bool>> Predicate { get; }

        public FindProductQuery(Expression<Func<ProductDTO, bool>> predicate)
        {
            Predicate = predicate;
        }
    }
}
