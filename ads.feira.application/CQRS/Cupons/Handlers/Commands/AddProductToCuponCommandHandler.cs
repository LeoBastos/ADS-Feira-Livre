using ads.feira.application.CQRS.Cupons.Commands;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Interfaces.Cupons;
using ads.feira.domain.Interfaces.Products;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Handlers.Commands
{
    public class AddProductToCuponCommandHandler : IRequestHandler<AddProductToCuponCommand, Cupon>
    {
        private readonly ICuponRepository _cuponRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductToCuponCommandHandler(ICuponRepository cuponRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _cuponRepository = cuponRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Cupon> Handle(AddProductToCuponCommand request, CancellationToken cancellationToken)
        {
            var cupon = await _cuponRepository.GetByIdAsync(request.CuponId);
            if (cupon == null)
            {
                throw new InvalidOperationException($"Cupon with ID {request.CuponId} not found.");
            }

            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with ID {request.ProductId} not found.");
            }

            cupon.AddProduct(product);

            await _cuponRepository.UpdateAsync(cupon);
            await _unitOfWork.Commit();

            return cupon;
        }
    }
}
