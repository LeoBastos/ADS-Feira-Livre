using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Paginated;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Queries
{
    public class GetAllCuponQuery : IRequest<PagedResult<Cupon>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
