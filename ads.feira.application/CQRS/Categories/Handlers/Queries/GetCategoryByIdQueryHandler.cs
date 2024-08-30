using ads.feira.application.CQRS.Categories.Queries;
using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Interfaces.Categories;
using MediatR;

namespace ads.feira.application.CQRS.Categories.Handlers.Queries
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly ICategoryRepository _context;
        public GetCategoryByIdQueryHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task<Category> Handle(GetCategoryByIdQuery request,
             CancellationToken cancellationToken)
        {
            return await _context.GetByIdAsync(request.Id);
        }
    }
}
