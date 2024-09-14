using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Paginated;
using MediatR;

namespace ads.feira.application.CQRS.Categories.Queries
{
    public class GetAllCategoryQuery : IRequest<PagedResult<Category>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
