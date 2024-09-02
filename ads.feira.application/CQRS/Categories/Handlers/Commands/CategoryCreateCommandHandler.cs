using ads.feira.application.CQRS.Categories.Commands;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Interfaces.Categories;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ads.feira.application.CQRS.Categories.Handlers.Commands
{
    public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryCreateCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<Category> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    throw new InvalidOperationException("User ID not found or invalid.");
                }

                var category = Category.Create(request.Id, request.Name, request.Description, request.Assets, request.Type);

                if (category == null)
                {
                    throw new InvalidOperationException("Failed to create category.");
                }

                await _categoryRepository.CreateAsync(category);
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