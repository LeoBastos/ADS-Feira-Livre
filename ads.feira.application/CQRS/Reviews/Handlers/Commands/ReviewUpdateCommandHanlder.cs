using ads.feira.application.CQRS.Reviews.Commands;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Interfaces.Reviews;
using ads.feira.domain.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ads.feira.application.CQRS.Reviews.Handlers.Commands
{
    public class ReviewUpdateCommandHanlder : IRequestHandler<ReviewUpdateCommand, Review>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewUpdateCommandHanlder(IReviewRepository reviewRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<Review> Handle(ReviewUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var review = await _reviewRepository.GetByIdAsync(request.Id);

                if (review == null)
                {
                    throw new InvalidOperationException($"Category with ID {request.Id} not found.");
                }

                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    throw new InvalidOperationException("User ID not found or invalid.");
                }


                review.Update(request.Id, request.UserId, request.ReviewContent, request.StoreId, request.Rate);

                await _reviewRepository.UpdateAsync(review);
                await _unitOfWork.Commit();

                return review;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
