using ads.feira.domain.Enums.Products;
using System.ComponentModel.DataAnnotations;

namespace ads.feira.api.Models.Categories
{
    public class UpdateCategoryViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public TypeCategoryEnum Type { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Description { get; set; }

        public string Assets { get; set; }
    }
}
