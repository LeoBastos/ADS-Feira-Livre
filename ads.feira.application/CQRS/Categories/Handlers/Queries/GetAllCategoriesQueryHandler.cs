using ads.feira.application.CQRS.Categories.Queries;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Interfaces.Categories;
using ads.feira.domain.Paginated;
using MediatR;

namespace ads.feira.application.CQRS.Categories.Handlers.Queries
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoryQuery, PagedResult<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedResult<Category>> Handle(GetAllCategoryQuery request,
            CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAllAsync(request.PageNumber, request.PageSize);
        }
    }
}
