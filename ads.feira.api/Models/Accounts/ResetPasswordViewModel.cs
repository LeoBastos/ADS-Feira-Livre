using System.ComponentModel.DataAnnotations;

namespace ads.feira.api.Models.Accounts
{
    public record ResetPasswordViewModel
    {

        [Required(ErrorMessage = "Informe Email")]
        [MinLength(2)]
        public string Email { get; set; }
        public string Token { get; set; }


        [Required(ErrorMessage = "Insira a nova Senha")]        
        public string NewPassword { get; set; }
    }
}
