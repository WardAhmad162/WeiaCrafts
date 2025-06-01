using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using WeiaCraftsInfrastructure.Data;
using WeiaCraftsInfrastructure.DataCreation;
using WeiaCraftsDomain.Entities;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsInfrastructure.DataSeeding;

public class TrainerSeeder : IDataSeeder
{
    public async Task SeedAsync(WeiaCraftsContext context, ILogger logger)
    {
        logger.LogInformation("Seeding trainers...");

        var trainerRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Trainer");
        var activeStatus = await context.AccountStatuses.FirstOrDefaultAsync(s => s.StatusName == "Active");

        if (trainerRole == null || activeStatus == null)
        {
            logger.LogWarning("Trainer role or active status not found. Skipping trainer seeding.");
            return;
        }

        var trainers = TrainerCreation.GetPreconfiguredTrainers();

        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            foreach (var trainer in trainers)
            {
                var existingTrainer = await context.Trainers
                    .FirstOrDefaultAsync(t => t.UserName == trainer.UserName);

                if (existingTrainer != null)
                {
                    logger.LogInformation($"Trainer {trainer.UserName} already exists. Skipping.");
                    continue;
                }

                // Rehash the password fresh
                using var hmac = new HMACSHA512();
                var salt = hmac.Key;
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Trainer123!"));

                byte[] hashBytes = new byte[salt.Length + hash.Length];
                Array.Copy(salt, 0, hashBytes, 0, salt.Length);
                Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

                trainer.Password = Convert.ToBase64String(hashBytes);
                trainer.AccountStatusId = activeStatus.Id;
                trainer.IsActive = true;
                trainer.RegisterationDate = DateOnly.FromDateTime(DateTime.UtcNow);
                trainer.CreatedAt = DateTime.UtcNow;

                context.Trainers.Add(trainer);
                await context.SaveChangesAsync();

                var cart = new Cart
                {
                    User = trainer,
                    UserName = trainer.UserName,
                    TotalAmount = new AmountWithCurrency { Amount = 0, Currency = "ISL" }
                };

                context.Carts.Add(cart);
                await context.SaveChangesAsync();

                trainer.CartId = cart.Id;
                context.Trainers.Update(trainer);
                await context.SaveChangesAsync();

                var userRole = new UserRole
                {
                    UserName = trainer.UserName,
                    User = trainer,
                    RoleId = trainerRole.Id,
                    Role = trainerRole,
                    IsActive = true,
                    AssignedAt = DateTime.UtcNow
                };

                context.UserRoles.Add(userRole);
                await context.SaveChangesAsync();

                logger.LogInformation($"Trainer {trainer.UserName} seeded successfully.");
            }

            await transaction.CommitAsync();
            logger.LogInformation("All trainers seeded successfully.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "Error occurred while seeding trainers. Transaction rolled back.");
            throw;
        }
    }
}
