using ads.feira.domain.Entity.Products;
using MediatR;

namespace ads.feira.application.CQRS.Products.Commands
{
    public class ProductRemoveCommand : IRequest<Product>
    {
        public string Id { get; set; }

        public ProductRemoveCommand(string id)
        {
            Id = id;
        }
    }
}
