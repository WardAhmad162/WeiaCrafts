using Microsoft.Extensions.Logging;
using WeiaCraftsInfrastructure.Data;
using WeiaCraftsDomain.Entities;

namespace WeiaCraftsInfrastructure.DataSeeding;

public class AccountStatusSeeder : IDataSeeder
{
    public async Task SeedAsync(WeiaCraftsContext context, ILogger logger)
    {
        if (context.AccountStatuses.Any())
            return;

        logger.LogInformation("Seeding account statuses...");
        
        var statuses = new List<AccountStatus>
        {
            new AccountStatus { Id = 1,StatusName = "Active" },
            new AccountStatus { Id = 2,StatusName = "Suspended" },
            new AccountStatus { Id = 3,StatusName = "Deactivated" }
        };

        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            context.AccountStatuses.AddRange(statuses);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            logger.LogInformation("Account statuses seeded successfully.");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
} 