using HipoSim.Platform.LeadManagement.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace HipoSim.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CreditLead> CreditLeads { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CreditLead>().ToTable("CreditLeads");
        modelBuilder.Entity<CreditLead>().HasKey(l => l.Id);
        modelBuilder.Entity<CreditLead>().Property(l => l.CustomerEmail).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<CreditLead>().Property(l => l.Currency).IsRequired().HasMaxLength(3);
    }
}