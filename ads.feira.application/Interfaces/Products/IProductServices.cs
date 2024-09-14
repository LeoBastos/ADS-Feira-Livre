using ads.feira.application.DTO.Products;
using ads.feira.domain.Paginated;

namespace ads.feira.application.Interfaces.Products
{
    public interface IProductServices
    {
        Task<PagedResult<ProductDTO>> GetAll(int pageNumber, int pageSize);
        Task<ProductDTO> GetById(string id);

        Task<IEnumerable<ProductStoreDTO>> GetProductsForStoreId(string storeId);

        Task AddCuponToProduct(string cuponId, string productId);
        Task RemoveCuponFromProduct(string cuponId, string productId);

        Task Create(CreateProductDTO productDTO);
        Task Update(UpdateProductDTO productDTO);
        Task Remove(string id);
    }
}
