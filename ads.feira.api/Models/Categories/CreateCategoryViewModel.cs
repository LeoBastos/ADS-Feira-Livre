using ads.feira.domain.Enums.Products;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ads.feira.api.Models.Categories
{
    public class CreateCategoryViewModel
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Imagem")]
        public IFormFile Assets { get; set; }
        
        [Required]
        [DisplayName("Tipo")]
        public TypeCategoryEnum Type { get; set; }
    }
}
