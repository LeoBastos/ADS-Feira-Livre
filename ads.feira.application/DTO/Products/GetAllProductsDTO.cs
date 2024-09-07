using Microsoft.AspNetCore.Http;

namespace ads.feira.application.DTO.Products
{
    public class GetAllProductsDTO
    {
        public int Id { get; set; }
        public string Store { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Assets { get; set; }
        public decimal Price { get; set; }       
    }
}
