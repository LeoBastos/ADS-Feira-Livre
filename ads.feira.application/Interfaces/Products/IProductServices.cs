using ads.feira.application.DTO.Products;

namespace ads.feira.application.Interfaces.Products
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<ProductDTO> GetById(int id);

        Task<IEnumerable<ProductStoreDTO>> GetProductsForStoreId(int storeId);

        Task AddCuponToProduct(int cuponId, int productId);
        Task RemoveCuponFromProduct(int cuponId, int productId);

        Task Create(CreateProductDTO productDTO);
        Task Update(UpdateProductDTO productDTO);
        Task Remove(int id);
    }
}
