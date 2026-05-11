using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace _27_FrontToBackSqlConnection.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();

        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=mydb;Username=postgres;Password=1234");

        return new AppDbContext(optionsBuilder.Options);
    }
}
