using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace ads.feira.application.DTO.Accounts
{
    public class AccountUpdateDTO
    {

        [DisplayName("Nome")]
        public string? Name { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Avatar")]
        public IFormFile? Assets { get; set; }
    }
}
