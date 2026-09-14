using System;

namespace HipoSim.Platform.PropertyCatalog.Domain.Model.Aggregates;

public class RealEstateProject
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public decimal BasePrice { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected RealEstateProject() { } // Requerido por EF Core

    public RealEstateProject(string name, string location, decimal basePrice)
    {
        Id = Guid.NewGuid();
        Name = name;
        Location = location;
        BasePrice = basePrice;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
}