using ads.feira.domain.Entity.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Queries
{
    public class GetAllCuponQuery : IRequest<IEnumerable<Cupon>>{}
}
