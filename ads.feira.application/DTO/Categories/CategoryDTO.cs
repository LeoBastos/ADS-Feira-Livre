using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ads.feira.application.DTO.Categories
{
    public record CategoryDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 3)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Imagem")]
        public string Assets { get; set; }

        [Required]
        [DisplayName("Tipo")]
        public string Type { get; set; }
    }
}
