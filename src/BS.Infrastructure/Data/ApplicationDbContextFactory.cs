using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BS.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        var connectionString = "Host=localhost;Port=5432;Database=bs_db;Username=bs_user;Password=bs_password";
        optionsBuilder.UseNpgsql(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}