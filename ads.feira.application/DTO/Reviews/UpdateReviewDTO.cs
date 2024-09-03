namespace ads.feira.application.DTO.Reviews
{
    public class UpdateReviewDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ReviewContent { get; set; }
        public int StoreId { get; set; }
        public int Rate { get; set; }
    }
}
