using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;

namespace DiscountAPI.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try 
                {
                    logger.LogInformation("Migrating postresql database.");

                    using var conn = new NpgsqlConnection
                                     (configuration.GetValue<string>
                                     ("DatabaseSettings:ConnectionString"));
                    conn.Open();

                    using var command = new NpgsqlCommand { Connection = conn };

                    command.CommandText = "Drop table if exists Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"Create table Coupon(Id Serial Primary Key,
                                        ProductName Varchar(24) Not Null,
                                        Description Text,
                                        Amount Int)";
                    command.ExecuteNonQuery();

                    command.CommandText = "Inserrt into Coupon(ProductName, Description, Amount) values ('iPhone X', 'iPhone X Discount', 150);";
                    command.ExecuteNonQuery();

                    command.CommandText = "Inserrt into Coupon(ProductName, Description, Amount) values ('Samsung 10', 'Samsung 10 Discount', 100);";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrating postresql database.");
                } 
                catch (NpgsqlException ex) 
                {
                    logger.LogError(ex, "An Error occured while migrating the postgresql database");

                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }
            }

            return host;
        }
    }
}
