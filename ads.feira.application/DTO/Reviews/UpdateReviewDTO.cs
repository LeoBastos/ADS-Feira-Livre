namespace ads.feira.application.DTO.Reviews
{
    public class UpdateReviewDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string ReviewContent { get; set; }
        public Guid StoreId { get; set; }
        public int Rate { get; set; }
    }
}
