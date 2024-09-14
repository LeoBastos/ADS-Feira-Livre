using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Paginated;
using MediatR;

namespace ads.feira.application.CQRS.Reviews.Queries
{
    public class GetAllReviewQuery : IRequest<PagedResult<Review>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
