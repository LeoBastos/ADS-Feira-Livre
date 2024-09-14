using ads.feira.application.DTO.Stores;
using ads.feira.domain.Paginated;

namespace ads.feira.application.Interfaces.Stores
{
    public interface IStoreServices
    {
        Task<PagedResult<StoreDTO>> GetAll(int pageNumber, int pageSize);
        Task<StoreDTO> GetById(string id);
        Task Create(CreateStoreDTO storeDTO);
        Task Update(UpdateStoreDTO storeDTO);
        Task Remove(string id);
    }
}
