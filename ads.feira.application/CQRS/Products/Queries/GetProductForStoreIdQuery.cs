using ads.feira.domain.Entity.Products;
using MediatR;

namespace ads.feira.application.CQRS.Products.Queries
{
    public class GetProductForStoreIdQuery : IRequest<IEnumerable<Product>>
    {
        public int StoreId { get; set; }

        public GetProductForStoreIdQuery(int storeId)
        {
            StoreId = storeId;
        }
    }
}
