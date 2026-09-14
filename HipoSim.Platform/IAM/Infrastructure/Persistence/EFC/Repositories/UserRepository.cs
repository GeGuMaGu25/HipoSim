using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HipoSim.Platform.IAM.Domain.Model.Aggregates;
using HipoSim.Platform.IAM.Domain.Model.Repositories;
using HipoSim.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace HipoSim.Platform.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}