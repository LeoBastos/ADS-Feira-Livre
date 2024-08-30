using ads.feira.application.CQRS.Products.Commands;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Interfaces.Products;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Products.Handlers.Commands
{
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductRemoveCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(request.Id);

                if (product == null)
                {
                    throw new InvalidOperationException($"Product with ID {request.Id} not found.");
                }

                product.Remove();

                await _productRepository.UpdateAsync(product);
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
