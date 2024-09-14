using ads.feira.domain.Entity.Reviews;
using MediatR;

namespace ads.feira.application.CQRS.Reviews.Queries
{
    public class GetReviewByIdQuery : IRequest<Review>
    {
        public string Id { get; set; }

        public GetReviewByIdQuery(string id)
        {
            Id = id;
        }
    }
}
