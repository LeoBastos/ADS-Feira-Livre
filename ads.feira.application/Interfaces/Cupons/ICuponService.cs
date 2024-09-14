using ads.feira.application.DTO.Cupons;
using ads.feira.domain.Paginated;

namespace ads.feira.application.Interfaces.Cupons
{
    public interface ICuponService
    {
        Task<PagedResult<CuponDTO>> GetAll(int pageNumber, int pageSize);
        Task<CuponDTO> GetById(string id);

        Task AddProductToCupon(string cuponId, string productId);
        Task RemoveProductFromCupon(string cuponId, string productId);
        Task AddStoreToCupon(string cuponId, string storeId);
        Task RemoveStoreFromCupon(string cuponId, string storeId);
        Task Create(CreateCuponDTO cuponDTO);
        Task Update(UpdateCuponDTO cuponDTO);
        Task Remove(string id);
    }
}
