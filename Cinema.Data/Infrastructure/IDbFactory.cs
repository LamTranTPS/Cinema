using System;

namespace Cinema.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        CinemaDbContext Init();
    }
}