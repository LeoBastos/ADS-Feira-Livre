using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace ads.feira.application.DTO.Stores
{
    public record UpdateStoreDTO
    {
        public Guid Id { get; set; }
        public Guid? StoreOwnerId { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        [DisplayName("Imagem")]
        public IFormFile? Assets { get; set; }
        public string? AssetsPath { get; set; }
        public string StoreNumber { get; set; }
        public bool HasDebt { get; set; }
        public string Locations { get; set; }
    }
}
