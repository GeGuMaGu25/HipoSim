using System;

namespace HipoSim.Platform.IAM.Domain.Model.Aggregates;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; } // "Admin" o "Agent"
    public DateTime CreatedAt { get; private set; }

    protected User() { } // Requerido por EF Core

    public User(string email, string passwordHash, string role)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }
}