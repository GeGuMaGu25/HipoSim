using System.Threading.Tasks;
using HipoSim.Platform.LeadManagement.Domain.Model.Aggregates;

namespace HipoSim.Platform.LeadManagement.Domain.Model.Repositories;

public interface ICreditLeadRepository
{
    Task AddAsync(CreditLead lead);
    Task SaveChangesAsync();
}