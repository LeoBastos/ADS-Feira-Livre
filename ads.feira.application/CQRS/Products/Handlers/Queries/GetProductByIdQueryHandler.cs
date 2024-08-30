using ads.feira.application.CQRS.Products.Queries;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Interfaces.Products;
using MediatR;

namespace ads.feira.application.CQRS.Products.Handlers.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProdructByIdQuery, Product>
    {
        private readonly IProductRepository _context;
        public GetProductByIdQueryHandler(IProductRepository context)
        {
            _context = context;
        }

        public async Task<Product> Handle(GetProdructByIdQuery request,
             CancellationToken cancellationToken)
        {
            return await _context.GetByIdAsync(request.Id);
        }
    }
}
