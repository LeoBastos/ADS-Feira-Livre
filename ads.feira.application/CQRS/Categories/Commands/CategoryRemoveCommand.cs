using ads.feira.domain.Entity.Categories;
using MediatR;

namespace ads.feira.application.CQRS.Categories.Commands
{
    public class CategoryRemoveCommand : IRequest<Category>
    {
        public int Id { get; set; }

        public CategoryRemoveCommand(int id)
        {
            Id = id;
        }
    }
}
