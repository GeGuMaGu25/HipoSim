using System.Collections.Generic;
using System.Threading.Tasks;
using HipoSim.Platform.PropertyCatalog.Domain.Model.Aggregates;

namespace HipoSim.Platform.PropertyCatalog.Domain.Model.Repositories;

public interface IProjectRepository
{
    Task AddAsync(RealEstateProject project);
    Task<IEnumerable<RealEstateProject>> ListAsync();
    Task SaveChangesAsync();
}