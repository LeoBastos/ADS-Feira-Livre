using ads.feira.application.CQRS.Cupons.Queries;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Interfaces.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Handlers.Queries
{
    public class GetAllCuponsQueryHandler : IRequestHandler<GetAllCuponQuery, IEnumerable<Cupon>>
    {
        private readonly ICuponRepository _cuponRepository;

        public GetAllCuponsQueryHandler(ICuponRepository cuponRepository)
        {
            _cuponRepository = cuponRepository;
        }

        public async Task<IEnumerable<Cupon>> Handle(GetAllCuponQuery request,
            CancellationToken cancellationToken)
        {
            return await _cuponRepository.GetAllAsync();
        }
    }
}
