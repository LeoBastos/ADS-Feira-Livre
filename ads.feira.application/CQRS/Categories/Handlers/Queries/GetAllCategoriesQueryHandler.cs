using ads.feira.application.CQRS.Categories.Queries;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Interfaces.Categories;
using MediatR;

namespace ads.feira.application.CQRS.Categories.Handlers.Queries
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> Handle(GetAllCategoryQuery request,
            CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}
