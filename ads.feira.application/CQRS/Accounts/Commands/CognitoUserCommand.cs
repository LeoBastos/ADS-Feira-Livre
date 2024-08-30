using ads.feira.domain.Entity.Accounts;
using MediatR;

namespace ads.feira.application.CQRS.Accounts.Commands
{
    public class CognitoUserCommand : IRequest<CognitoUser>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Assets { get; set; }
        public bool TosAccept { get; set; } = true;
        public bool PrivacyAccept { get; set; } = true;
        public string? Roles { get; set; }
    }
}
