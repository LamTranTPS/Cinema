namespace Cinema.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}