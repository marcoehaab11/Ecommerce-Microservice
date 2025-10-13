using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Infrastructure.Extentions
{
    public static class DbExtension
    { 
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Migrating postresql database start");
                    ApplyMigrationsAsync(config).Wait();
                    logger.LogInformation("Migrating postresql database completed");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the postresql database");
                    throw;
                }
            }
            return host;
        }

        private static async Task ApplyMigrationsAsync(IConfiguration config)
        {
            var retry = 5;
            while (retry > 0)
            {
                try
                {
                    await using var connection =
                        new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
                     connection.Open();
                    var command = new NpgsqlCommand
                    {
                        Connection = connection,
                    };
                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    await command.ExecuteNonQueryAsync();
                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
                    await command.ExecuteNonQueryAsync();
                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('iPhone 15', 'IPhone Discount', 150);";
                    await command.ExecuteNonQueryAsync();
                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Adidas Ultraboost', 'Adidas Discount', 100);";
                    await command.ExecuteNonQueryAsync();
                    break;
                }
                catch (NpgsqlException ex)
                {
                    retry--;
                  if (retry == 0)
                    {
                        throw;
                    }
                        await Task.Delay(2000);
                }
            }
           
        }
    }
}
