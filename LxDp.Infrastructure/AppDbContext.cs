using LxDp.Domain.DataModels;
using Microsoft.EntityFrameworkCore;

namespace LxDp.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Server> Servers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Script> Scripts { get; set; }
    public DbSet<Deployment> Deployments { get; set; }
    public DbSet<ProjectConfiguration> ProjectConfigurations { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<User> Users { get; set; }
}
