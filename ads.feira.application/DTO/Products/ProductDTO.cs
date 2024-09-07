namespace ads.feira.application.DTO.Products
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            
        }

        public int Id { get; set; }
        public string Store { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
    }
}
