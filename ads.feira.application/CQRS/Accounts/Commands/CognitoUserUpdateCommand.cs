namespace ads.feira.application.CQRS.Accounts.Commands
{
    public class CognitoUserUpdateCommand : CognitoUserCommand
    {
        public Guid Id { get; set; }
    }
}
