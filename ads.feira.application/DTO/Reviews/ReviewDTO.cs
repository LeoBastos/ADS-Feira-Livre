using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Entity.Stores;

namespace ads.feira.application.DTO.Reviews
{
    public class ReviewDTO
    {
        public ReviewDTO()
        {

        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string ReviewContent { get; set; }
        public int StoreId { get; set; }
        public int Rate { get; set; }

        public Store Store { get; set; }
        public Account User { get; set; }
    }
}
