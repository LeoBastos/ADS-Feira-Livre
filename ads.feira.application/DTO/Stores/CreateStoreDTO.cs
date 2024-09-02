using ads.feira.domain.Entity.Categories;

namespace ads.feira.application.DTO.Stores
{
    public record CreateStoreDTO
    {
        public int Id { get; set; }
        public string? StoreOwnerId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public string StoreNumber { get; set; }
        public string Locations { get; set; }     
    }
}
