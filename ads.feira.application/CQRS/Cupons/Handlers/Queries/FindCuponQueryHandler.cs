using ads.feira.application.CQRS.Cupons.Queries;
using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Interfaces.Cupons;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace ads.feira.application.CQRS.Cupons.Handlers.Queries
{

    public class FindCuponQueryHandler : IRequestHandler<FindCuponQuery, IEnumerable<Cupon>>
    {
        private readonly ICuponRepository _cuponRepository;
        private readonly IMapper _mapper;

        public FindCuponQueryHandler(ICuponRepository cuponRepository, IMapper mapper)
        {
            _cuponRepository = cuponRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Cupon>> Handle(FindCuponQuery request, CancellationToken cancellationToken)
        {
            // Convert the DTO predicate to an entity predicate
            var entityPredicate = _mapper.Map<Expression<Func<Cupon, bool>>>(request.Predicate);

            // Use the repository to find categories based on the predicate
            var cupons = await _cuponRepository.Find(entityPredicate);

            return cupons;
        }
    }
}
