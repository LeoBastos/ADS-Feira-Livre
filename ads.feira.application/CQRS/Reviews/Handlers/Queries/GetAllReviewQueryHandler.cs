using ads.feira.application.CQRS.Reviews.Queries;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Interfaces.Reviews;
using ads.feira.domain.Paginated;
using MediatR;

namespace ads.feira.application.CQRS.Reviews.Handlers.Queries
{
    public class GetAllReviewQueryHandler : IRequestHandler<GetAllReviewQuery, PagedResult<Review>>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetAllReviewQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<PagedResult<Review>> Handle(GetAllReviewQuery request,
            CancellationToken cancellationToken)
        {
            return await _reviewRepository.GetAllAsync(request.PageNumber, request.PageSize);
        }
    }
}
