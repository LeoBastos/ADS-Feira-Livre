using ads.feira.domain.Entity.Categories;
using ads.feira.domain.Enums.Products;
using MediatR;

namespace ads.feira.application.CQRS.Categories.Commands
{
    public abstract class CategoryCommand : IRequest<Category>
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public string? AssetsPath { get; set; }
    }
}
