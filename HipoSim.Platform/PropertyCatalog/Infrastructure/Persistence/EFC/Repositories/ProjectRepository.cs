using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HipoSim.Platform.PropertyCatalog.Domain.Model.Aggregates;
using HipoSim.Platform.PropertyCatalog.Domain.Model.Repositories;
using HipoSim.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace HipoSim.Platform.PropertyCatalog.Infrastructure.Persistence.EFC.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RealEstateProject project)
    {
        await _context.RealEstateProjects.AddAsync(project);
    }

    public async Task<IEnumerable<RealEstateProject>> ListAsync()
    {
        return await _context.RealEstateProjects.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}