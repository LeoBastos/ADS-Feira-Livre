using ads.feira.domain.Entity.Reviews;
using MediatR;

namespace ads.feira.application.CQRS.Reviews.Queries
{
    public class GetAllReviewQuery : IRequest<IEnumerable<Review>>
    {

    }
}
