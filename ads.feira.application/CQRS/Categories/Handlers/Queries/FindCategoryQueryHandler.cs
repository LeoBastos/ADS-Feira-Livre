using ads.feira.application.CQRS.Categories.Queries;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Interfaces.Categories;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace ads.feira.application.CQRS.Categories.Handlers.Queries
{
    public class FindCategoryQueryHandler : IRequestHandler<FindCategoryQuery, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public FindCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> Handle(FindCategoryQuery request, CancellationToken cancellationToken)
        {
            // Convert the DTO predicate to an entity predicate
            var entityPredicate = _mapper.Map<Expression<Func<Category, bool>>>(request.Predicate);

            // Use the repository to find categories based on the predicate
            var categories = await _categoryRepository.Find(entityPredicate);

            return categories;
        }
    }
}
