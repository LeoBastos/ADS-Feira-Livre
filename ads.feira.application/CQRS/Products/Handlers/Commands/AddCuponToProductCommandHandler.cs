using ads.feira.application.CQRS.Products.Commands.ads.feira.application.CQRS.Categories.Commands;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Interfaces.Cupons;
using ads.feira.domain.Interfaces.Products;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Products.Handlers.Commands
{
    public class AddCuponToProductCommandHandler : IRequestHandler<AddCuponToProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICuponRepository _cuponRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddCuponToProductCommandHandler(
            ICuponRepository cuponRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _cuponRepository = cuponRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> Handle(AddCuponToProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(request.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Product with ID {request.ProductId} not found.");
                }

                var cupon = await _cuponRepository.GetByIdAsync(request.CuponId);
                if (cupon == null)
                {
                    throw new InvalidOperationException($"Cupon with ID {request.CuponId} not found.");
                }

                product.AddCoupon(cupon);

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
