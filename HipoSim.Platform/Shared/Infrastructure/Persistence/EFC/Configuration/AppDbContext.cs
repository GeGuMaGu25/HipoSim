using HipoSim.Platform.LeadManagement.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using HipoSim.Platform.IAM.Domain.Model.Aggregates;
using HipoSim.Platform.PropertyCatalog.Domain.Model.Aggregates;

namespace HipoSim.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CreditLead> CreditLeads { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<RealEstateProject> RealEstateProjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CreditLead>().ToTable("CreditLeads");
        modelBuilder.Entity<CreditLead>().HasKey(l => l.Id);
        modelBuilder.Entity<CreditLead>().Property(l => l.CustomerEmail).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<CreditLead>().Property(l => l.Currency).IsRequired().HasMaxLength(3);
        modelBuilder.Entity<CreditLead>().Property(c => c.ProjectId).IsRequired();
        modelBuilder.Entity<CreditLead>().Property(c => c.Status).IsRequired().HasMaxLength(50).HasDefaultValue("Pending");
        
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.Role).IsRequired().HasMaxLength(20);
        
        modelBuilder.Entity<RealEstateProject>().ToTable("RealEstateProjects");
        modelBuilder.Entity<RealEstateProject>().HasKey(p => p.Id);
        modelBuilder.Entity<RealEstateProject>().Property(p => p.Name).IsRequired().HasMaxLength(150);
        modelBuilder.Entity<RealEstateProject>().Property(p => p.Location).IsRequired().HasMaxLength(250);
        modelBuilder.Entity<RealEstateProject>().Property(p => p.BasePrice).IsRequired();
    }
}