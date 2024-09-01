namespace ads.feira.api.Models.Accounts
{
    public record AccountResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
