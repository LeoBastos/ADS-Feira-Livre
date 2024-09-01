namespace ads.feira.api.Models.Accounts
{
    public record UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
