﻿using System.ComponentModel.DataAnnotations;

namespace ads.feira.api.Models.Accounts
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Informe Email")]
        [MinLength(2)]
        public string Email { get; set; }
    }
}
