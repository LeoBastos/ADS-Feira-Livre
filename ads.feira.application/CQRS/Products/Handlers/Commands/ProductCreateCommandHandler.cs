using ads.feira.application.CQRS.Products.Commands;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Interfaces.Products;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Products.Handlers.Commands
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductCreateCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = Product.Create(request.Id, request.StoreId, request.CategoryId, request.Name, request.Description, request.Assets, request.Price, request?.DiscountedPrice);

                if (product == null)
                {
                    throw new InvalidOperationException("Failed to create product.");
                }

                await _productRepository.CreateAsync(product);
                await _unitOfWork.Commit();

                return product;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
