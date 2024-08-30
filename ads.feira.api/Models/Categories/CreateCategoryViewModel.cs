using ads.feira.api.Helpers;
using ads.feira.domain.Enums.Products;
using System.ComponentModel.DataAnnotations;

namespace ads.feira.api.Models.Categories
{
    public class CreateCategoryViewModel
    {       
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }       

        [Required]
        public TypeCategoryEnum Type {  get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Description { get; set; }

        public IFormFile Assets { get; set; }       

        public List<int> ProductIds { get; set; } = new List<int>();
    }
}
