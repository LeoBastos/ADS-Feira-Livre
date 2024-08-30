using ads.feira.domain.Entity.Categories;
using MediatR;

namespace ads.feira.application.CQRS.Categories.Queries
{
    public class GetAllCategoryQuery : IRequest<IEnumerable<Category>>
    {

    }
}
