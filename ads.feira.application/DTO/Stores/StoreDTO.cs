namespace ads.feira.application.DTO.Stores
{
    public class StoreDTO
    {
        public StoreDTO()
        {

        }

        public Guid Id { get; set; }
        public Guid StoreOwnerId { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public string StoreNumber { get; set; }
        public bool HasDebt { get; set; }
        public string Locations { get; set; }
    }
}
