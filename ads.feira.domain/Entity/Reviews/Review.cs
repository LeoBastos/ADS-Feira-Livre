using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Validation;

namespace ads.feira.domain.Entity.Reviews
{
    public sealed class Review : BaseEntity
    {
        private Review() { }

        public Review(string id, string userId, string reviewContent, string storeId, int rate)
        {
            ValidateDomain(id, userId, reviewContent, storeId, rate);
        }

        public string UserId { get; private set; }
        public string ReviewContent { get; private set; }
        public string StoreId { get; private set; }
        public int Rate { get; private set; }

        public Store Store { get; private set; }
        public Account User { get; private set; }



        public static Review Create(string id, string userId, string reviewContent, string storeId, int rate)
        {
            return new Review(id, userId, reviewContent, storeId, rate);
        }

        public void Update(string id, string userId, string reviewContent, string storeId, int rate)
        {
            ValidateDomain(id, userId, reviewContent, storeId, rate);
        }

        public void Remove()
        {
            IsActive = false;
        }

        private void ValidateDomain(string id, string userId, string reviewContent, string storeId, int rate)
        {
            DomainExceptionValidation.When(userId.ToString().Length < 0, "Id inválido.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(reviewContent), "Review não pode ser nulo.");
            DomainExceptionValidation.When(reviewContent.Length < 3, "Minimo de 3 caracteres.");
            DomainExceptionValidation.When(rate < 0 || rate > 5, "Rate deve ter um valor entre 0 e 5");

            Id = id;
            UserId = userId;
            ReviewContent = reviewContent;
            StoreId = storeId;
            Rate = rate;
        }
    }
}
