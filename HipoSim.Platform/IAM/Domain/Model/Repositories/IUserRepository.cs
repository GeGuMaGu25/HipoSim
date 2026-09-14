using System.Threading.Tasks;
using HipoSim.Platform.IAM.Domain.Model.Aggregates;

namespace HipoSim.Platform.IAM.Domain.Model.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> FindByEmailAsync(string email);
    Task SaveChangesAsync();
}