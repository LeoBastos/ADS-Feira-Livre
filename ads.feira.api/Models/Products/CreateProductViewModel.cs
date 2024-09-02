namespace ads.feira.api.Models.Products
{
    public class CreateProductViewModel
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Assets { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
    }  
}
