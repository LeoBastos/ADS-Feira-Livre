using ads.feira.domain.Entity.Categories;
using MediatR;

namespace ads.feira.application.CQRS.Categories.Commands
{
    public class CategoryRemoveCommand : IRequest<Category>
    {
        public string Id { get; set; }

        public CategoryRemoveCommand(string id)
        {
            Id = id;
        }
    }
}
