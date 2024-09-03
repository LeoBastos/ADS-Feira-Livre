using ads.feira.application.CQRS.Reviews.Queries;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Interfaces.Reviews;
using MediatR;

namespace ads.feira.application.CQRS.Reviews.Handlers.Queries
{
    public class GetAllReviewQueryHandler : IRequestHandler<GetAllReviewQuery, IEnumerable<Review>>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetAllReviewQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Review>> Handle(GetAllReviewQuery request,
            CancellationToken cancellationToken)
        {
            return await _reviewRepository.GetAllAsync();
        }
    }
}
