using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Reviews;
using ads.feira.domain.Entity.Stores;
using Microsoft.AspNetCore.Identity;

namespace ads.feira.domain.Entity.Accounts
{
    public class Account : IdentityUser
    {
        public Account() { }

        public Account(string? name, string? assets, bool tosAccept, bool privacyAccept, bool isActive, UserType userType, StoreOwnerPlan storeOwnerPlan)
        {
            Name = name;
            Assets = assets;
            TosAccept = tosAccept;
            PrivacyAccept = privacyAccept;
            IsActive = isActive;           
            UserType = userType;
            StoreOwnerPlan = storeOwnerPlan;
        }

        public string? Name { get; set; }
        public string? Assets { get; set; }
        public bool TosAccept { get; set; } = true;
        public bool PrivacyAccept { get; set; } = true;
        public bool IsActive { get; set; } = true;       
        public UserType UserType { get; set; }
        public StoreOwnerPlan StoreOwnerPlan { get; set; } = StoreOwnerPlan.Bronze;

        public ICollection<Store> Stores { get; private set; } = new List<Store>();
        public ICollection<Review> Reviews { get; private set; } = new List<Review>();
        public ICollection<Store> FavoriteStores { get; private set; } = new List<Store>();
        public ICollection<Cupon> RedeemedCoupons { get; private set; } = new List<Cupon>();

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateAvatar(string assets)
        {
            Assets = assets;
        }

        public void UpgradePlan(StoreOwnerPlan newPlan)
        {
            if (UserType == UserType.StoreOwner && newPlan > StoreOwnerPlan)
            {
                StoreOwnerPlan = newPlan;
            }
        }

        public void Remove()
        {
            IsActive = false;
        }

        // Methods for StoreOwner
        public void AddStore(Store store)
        {
            if (UserType == UserType.StoreOwner)
            {
                Stores.Add(store);
            }
        }

        public void RemoveStore(Store store)
        {
            if (UserType == UserType.StoreOwner)
            {
                Stores.Remove(store);
            }
        }

        // Methods for Customer
        public void AddFavoriteStore(Store store)
        {
            if (UserType == UserType.Customer && !FavoriteStores.Contains(store))
            {
                FavoriteStores.Add(store);
            }
        }

        public void RemoveFavoriteStore(Store store)
        {
            if (UserType == UserType.Customer)
            {
                FavoriteStores.Remove(store);
            }
        }

        public void AddReview(Review review)
        {
            if (UserType == UserType.Customer)
            {
                Reviews.Add(review);
            }
        }

        public void RemoveReview(Review review)
        {
            if (UserType == UserType.Customer)
            {
                Reviews.Remove(review);
            }
        }

        public void RedeemCoupon(Cupon coupon)
        {
            if (UserType == UserType.Customer && !RedeemedCoupons.Contains(coupon))
            {
                RedeemedCoupons.Add(coupon);
            }
        }
    }

    public enum UserType
    {
        Administrator,
        StoreOwner,
        Customer
    }

    public enum StoreOwnerPlan
    {
        Bronze,
        Silver,
        Gold,
        Platinum
    }
}
