using ads.feira.domain.Entity.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Queries
{
    public class GetCuponByIdQuery : IRequest<Cupon>
    {
        public int Id { get; set; }

        public GetCuponByIdQuery(int id)
        {
            Id = id;
        }
    }
}
