using ads.feira.application.CQRS.Categories.Queries;
using ads.feira.application.CQRS.Products.Queries;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Interfaces.Products;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace ads.feira.application.CQRS.Products.Handlers.Queries
{
    public class FindProductQueryHandler : IRequestHandler<FindProductQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public FindProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> Handle(FindProductQuery request, CancellationToken cancellationToken)
        {
            // Convert the DTO predicate to an entity predicate
            var entityPredicate = _mapper.Map<Expression<Func<Product, bool>>>(request.Predicate);

            // Use the repository to find categories based on the predicate
            var products = await _productRepository.Find(entityPredicate);

            return products;
        }
    }
}
