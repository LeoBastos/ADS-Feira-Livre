using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace ads.feira.application.DTO.Categories
{
    public class UpdateCategoryDTO
    {
        public string Id { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Imagem")]
        public IFormFile? Assets { get; set; }
        public string? AssetsPath { get; set; }
    }
}
