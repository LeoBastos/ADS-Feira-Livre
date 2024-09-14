namespace ads.feira.application.CQRS.Reviews.Commands
{
    public class ReviewUpdateCommand : ReviewCommand
    {
        public string Id { get; set; }
    }
}
