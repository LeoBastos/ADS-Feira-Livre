using ads.feira.application.CQRS.Products.Queries;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Interfaces.Products;
using MediatR;

namespace ads.feira.application.CQRS.Products.Handlers.Queries
{
    public class GetProductForStoreIdQueryHandler : IRequestHandler<GetProductForStoreIdQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _context;
        public GetProductForStoreIdQueryHandler(IProductRepository context)
        {
            _context = context;
        }
       
        public async Task<IEnumerable<Product>> Handle(GetProductForStoreIdQuery request,
             CancellationToken cancellationToken)
        {
            return await _context.GetProductsForStoreIdAsync(request.StoreId);
        }
    }
}
