using ads.feira.domain.Entity.Reviews;
using MediatR;

namespace ads.feira.application.CQRS.Reviews.Queries
{
    public class GetReviewByIdQuery : IRequest<Review>
    {
        public int Id { get; set; }

        public GetReviewByIdQuery(int id)
        {
            Id = id;
        }
    }
}
