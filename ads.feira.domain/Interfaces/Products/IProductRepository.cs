using ads.feira.domain.Entity.Products;

namespace ads.feira.domain.Interfaces.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsForStoreIdAsync(string storeId);
        Task<int> GetProductCountByStoreOwnerAsync(string storeId);
    }
}
