namespace ads.feira.application.CQRS.Stores.Commands
{
    public class StoreUpdateCommand : StoreCommand
    {
        public string Id { get; set; }
    }
}
