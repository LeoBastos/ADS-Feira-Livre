using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace ads.feira.application.DTO.Products
{
    public class CreateProductDTO
    {
        public int Id { get; set; }

        [DisplayName("Store Id")]
        public int StoreId { get; set; }

        [DisplayName("Category Id")]
        public int CategoryId { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Imagem")]
        public string? Assets { get; set; }

        [DisplayName("Preço")]
        public decimal Price { get; set; }

        [DisplayName("Preço com Desconto")]
        public decimal? DiscountedPrice { get; set; }
    }
}
