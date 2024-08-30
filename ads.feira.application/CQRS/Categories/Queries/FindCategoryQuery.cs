using ads.feira.application.DTO.Categories;
using ads.feira.domain.Entity.Categories;
using MediatR;
using System.Linq.Expressions;

namespace ads.feira.application.CQRS.Categories.Queries
{
    public class FindCategoryQuery : IRequest<IEnumerable<Category>>
    {
        public Expression<Func<CategoryDTO, bool>> Predicate { get; }

        public FindCategoryQuery(Expression<Func<CategoryDTO, bool>> predicate)
        {
            Predicate = predicate;
        }
    }
}
