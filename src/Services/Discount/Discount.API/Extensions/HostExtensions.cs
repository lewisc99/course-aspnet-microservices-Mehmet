using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.API.Extensions
{
    public static class HostExtensions
    {


        //when the application starts the first thing will do is drop table.
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry =0) //going to retry migration operation
        {
            int retryFOrAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();


                try
                {
                    logger.LogInformation("Migrating postgresql database");

                    using var connection = new NpgsqlConnection
                    (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();


                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
                    command.ExecuteNonQuery();


                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated postgresql database.");


                }
                catch(NpgsqlException e)
                {
                    logger.LogError(e, "An error occured while migrating the postgsql database");

                    if (retryFOrAvailability < 50)
                    {
                        retryFOrAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryFOrAvailability);
                    }
                }

            }

            return host;

        }

    }
}
