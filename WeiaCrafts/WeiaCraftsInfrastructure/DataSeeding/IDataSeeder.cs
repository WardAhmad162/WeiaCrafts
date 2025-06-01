using Microsoft.Extensions.Logging;
using WeiaCraftsInfrastructure.Data;

namespace WeiaCraftsInfrastructure.DataSeeding;

public interface IDataSeeder
{
    Task SeedAsync(WeiaCraftsContext context, ILogger logger);
} 