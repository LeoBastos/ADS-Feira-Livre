using ads.feira.domain.Enums.Products;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ads.feira.application.DTO.Categories
{
    public class CreateCategoryDTO 
    {
        public int Id { get; set; }
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

        public string AssetsPath { get; set; }

        [Required]
        [DisplayName("Tipo")]
        public TypeCategoryEnum Type { get; set; }
    }
}
