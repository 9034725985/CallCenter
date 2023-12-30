using CallCenter.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CallCenter.Data;

public class CallCenterDbContext(IConfiguration configuration) : DbContext
{
    public required DbSet<MyPerson> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"));
    }
}
