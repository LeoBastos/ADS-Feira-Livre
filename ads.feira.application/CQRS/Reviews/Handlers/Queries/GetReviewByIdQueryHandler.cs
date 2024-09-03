using ads.feira.application.CQRS.Reviews.Queries;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Interfaces.Reviews;
using MediatR;

namespace ads.feira.application.CQRS.Reviews.Handlers.Queries
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, Review>
    {
        private readonly IReviewRepository _context;
        public GetReviewByIdQueryHandler(IReviewRepository context)
        {
            _context = context;
        }

        public async Task<Review> Handle(GetReviewByIdQuery request,
             CancellationToken cancellationToken)
        {
            return await _context.GetByIdAsync(request.Id);
        }
    }
}
