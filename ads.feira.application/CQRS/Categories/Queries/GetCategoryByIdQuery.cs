using ads.feira.domain.Entity.Categories;
using MediatR;

namespace ads.feira.application.CQRS.Categories.Queries
{

    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public string Id { get; set; }

        public GetCategoryByIdQuery(string id)
        {
            Id = id;
        }
    }
}
