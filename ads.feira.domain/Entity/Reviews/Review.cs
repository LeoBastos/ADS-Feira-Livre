using ads.feira.domain.Entity.Identity;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Validation;

namespace ads.feira.domain.Entity.Reviews
{
    public sealed class Review : BaseEntity
    {
        private Review() { }

        public Review(int id, int userId, string reviewContent, int storeId, int rate)
        {
            ValidateDomain(id, userId, reviewContent, storeId, rate);            
        }

        public int UserId { get; private set; }
        public string ReviewContent { get; private set; }
        public int StoreId { get; private set; }       
        public int Rate { get; private set; }

        public Store Store { get; private set; }
        public CognitoUser User { get; private set; }



        public static Review Create(int id, int userId, string reviewContent, int storeId, int rate)
        {
            return new Review(id, userId, reviewContent, storeId, rate);
        }

        public void Update(int id, int userId, string reviewContent, int storeId, int rate)
        {
            ValidateDomain(id, userId, reviewContent, storeId, rate);
        }

        public void Remove()
        {
            IsActive = false;
        }

        private void ValidateDomain(int id, int userId, string reviewContent, int storeId, int rate)
        {
            DomainExceptionValidation.When(id < 0, "Id inválido.");
            DomainExceptionValidation.When(userId < 0, "Id inválido.");
            DomainExceptionValidation.When(storeId < 0, "Id inválido.");           

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
