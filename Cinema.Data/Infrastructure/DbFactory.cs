namespace Cinema.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private CinemaDbContext dbContext;

        public CinemaDbContext Init()
        {
            return dbContext ?? (dbContext = new CinemaDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}