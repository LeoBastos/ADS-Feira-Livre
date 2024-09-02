using ads.feira.domain.Entity.Categories;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ads.feira.api.Models.Stores
{
    public record CreateStoreViewModel
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
