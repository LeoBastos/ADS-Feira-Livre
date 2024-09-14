using ads.feira.application.DTO.Categories;
using ads.feira.domain.Paginated;

namespace ads.feira.application.Interfaces.Categories
{
    public interface ICategoryServices
    {      
        Task<PagedResult<CategoryDTO>> GetAll(int pageNumber, int pageSize);
        Task<CategoryDTO> GetById(string id);
      

        Task Create(CreateCategoryDTO categoryDTO);
        Task Update(UpdateCategoryDTO categoryDTO);
        Task Remove(string id);
    }
}
