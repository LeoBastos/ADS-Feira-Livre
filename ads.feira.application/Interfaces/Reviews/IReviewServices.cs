using ads.feira.application.DTO.Products;
using ads.feira.application.DTO.Reviews;

namespace ads.feira.application.Interfaces.Reviews
{
    public interface IReviewServices
    {
        Task<IEnumerable<ReviewDTO>> GetAll();
        Task<ReviewDTO> GetById(int id);        
        Task Create(CreateReviewDTO reviewDTO);
        Task Update(UpdateReviewDTO reviewDTO);
        Task Remove(int id);
    }
}
