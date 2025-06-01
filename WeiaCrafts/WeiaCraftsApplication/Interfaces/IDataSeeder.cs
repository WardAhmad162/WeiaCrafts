using Microsoft.Extensions.Logging;
using WeiaCraftsInfrastructure.Data;

namespace WeiaCraftsApplication.Interfaces;

public interface IDataSeeder
{
    Task SeedAsync(WeiaCraftsContext context, ILogger logger);
} 