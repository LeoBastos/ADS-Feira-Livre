using ads.feira.application.DTO.Categories;
using System.Linq.Expressions;

namespace ads.feira.application.Interfaces.Categories
{
    public interface ICategoryServices
    {
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task<CategoryDTO> GetById(int id);
        Task<IEnumerable<CategoryDTO>> Find(Expression<Func<CategoryDTO, bool>> predicate);

        Task Create(CreateCategoryDTO categoryDTO, IEnumerable<int> productIds);
        Task Update(UpdateCategoryDTO categoryDTO);
        Task Remove(int id);
    }
}
