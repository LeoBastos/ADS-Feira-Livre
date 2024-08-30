using ads.feira.application.DTO.Products;
using System.Linq.Expressions;

namespace ads.feira.application.Interfaces.Products
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<ProductDTO> GetById(int id);
        Task<IEnumerable<ProductDTO>> Find(Expression<Func<ProductDTO, bool>> predicate);

        Task AddCuponToProduct(int cuponId, int productId);
        Task RemoveCuponFromProduct(int cuponId, int productId);

        Task Create(CreateProductDTO productDTO);
        Task Update(UpdateProductDTO productDTO);
        Task Remove(int id);
    }
}
