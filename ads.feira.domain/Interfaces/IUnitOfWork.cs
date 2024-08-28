namespace ads.feira.domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();

        Task Rollback();
    }
}
