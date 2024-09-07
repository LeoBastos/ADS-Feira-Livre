namespace ads.feira.api.Models.Stores
{
    public class StoreViewModel
    {
        public int Id { get; set; }
        public string? StoreOwnerId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public IFormFile? Assets { get; set; }
        public string StoreNumber { get; set; }
        public string Locations { get; set; }
    }
}
