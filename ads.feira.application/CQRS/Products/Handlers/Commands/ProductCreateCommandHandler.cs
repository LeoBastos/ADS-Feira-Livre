using ads.feira.application.CQRS.Products.Commands;
using ads.feira.application.Interfaces.ProductPlanServices;
using ads.feira.domain.Entity.Products;
using ads.feira.domain.Interfaces.Accounts;
using ads.feira.domain.Interfaces.Products;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ads.feira.application.CQRS.Products.Handlers.Commands
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductPlanService _productPlanService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductCreateCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IAccountRepository accountRepository, IProductPlanService productPlanService, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _productPlanService = productPlanService ?? throw new ArgumentNullException(nameof(productPlanService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(_httpContextAccessor));
        }

        public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    throw new InvalidOperationException("User ID not found or invalid.");
                }


                if (!await _productPlanService.CanAddProduct(userIdClaim.Value))
                {
                    throw new InvalidOperationException("Product limit reached for the current plan.");
                }

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

            //try
            //{
            //    var product = Product.Create(request.Id, request.StoreId, request.CategoryId, request.Name, request.Description, request.Assets, request.Price, request?.DiscountedPrice);

            //    if (product == null)
            //    {
            //        throw new InvalidOperationException("Failed to create product.");
            //    }

            //    await _productRepository.CreateAsync(product);
            //    await _unitOfWork.Commit();

            //    return product;
            //}
            //catch (Exception)
            //{
            //    await _unitOfWork.Rollback();
            //    throw;
            //}
        }
    }
}
