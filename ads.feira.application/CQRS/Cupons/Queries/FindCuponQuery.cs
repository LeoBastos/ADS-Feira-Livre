using ads.feira.application.DTO.Cupons;
using ads.feira.domain.Entity.Cupons;
using MediatR;
using System.Linq.Expressions;

namespace ads.feira.application.CQRS.Cupons.Queries
{
    public class FindCuponQuery : IRequest<IEnumerable<Cupon>>
    {
        public Expression<Func<CuponDTO, bool>> Predicate { get; }

        public FindCuponQuery(Expression<Func<CuponDTO, bool>> predicate)
        {
            Predicate = predicate;
        }
    }
}
