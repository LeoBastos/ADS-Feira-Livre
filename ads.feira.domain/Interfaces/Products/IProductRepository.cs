using ads.feira.domain.Entity.Products;
using ads.feira.domain.Entity.Stores;

namespace ads.feira.domain.Interfaces.Products
{
    public interface IProductRepository : IRepository<Product>
    { 
        Task<IEnumerable<Product>> GetProductsForStoreIdAsync(int storeId);
    }
}
