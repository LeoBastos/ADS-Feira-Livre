using ads.feira.application.DTO.Cupons;
using System.Linq.Expressions;

namespace ads.feira.application.Interfaces.Cupons
{
    public interface ICuponService
    {
        Task<IEnumerable<CuponDTO>> GetAll();
        Task<CuponDTO> GetById(int id);      

        Task AddProductToCupon(int cuponId, int productId);
        Task RemoveProductFromCupon(int cuponId, int productId);
        Task AddStoreToCupon(int cuponId, int storeId);
        Task RemoveStoreFromCupon(int cuponId, int storeId);
        Task Create(CreateCuponDTO cuponDTO);
        Task Update(UpdateCuponDTO cuponDTO);
        Task Remove(int id);
    }
}
