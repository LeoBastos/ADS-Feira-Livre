using ads.feira.application.CQRS.Categories.Commands;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Interfaces.Categories;
using ads.feira.domain.Interfaces.Products;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;

namespace ads.feira.application.CQRS.Categories.Handlers.Commands
{
    public class AddProductToCategoryCommandHandler : IRequestHandler<AddProductToCategoryCommand, Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductToCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Category> Handle(AddProductToCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
                if (category == null)
                {
                    throw new InvalidOperationException($"Category with ID {request.CategoryId} not found.");
                }

                var product = await _productRepository.GetByIdAsync(request.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Product with ID {request.ProductId} not found.");
                }

                category.AddProduct(product);

                await _categoryRepository.UpdateAsync(category);
                await _unitOfWork.Commit();

                return category;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw; 
            }
        }
    }
}
