using Microsoft.Extensions.Logging;
using WeiaCraftsInfrastructure.Data;
using WeiaCraftsDomain.Entities;

namespace WeiaCraftsInfrastructure.DataSeeding;

public class RoleSeeder : IDataSeeder
{
    public async Task SeedAsync(WeiaCraftsContext context, ILogger logger)
    {
        if (context.Roles.Any())
            return;

        logger.LogInformation("Seeding initial roles...");
        
        var roles = new List<Role> 
        { 
            new Role 
            { 
                Name = "Trainer",
                NormalizedName = "TRAINER",
                Description = "Course trainer with ability to create and manage courses",
                IsSystem = true,
                CreatedAt = DateTime.UtcNow
            }, 
            new Role 
            { 
                Name = "Trainee",
                NormalizedName = "TRAINEE",
                Description = "Regular trainee user",
                IsDefault = true,
                IsSystem = true,
                CreatedAt = DateTime.UtcNow
            }, 
            new Role 
            { 
                Name = "Paid Account Trainee",
                NormalizedName = "PAID ACCOUNT TRAINEE",
                Description = "Trainee with paid account privileges",
                IsSystem = true,
                CreatedAt = DateTime.UtcNow
            } 
        };

        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            logger.LogInformation("Roles seeded successfully.");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
} 