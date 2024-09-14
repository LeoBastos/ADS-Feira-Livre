namespace ads.feira.application.CQRS.Products.Commands
{
    public class ProductUpdateCommand : ProductCommand
    {
        public string Id { get; set; }
    }
}
