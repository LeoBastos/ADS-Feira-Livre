using ads.feira.application.DTO.Products;
using ads.feira.application.DTO.Reviews;
using ads.feira.domain.Paginated;

namespace ads.feira.application.Interfaces.Reviews
{
    public interface IReviewServices
    {
        Task<PagedResult<ReviewDTO>> GetAll(int pageNumber, int pageSize);
        Task<ReviewDTO> GetById(string id);        
        Task Create(CreateReviewDTO reviewDTO);
        Task Update(UpdateReviewDTO reviewDTO);
        Task Remove(string id);
    }
}
