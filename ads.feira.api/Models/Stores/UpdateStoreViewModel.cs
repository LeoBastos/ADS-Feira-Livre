﻿namespace ads.feira.api.Models.Stores
{
    public record UpdateStoreViewModel
    {
        public int Id { get; set; }
        public string StoreOwner { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Assets { get; set; }
        public string StoreNumber { get; set; }
        public bool HasDebt { get; set; }
        public string Locations { get; set; }
    }
}