namespace ads.feira.api.Models.Reviews
{
    public class CreateReviewViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ReviewContent { get; set; }
        public int StoreId { get; set; }
        public int Rate { get; set; }
    }
}
