namespace ads.feira.domain.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();

        Task Rollback();
    }
}
