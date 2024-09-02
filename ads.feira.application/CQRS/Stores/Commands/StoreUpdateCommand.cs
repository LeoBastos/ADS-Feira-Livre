namespace ads.feira.application.CQRS.Stores.Commands
{
    public class StoreUpdateCommand : StoreCommand
    {
        public int Id { get; set; }
    }
}
