using InvestAnalytics.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace InvestAnalytics.API.Db;

public class ApplicationContext : DbContext
{
    public DbSet<BondInfo> Bonds { get; set; }
    public DbSet<CouponInfo> Coupons { get; set; }

    public ApplicationContext()
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=localhost;Database=InvestAnalytics.Db;Username=postgres;Password=1324");
    }
}