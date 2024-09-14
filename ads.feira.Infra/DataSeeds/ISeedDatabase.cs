namespace ads.feira.Infra.DataSeeds
{
    public interface ISeedDatabase
    {
        Task SeedDataDB(bool forceReseed = false);
    }
}
