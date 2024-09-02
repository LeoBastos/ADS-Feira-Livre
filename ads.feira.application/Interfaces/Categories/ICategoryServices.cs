using ads.feira.application.DTO.Categories;

namespace ads.feira.application.Interfaces.Categories
{
    public interface ICategoryServices
    {
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task<CategoryDTO> GetById(int id);

        Task Create(CreateCategoryDTO categoryDTO);
        Task Update(UpdateCategoryDTO categoryDTO);
        Task Remove(int id);
    }
}
