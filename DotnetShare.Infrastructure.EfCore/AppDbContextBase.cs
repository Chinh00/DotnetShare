using Microsoft.EntityFrameworkCore;

namespace DotnetShare.Infrastructure.EfCore
{
    public abstract class AppDbContextBase : DbContext, IDbFacadeResolver
    {
        protected AppDbContextBase(DbContextOptions options) : base(options)
        {
        }

        
    }
}
