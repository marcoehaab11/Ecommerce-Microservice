using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Polly;

namespace Ordering.API.Extensions
{
    public static class DbExtension
    {

        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetRequiredService<TContext>();
                try
                {
                    logger.LogInformation("Staerted db migration {DbContextName}", typeof(TContext).Name);
                    var rety = Policy.Handle<SqlException>()
                        .WaitAndRetry(
                            retryCount: 5,
                            SynchronizationLockException => TimeSpan.FromSeconds(5),
                            (exception, timeSpan, retryCount, context) =>
                            {
                                logger.LogWarning(exception, "An error occurred while migrating the database {DbContextName}. Retrying {RetryCount} of {TotalRetries}", typeof(TContext).Name, retryCount, 5);
                            });

                        rety.Execute(() =>CallSeeder(seeder, context, services));

                        logger.LogInformation("finished db migration {DbContextName}", typeof(TContext).Name);
                   
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                }
            }
            return host;
        }

        private static void CallSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
