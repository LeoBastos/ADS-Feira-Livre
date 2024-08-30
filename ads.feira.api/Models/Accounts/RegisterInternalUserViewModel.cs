using System.ComponentModel;

namespace ads.feira.api.Models.Accounts
{
    public class RegisterInternalUserViewModel
    {
        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Descrição")]
        public string? Description { get; set; }

        [DisplayName("Avatar")]
        public IFormFile Assets { get; set; }

        [DisplayName("Termo de Serviço")]
        public bool TosAccept { get; set; }

        [DisplayName("Termo de Privacidade")]
        public bool PrivacyAccept { get; set; }
    }
}
