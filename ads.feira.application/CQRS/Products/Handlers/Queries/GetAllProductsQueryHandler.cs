using ads.feira.application.CQRS.Products.Queries;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Interfaces.Products;
using MediatR;

namespace ads.feira.application.CQRS.Products.Handlers.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductQuery request,
            CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync();
        }
    }
}
