using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DotnetShare.Infrastructure.EfCore
{
    public interface IDbFacadeResolver
    {
        DatabaseFacade Database { get; }
    }
}