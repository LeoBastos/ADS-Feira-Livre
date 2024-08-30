using ads.feira.domain.Entity.Products;
using MediatR;

namespace ads.feira.application.CQRS.Products.Queries
{
    public class GetProdructByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }

        public GetProdructByIdQuery(int id)
        {
            Id = id;
        }
    }
}
