namespace ads.feira.api.Models.Accounts
{
    public class UpdateCognitorUserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string? Description { get; private set; }
        public string? Assets { get; private set; }
        public bool? TosAccept { get; private set; }
        public bool? PrivacyAccept { get; private set; }
        public string? Roles { get; set; }
    }
}
