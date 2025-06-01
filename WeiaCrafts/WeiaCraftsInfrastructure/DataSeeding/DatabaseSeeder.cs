using Microsoft.Extensions.Logging;
using WeiaCraftsInfrastructure.Data;

namespace WeiaCraftsInfrastructure.DataSeeding;

public class DatabaseSeeder
{
    private readonly IEnumerable<IDataSeeder> _seeders;
    private readonly ILogger _logger;

    public DatabaseSeeder(ILogger logger)
    {
        _logger = logger;
        _seeders = new List<IDataSeeder>
        {
            new AccountStatusSeeder(),
            new RoleSeeder(),
            new TrainerSeeder(),
            new CourseSeeder()
        };
    }

    public async Task SeedAsync(WeiaCraftsContext context)
    {
        try
        {
            foreach (var seeder in _seeders)
            {
                await seeder.SeedAsync(context, _logger);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during database seeding.");
            if (ex.InnerException != null)
            {
                _logger.LogError(ex.InnerException, "Inner Exception Details:");
            }
            throw;
        }
    }
} 