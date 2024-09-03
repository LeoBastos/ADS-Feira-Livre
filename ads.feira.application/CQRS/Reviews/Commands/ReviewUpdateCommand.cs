namespace ads.feira.application.CQRS.Reviews.Commands
{
    public class ReviewUpdateCommand : ReviewCommand
    {
        public int Id { get; set; }
    }
}
