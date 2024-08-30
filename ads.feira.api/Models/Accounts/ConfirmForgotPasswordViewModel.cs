namespace ads.feira.api.Models.Accounts
{
    public class ConfirmForgotPasswordViewModel
    {      
        public string Email { get; set; }        
        public string NewPassword { get; set; }
        public string ConfirmationCode { get; set; }
    }
}
