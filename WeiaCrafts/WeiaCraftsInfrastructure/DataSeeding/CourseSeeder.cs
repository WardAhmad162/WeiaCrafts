using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeiaCraftsInfrastructure.Data;
using WeiaCraftsInfrastructure.DataCreation;

namespace WeiaCraftsInfrastructure.DataSeeding;

public class CourseSeeder : IDataSeeder
{
    public async Task SeedAsync(WeiaCraftsContext context, ILogger logger)
    {
        if (context.Courses.Any())
            return;

        var trainer = await context.Trainers.FirstOrDefaultAsync();
        if (trainer == null)
        {
            logger.LogWarning("No trainers found. Skipping course seeding.");
            return;
        }

        logger.LogInformation("Seeding courses...");
        var courses = CourseCreation.GetPreconfiguredCourses();
        foreach (var course in courses)
        {
            course.TrainerUserName = trainer.UserName;
            course.DateOfCreation = DateTime.UtcNow;
            course.LastUpdatedTimestamp = DateTime.UtcNow;
        }

        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            context.Courses.AddRange(courses);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            logger.LogInformation("Courses seeded successfully.");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
} 