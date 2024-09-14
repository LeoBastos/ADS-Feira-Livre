using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ads.feira.application.DTO.Categories
{
    public class CreateCategoryDTO
    {
        public string? Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Imagem")]
        public IFormFile? Assets { get; set; }

        public string? AssetsPath { get; set; }
    }
}
