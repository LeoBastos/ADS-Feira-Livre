using ads.feira.application.DTO.Stores;

namespace ads.feira.application.Interfaces.Stores
{
    public interface IStoreServices
    {
        Task<IEnumerable<StoreDTO>> GetAll();
        Task<StoreDTO> GetById(int id);

        Task Create(CreateStoreDTO storeDTO);
        Task Update(UpdateStoreDTO storeDTO);
        Task Remove(int id);
    }
}
