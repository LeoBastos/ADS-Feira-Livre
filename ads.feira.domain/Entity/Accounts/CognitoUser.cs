using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Entity.Stores;
using ads.feira.domain.Validation;

namespace ads.feira.domain.Entity.Accounts
{
    public sealed class CognitoUser : BaseEntityAccount
    {
        private CognitoUser() { }

        public CognitoUser(Guid id, string email, string? name, string? description, string? assets, bool tosAccept, bool privacyAccept, string? roles)
        {
            ValidateDomain(id, email, name, description, assets, tosAccept, privacyAccept, roles);            
        }
      
        public string Email { get; private set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public string? Assets { get; private set; }
        public bool TosAccept { get; private set; } = true;
        public bool PrivacyAccept { get; private set; } = true;
        public string? Roles { get; set; }
       

        public ICollection<Review> Reviews { get; private set; } = new List<Review>();
        public ICollection<Cupon> RedeemedCoupons { get; private set; } = new List<Cupon>();
        public ICollection<Store> Stores { get; private set; } = new List<Store>();


        public static CognitoUser Create(Guid id, string email, string? name, string? description, string? assets, bool tosAccept, bool privacyAccept, string? roles)
        {
            return new CognitoUser(id, email, name, description, assets, true, true, roles);
        }

        public void Update(Guid id, string email, string name, string description, string assets, bool tosAccept, bool privacyAccept , string roles)
        {
            ValidateDomain(id, email, name, description, assets, tosAccept, privacyAccept, roles);
        }
        public void Remove()
        {
            IsActive = false;
        }

        private void ValidateDomain(Guid id, string email, string? name, string? description, string? assets, bool tosAccept, bool privacyAccept, string? roles)
        {
            DomainExceptionValidation.When(id.ToString().Length < 0, "Id inválido.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Email não pode ser nulo.");
            DomainExceptionValidation.When(email.Length < 2, "Minimo de 2 caracteres.");
            //DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome não pode ser nulo.");
            //DomainExceptionValidation.When(name.Length < 3, "Minimo de 3 caracteres.");
            //DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Descrição não pode ser nulo.");
            //DomainExceptionValidation.When(description.Length < 3, "Minimo de 3 caracteres.");
            //DomainExceptionValidation.When(assets?.Length > 250, "Nome de imagem inválida, máximo 250 caracteres");
            //DomainExceptionValidation.When(string.IsNullOrEmpty(roles), "Role não pode ser nulo.");

            Id = id;
            Email = email;
            Name = name;
            Description = description;
            Assets = assets;
            TosAccept = tosAccept;
            PrivacyAccept = privacyAccept;
            Roles = roles;
        }
    }
}
