using ads.feira.domain.Enums.Products;

namespace ads.feira.application.DTO.Categories
{
    public class UpdateCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TypeCategoryEnum Type { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public int CreatedById { get; set; }
    }
}
