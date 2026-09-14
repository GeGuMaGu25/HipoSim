using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HipoSim.Platform.LeadManagement.Domain.Model.Aggregates;
using HipoSim.Platform.LeadManagement.Domain.Model.Repositories;
using HipoSim.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace HipoSim.Platform.LeadManagement.Infrastructure.Persistence.EFC.Repositories;

public class CreditLeadRepository : ICreditLeadRepository
{
    private readonly AppDbContext _context;

    public CreditLeadRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CreditLead lead)
    {
        await _context.CreditLeads.AddAsync(lead);
    }
    
    public async Task<IEnumerable<CreditLead>> ListAsync()
    {
        return await _context.CreditLeads.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}