namespace ads.feira.application.DTO.Accounts
{
    public class CreateCognitoUserDTO
    {
        public CreateCognitoUserDTO()
        {
            
        }

        public CreateCognitoUserDTO(Guid id, string email, string name, string? description, string? assets, bool tosAccept, bool privacyAccept, string? roles)
        {
            Id = id;
            Email = email;
            Name = name;
            Description = description;
            Assets = assets;
            TosAccept = tosAccept;
            PrivacyAccept = privacyAccept;
            Roles = roles;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Assets { get; set; }
        public bool TosAccept { get; set; }
        public bool PrivacyAccept { get; set; }
        public string? Roles { get; set; }
    }
}
