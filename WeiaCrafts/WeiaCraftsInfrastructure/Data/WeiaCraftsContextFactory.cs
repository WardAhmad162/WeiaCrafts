using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WeiaCraftsInfrastructure.Data;

public class WeiaCraftsContextFactory : IDesignTimeDbContextFactory<WeiaCraftsContext>
{
    public WeiaCraftsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WeiaCraftsContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WeiaCraftsDatabase;Trusted_Connection=True;");

        return new WeiaCraftsContext(optionsBuilder.Options);

 
    }
}




