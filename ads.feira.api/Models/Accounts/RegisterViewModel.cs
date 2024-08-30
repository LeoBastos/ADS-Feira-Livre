using System.ComponentModel;

namespace ads.feira.api.Models.Accounts
{
    public class RegisterViewModel
    {
        [DisplayName("Email")]
        public string Email { get; set; }
        
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Nome Completo")]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        public string? Description { get;  set; }

        [DisplayName("Avatar")]
        public IFormFile? Assets { get;  set; }

        [DisplayName("Termo de Serviço")]
        public bool TosAccept { get; set; } = true;

        [DisplayName("Termo de Privacidade")]
        public bool PrivacyAccept { get; set; } = true;

        [DisplayName("Role")]
        public string? Roles { get; set; }
    }
}
