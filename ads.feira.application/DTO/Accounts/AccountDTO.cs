using ads.feira.domain.Entity.Accounts;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace ads.feira.application.DTO.Accounts
{
    public record AccountDTO
    {
        public string? Id { get; set; }
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [DisplayName("Avatar")]
        public IFormFile? Assets { get; set; }

        [DisplayName("Termo de Serviço")]
        public bool TosAccept { get; set; } = true;

        [DisplayName("Termo de Privacidade")]
        public bool PrivacyAccept { get; set; } = true;

        [DisplayName("Ativo")]
        public bool IsActive { get; set; } = true;

        [DisplayName("Role")]
        public UserType UserType { get; set; }
    }
}
