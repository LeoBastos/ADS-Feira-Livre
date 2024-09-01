using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ads.feira.api.Models.Accounts
{
    public record RegisterViewModel
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail Inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password é obrigatório")]
        [StringLength(20, ErrorMessage = "O {0} deve ter um minimo de {2} e no máximo " +
            "{1} caracteres.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string? ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Preencha seu nome")]
        [MinLength(3)]
        [DisplayName("Nome Completo")]
        public string? Name { get; set; }

        [DisplayName("Avatar")]
        public IFormFile? Assets { get; set; }

        [DisplayName("Termos de Serviço")]
        public bool TosAccept { get; set; }

        [DisplayName("Termo de Privacidade")]
        public bool PrivacyAccept { get; set; }
    }
}
