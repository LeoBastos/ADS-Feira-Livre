using ads.feira.domain.Entity.Categories;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace ads.feira.application.DTO.Stores
{
    public record CreateStoreDTO
    {
        public string? Id { get; set; }

        [DisplayName("Id Lojista")]
        public string? StoreOwnerId { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Categoria ID")]
        public string CategoryId { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Imagem")]
        public IFormFile? Assets { get; set; }
        public string? AssetsPath { get; set; }

        [DisplayName("Número da Loja")]
        public string StoreNumber { get; set; }

        [DisplayName("Local")]
        public string Locations { get; set; }     
    }
}
