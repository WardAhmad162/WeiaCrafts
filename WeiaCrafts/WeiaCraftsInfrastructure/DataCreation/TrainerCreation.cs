using WeiaCraftsDomain.Entities;
using System.Security.Cryptography;

namespace WeiaCraftsInfrastructure.DataCreation;

public static class TrainerCreation
{
    public static List<Trainer> GetPreconfiguredTrainers()
    {
        return new List<Trainer>
        {
            new Trainer
            {
                UserName = "elyaa.sabanah",
                Email = "elyaa.sabanah@gmail.com",
                Password = HashPassword("Trainer123!"),
                AccountStatusId = 1,
                RegisterationDate = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-6)),
                CreatedAt = DateTime.UtcNow.AddMonths(-6),
                IsActive = true,
                Rating = 4.8,
                RatingCount = 150,
                IsAlsoTrainee = false,
                ProfilePictureUrl = "/images/trainers/elyaa-sabaneh.jpeg"
            },
            new Trainer
            {
                UserName = "zahraa.altaan",
                Email = "zahraa.altaan@gmail.com",
                Password = HashPassword("Trainer123!"),
                AccountStatusId = 1,
                RegisterationDate = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-4)),
                CreatedAt = DateTime.UtcNow.AddMonths(-4),
                IsActive = true,
                Rating = 4.5,
                RatingCount = 100,
                IsAlsoTrainee = true,
                ProfilePictureUrl = "/images/trainers/zahraa-altaan.jpeg"
            }
        };
    }

    private static string HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        
        byte[] hashBytes = new byte[salt.Length + hash.Length];
        Array.Copy(salt, 0, hashBytes, 0, salt.Length);
        Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);
        
        return Convert.ToBase64String(hashBytes);
    }
}

