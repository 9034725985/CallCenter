using CallCenter.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CallCenter.Data;

public class CallCenterDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public required DbSet<MyPerson> Persons { get; set; }

    public CallCenterDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
    }
}
