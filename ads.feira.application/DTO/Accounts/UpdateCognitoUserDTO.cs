namespace ads.feira.application.DTO.Accounts
{
    public class UpdateCognitoUserDTO
    {
        public Guid CognitoId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public bool TosAccept { get; set; }
        public bool PrivacyAccept { get; set; }
        public string Roles { get; set; }
    }
}
