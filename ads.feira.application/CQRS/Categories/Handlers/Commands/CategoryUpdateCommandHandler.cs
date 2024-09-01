using ads.feira.application.CQRS.Categories.Commands;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Interfaces.Categories;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ads.feira.application.CQRS.Categories.Handlers.Commands
{
    public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand, Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryUpdateCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<Category> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(request.Id);

                if (category == null)
                {
                    throw new InvalidOperationException($"Category with ID {request.Id} not found.");
                }

                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    throw new InvalidOperationException("User ID not found or invalid.");
                }


                category.Update(request.Id, request.Name, request.Description, request.Assets);

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
