using Npgsql;

namespace MyTrainingSheet.Api.Extension;

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

                using var connection = new NpgsqlConnection
                    (configuration.GetConnectionString("TrainingSheetConnectionString"));
                connection.Open();

                using var command = new NpgsqlCommand
                {
                    Connection = connection
                };

                command.CommandText = "DROP TABLE IF EXISTS Lifts";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Lifts (Id SERIAL PRIMARY KEY, 
                                                                Name VARCHAR(255) NOT NULL,
                                                                Weight INT,
                                                                Reps INT)";
                command.ExecuteNonQuery();

                command.CommandText = @"INSERT INTO Lifts (Name, Weight, Reps) 
                                        VALUES('Deadlift', 20, 3);";
                command.ExecuteNonQuery();

                command.CommandText = @"INSERT INTO Lifts (Name, Weight, Reps) 
                                        VALUES('Squat', 30, 4);";
                command.ExecuteNonQuery();

                logger.LogInformation("Migrated postresql database.");
            }
            catch (NpgsqlException ex)
            {
                logger.LogError(ex, "An error occurred while migrating the postresql database");

                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host, retryForAvailability);
                }
            }
        }
        return host;
    }
}
