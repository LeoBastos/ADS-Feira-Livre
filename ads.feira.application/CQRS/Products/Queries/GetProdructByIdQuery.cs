using ads.feira.domain.Entity.Products;
using MediatR;

namespace ads.feira.application.CQRS.Products.Queries
{
    public class GetProdructByIdQuery : IRequest<Product>
    {
        public string Id { get; set; }

        public GetProdructByIdQuery(string id)
        {
            Id = id;
        }
    }
}
