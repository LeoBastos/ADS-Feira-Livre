using ads.feira.application.CQRS.Cupons.Queries;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Interfaces.Cupons;
using MediatR;

namespace ads.feira.application.CQRS.Cupons.Handlers.Queries
{
    public class GetCuponByIdQueryHandler : IRequestHandler<GetCuponByIdQuery, Cupon>
    {
        private readonly ICuponRepository _context;
        public GetCuponByIdQueryHandler(ICuponRepository context)
        {
            _context = context;
        }

        public async Task<Cupon> Handle(GetCuponByIdQuery request,
             CancellationToken cancellationToken)
        {
            return await _context.GetByIdAsync(request.Id);
        }
    }
}
