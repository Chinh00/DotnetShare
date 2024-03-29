using System.Reflection;
using DotnetShare.Core.Repositoy;
using DotnetShare.Infrastructure.EfCore.Internal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DotnetShare.Infrastructure.EfCore
{
    public static class Extensions
    {
        public static IServiceCollection AddPostgresDbContext<TDbContext>(this IServiceCollection services,
            string connString, Action<DbContextOptionsBuilder> doMoreDbContextOptionsConfigure = null,
            Action<IServiceCollection> doMoreActions = null)
                where TDbContext : DbContext, IDbFacadeResolver
        {
            services.AddDbContext<TDbContext>(options =>
            {
                // options.UseNpgsql(connString, sqlOptions =>
                // {
                //     sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.GetName().Name);
                //     sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                // }).UseSnakeCaseNamingConvention();

                doMoreDbContextOptionsConfigure?.Invoke(options);
            });

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<TDbContext>());


            services.AddHostedService<DbContextMigratorHostedService>();

            doMoreActions?.Invoke(services);

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services, Type repoType)
        {
            services.Scan(scan => scan
                .FromAssembliesOf(repoType)
                .AddClasses(classes =>
                    classes.AssignableTo(repoType)).As(typeof(IRepository<>)).WithScopedLifetime()
                .AddClasses(classes =>
                    classes.AssignableTo(repoType)).As(typeof(IGridRepository<>)).WithScopedLifetime()
            );

            return services;
        }

        
    }
}
